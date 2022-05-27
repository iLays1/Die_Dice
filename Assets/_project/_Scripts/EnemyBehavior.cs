using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    CombatUnit unitSelf;

    private void Awake()
    {
        unitSelf = GetComponent<CombatUnit>();
    }

    public void TakeTurn(CombatUnit playerUnit)
    {
        Attack(3, playerUnit);
    }

    public void Attack(int damage, CombatUnit playerUnit) => StartCoroutine(AttackCoroutine(damage, playerUnit));
    IEnumerator AttackCoroutine(int damage, CombatUnit playerUnit)
    {
        unitSelf.transform.DOComplete();

        Sequence s = DOTween.Sequence();
        s.Append(unitSelf.transform.DORotate(new Vector3(60f, -90, 90), 0.5f));
        s.Append(unitSelf.transform.DORotate(new Vector3(90, 0, -180), 0.4f).SetEase(Ease.OutBack));
        
        yield return new WaitForSeconds(0.5f);
        playerUnit.TakeDamage(damage);
        yield return new WaitForSeconds(0.1f);
    }
}
