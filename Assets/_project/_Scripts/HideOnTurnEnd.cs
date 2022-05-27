using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTurnEnd : MonoBehaviour
{
    Vector3 opos;

    private void Start()
    {
        opos = transform.position;

        CombatManager.OnTurnStart.AddListener(TurnStart);
        CombatManager.OnTurnEnd.AddListener(TurnEnd);
    }

    void TurnStart()
    {
        transform.DOComplete();
        transform.DOMove(opos, 1f).SetEase(Ease.OutBack);
    }

    void TurnEnd()
    {
        transform.DOComplete();
        transform.DOMove(opos + (Vector3.down * 10), 0.6f).SetEase(Ease.InBack);
    }
}
