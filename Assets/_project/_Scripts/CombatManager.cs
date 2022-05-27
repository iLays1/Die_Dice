using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CombatState
{
    PlayerTurn,
    Rolling,
    EnemyTurn,
    Animation,
    BattleOver
}

public class CombatManager : Singleton<CombatManager>
{
    public CombatState state = CombatState.PlayerTurn;

    public static UnityEvent OnTurnStart = new UnityEvent();
    public static UnityEvent OnTurnEnd = new UnityEvent();

    public CombatUnit playerUnit;
    public CombatUnit enemyUnit;
    EnemyBehavior enemyBehavior;
    DiceManager diceManager;

    protected override void Awake()
    {
        diceManager = FindObjectOfType<DiceManager>();
        enemyBehavior = enemyUnit.GetComponent<EnemyBehavior>();

        playerUnit.OnUnitDeath.AddListener(() =>
        {
            state = CombatState.BattleOver;
            Debug.Log("Lose");
            StopAllCoroutines();
        });
        enemyUnit.OnUnitDeath.AddListener(() =>
        {
            state = CombatState.BattleOver;
            Debug.Log("Win");
            StopAllCoroutines();
        });

        base.Awake();
    }
    private void Start()
    {
        OnTurnStart.Invoke();
    }

    public void EndTurn()
    {
        if (state != CombatState.PlayerTurn)
            return;

        StartCoroutine(EndTurnCoroutine());
    }
    IEnumerator EndTurnCoroutine()
    {
        state = CombatState.Animation;

        OnTurnEnd.Invoke();

        yield return new WaitForSeconds(0.2f);

        var opos = playerUnit.transform.position;

        for (int i = 0; i < diceManager.attacksRolled; i++)
        {
            Debug.Log("Attack");
            
            playerUnit.transform.DOComplete();

            Sequence s = DOTween.Sequence();
            s.Append(playerUnit.transform.DORotate(new Vector3(60f, 90, -90), 0.5f));
            s.Append(playerUnit.transform.DORotate(new Vector3(90, 0, -180), 0.4f).SetEase(Ease.OutBack));

            //playerUnit.transform.DOPunchPosition(Vector3.left * 0.9f, 0.5f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(0.5f);
            enemyUnit.TakeDamage(PlayerDataSystem.Instance.playerAttackPower);
            yield return new WaitForSeconds(0.1f);
        }

        playerUnit.transform.DOMove(opos, 0.3f);

        if(diceManager.blocksRolled > 0)
        {
            yield return new WaitForSeconds(0.35f);
            playerUnit.transform.DOComplete();
            playerUnit.transform.DOJump(playerUnit.transform.position, 1, 1, 0.8f).SetEase(Ease.OutBack);
            playerUnit.GainArmour(diceManager.blocksRolled * PlayerDataSystem.Instance.playerBlockPower);
        }

        yield return new WaitForSeconds(0.6f);
        state = CombatState.EnemyTurn;

        enemyBehavior.TakeTurn(playerUnit);

        yield return new WaitForSeconds(0.5f);
        state = CombatState.PlayerTurn;
        OnTurnStart.Invoke();
    }

}
