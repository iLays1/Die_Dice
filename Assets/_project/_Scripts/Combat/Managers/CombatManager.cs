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
    PlayerDiceManager diceManager;

    protected override void Awake()
    {
        enemyUnit = GetComponent<EncounterLoader>().enemy;

        diceManager = FindObjectOfType<PlayerDiceManager>();
        enemyBehavior = enemyUnit.GetComponent<EnemyBehavior>();

        playerUnit.OnUnitDeath.AddListener(() =>
        {
            state = CombatState.BattleOver;
            WinLoseManager.OnLoseEvent.Invoke();
            StopAllCoroutines();
        });
        enemyUnit.OnUnitDeath.AddListener(() =>
        {
            state = CombatState.BattleOver;
            WinLoseManager.OnWinEvent.Invoke();
            StopAllCoroutines();
        });
        WinLoseManager.OnWinEvent.AddListener(() => { Instance = null; });

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
        for (int i = 0; i < diceManager.totalAttacksRolled; i++)
        {
            //Player Attack code
            playerUnit.transform.DOComplete();
            Sequence s = DOTween.Sequence();
            s.Append(playerUnit.transform.DORotate(new Vector3(75f, 90, -90), 0.3f));
            s.Append(playerUnit.transform.DORotate(new Vector3(110, 90, -90), 0.1f).SetEase(Ease.OutBack));
            s.Append(playerUnit.transform.DORotate(new Vector3(90, 0, -180), 0.3f));

            yield return new WaitForSeconds(0.3f);
            enemyUnit.TakeDamage(GameDataSystem.Instance.attackPower);
            yield return new WaitForSeconds(0.1f);
        }
        playerUnit.transform.DOMove(opos, 0.3f);

        if(diceManager.totalBlocksRolled > 0)
        {
            //Player Block code

            yield return new WaitForSeconds(0.35f);
            playerUnit.transform.DOComplete();
            playerUnit.transform.DOJump(playerUnit.transform.position, 1, 1, 0.8f).SetEase(Ease.OutBack);
            playerUnit.GainArmour(diceManager.totalBlocksRolled * GameDataSystem.Instance.blockPower);
        }

        yield return new WaitForSeconds(0.6f);

        //Enemy Turn
        state = CombatState.EnemyTurn;
        enemyBehavior.TakeTurn(playerUnit);

        yield return new WaitForSeconds(1.2f); //this should probably be more robust
        state = CombatState.PlayerTurn;
        OnTurnStart.Invoke();
    }

}
