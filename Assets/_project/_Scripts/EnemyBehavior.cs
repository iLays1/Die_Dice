using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyAction currentAction { get { return actions[indexOrder[actionIndex]]; } }
    public EnemyAction[] actions;
    public ActionPreviewer previewer;

    public int actionIndex = -1;
    public int[] indexOrder;

    CombatUnit unitSelf;

    private void Awake()
    {
        WinLoseManager.OnCombatEnd.AddListener(() => Destroy(this));
        unitSelf = GetComponent<CombatUnit>();
        QueNextAction();
    }

    public void TakeTurn(CombatUnit playerUnit)
    {
        currentAction.Use(playerUnit, unitSelf);

        if(currentAction.actionType != EnemyAction.Type.Attack)
            QueNextAction();
    }

    private void QueNextAction()
    {
        if(indexOrder == null)
        {
            indexOrder = CustomUtility.GetRandomIntArray(0, actions.Length);
        }

        actionIndex++;
        if (actionIndex >= indexOrder.Length)
        {
            actionIndex = 0;
            indexOrder = CustomUtility.GetRandomIntArray(0, actions.Length);
        }
        previewer.ShowAction(currentAction);
    }

    public void Attack(int damage, CombatUnit playerUnit) => StartCoroutine(AttackCoroutine(damage, playerUnit));
    IEnumerator AttackCoroutine(int damage, CombatUnit playerUnit)
    {
        unitSelf.transform.DOComplete();

        Sequence s = DOTween.Sequence();
        s.Append(unitSelf.transform.DORotate(new Vector3(75f, -90, 90), 0.8f));
        s.Append(unitSelf.transform.DORotate(new Vector3(110, -90, 90), 0.15f).SetEase(Ease.OutBack));
        s.Append(unitSelf.transform.DORotate(new Vector3(90, -90, 90), 0.3f));

        yield return new WaitForSeconds(0.8f);
        playerUnit.TakeDamage(damage);
        yield return new WaitForSeconds(0.5f);
        QueNextAction();
    }
}
