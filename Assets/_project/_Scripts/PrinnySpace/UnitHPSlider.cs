using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHPSlider : MonoBehaviour
{
    public CombatUnit unit;

    [Space]
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI hpText;

    [SerializeField] TextMeshProUGUI blockText;
    [SerializeField] Image blockImage;
    [SerializeField] Image hpBarImage;

    [SerializeField] Gradient colorGradient;
    [SerializeField] Color blockColor;

    [SerializeField] Slider delaySlider;

    public void Awake()
    {
        unit.OnValuesChange.AddListener(UpdateUI);
        UpdateUI();
    }

    public void UpdateUI()
    {
        Sequence s = DOTween.Sequence();

        slider.maxValue = unit.maxHP;
        delaySlider.maxValue = unit.maxHP;

        slider.DOKill();

        s.Append(slider.DOValue(unit.HP, 0.2f).SetEase(Ease.OutBack));
        s.AppendInterval(0.4f);
        s.Append(delaySlider.DOValue(unit.HP, 0.6f));
        
        if (unit.block <= 0)
        {
            var gradient = colorGradient.Evaluate((float)unit.HP / (float)unit.maxHP);
            hpBarImage.DOColor(gradient, 0.2f);

            blockImage.gameObject.SetActive(false);
        }
        else
        {
            blockImage.gameObject.SetActive(true);
            hpBarImage.DOColor(blockColor, 0.2f);
            blockText.text = $"{unit.block}";
        }

        hpText.text = $"{unit.HP}/{unit.maxHP}";
    }
}
