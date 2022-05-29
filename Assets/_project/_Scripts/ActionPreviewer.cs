using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionPreviewer : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI valueText;

    public void ShowAction(EnemyAction action)
    {
        iconImage.sprite = action.previewIcon;
        iconImage.color = action.previewIconColor;
        valueText.text = action.value.ToString();

        transform.DOPunchScale(transform.lossyScale + (Vector3.one*0.4f), 0.4f);
    }
}
