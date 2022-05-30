using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPreviewer : MonoBehaviour
{
    public DisplayDieCanvasPos[] dicePositions;
    public Transform panel;
    public bool hideOnStart = true;
    Vector3 opos;
    bool hidden = false;

    private void Start()
    {
        opos = panel.position;

        if(hideOnStart)
        {
            panel.position = opos + (Vector3.down * 10);
            hidden = true;
        }

        CombatManager.OnTurnEnd.AddListener(Hide);

        for (int i = 0; i < PlayerDataSystem.Instance.dice.Count; i++)
        {
            var die = PlayerDataSystem.Instance.dice[i];
            var pos = dicePositions[i];

            pos.data = die;
        }
    }

    public void Toggle()
    {
        if(CombatManager.Instance != null && CombatManager.Instance.state != CombatState.PlayerTurn)
        {
            Hide();
            return;
        }

        hidden = !hidden;

        if (hidden)
             Hide();
        else Show();
    }

    public void Show()
    {
        panel.DOKill();
        panel.DOMove(opos, 0.5f).SetEase(Ease.OutBack);
        hidden = false;
    }
    public void Hide()
    {
        panel.DOKill();
        panel.DOMove(opos + (Vector3.down*5), 0.4f).SetEase(Ease.InBack); ;
        hidden = true;
    }
}
