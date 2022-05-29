using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoPanel : MonoBehaviour
{
    public static DiceInfoPanelDisplayer displayerHovered = null;

    public Image[] iconImages;
    bool shown = false;
    Vector3 oPos;
    
    private void Start()
    {
        oPos = transform.position;

        Hide(true);
    }

    private void Update()
    {
        if(CombatManager.Instance != null && CombatManager.Instance.state != CombatState.PlayerTurn)
        {
            if(shown)
                Hide();

            return;
        }

        if(displayerHovered == null && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            Hide();
        }
    }

    public void Show(Die die)
    {
        for (int i = 0; i < iconImages.Length; i++)
        {
            var faceData = die.data.faces[i];
            var iconImage = iconImages[i];

            iconImage.sprite = faceData.faceSprite;
            iconImage.color = die.data.color;
        }

        if(shown)
        {
            transform.DOComplete();
            transform.DOPunchPosition(Vector3.up * 2f, 0.3f).SetEase(Ease.OutElastic);
        }
        else
        {
            transform.DOKill();
            transform.DOMove(oPos, 0.5f).SetEase(Ease.OutBack);
        }

        shown = true;
    }
    public void Hide(bool instant = false)
    {
        shown = false;

        var finalPos = oPos + (Vector3.up * 10);
        transform.DOKill();

        if (instant)
        {
            transform.position = finalPos;
        }
        else
        {
            transform.DOMove(finalPos, 0.35f);
        }
    }
}
