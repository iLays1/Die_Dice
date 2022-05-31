using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionPreviewer : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI valueText;

    public void ShowAction(EnemyAction action, EnemyBehavior enemy)
    {
        iconImage.sprite = action.previewIcon;
        iconImage.color = action.previewIconColor;
        valueText.text = (action.value + enemy.power).ToString();

        transform.DOPunchScale(transform.lossyScale + (Vector3.one*0.6f), 0.5f);
    }
}
