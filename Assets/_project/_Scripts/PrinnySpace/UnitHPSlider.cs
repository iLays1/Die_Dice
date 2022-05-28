using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHPSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hpText;

    public TextMeshProUGUI blockText;
    public Image blockImage;

    public CombatUnit unit;

    public void Awake()
    {
        unit.OnValuesChange.AddListener(UpdateUI);
        UpdateUI();
    }

    public void UpdateUI()
    {
        slider.maxValue = unit.maxHP;
        slider.DOKill();
        slider.DOValue(unit.HP, 0.3f).SetEase(Ease.OutBounce);

        if (unit.block <= 0)
        {
            blockImage.gameObject.SetActive(false);
        }
        else
        {
            blockImage.gameObject.SetActive(true);
            blockText.text = unit.block.ToString();
        }

        hpText.text = unit.HP.ToString();
    }
}
