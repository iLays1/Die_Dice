using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatUnit : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnValuesChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUnitDeath = new UnityEvent();

    public Transform unitCanvas;
    public Transform textPopupLocation;

    public int HP;
    public int maxHP;
    public int block;

    private void Start()
    {
        HP = maxHP;
        OnValuesChange.Invoke();

        CombatManager.OnTurnStart.AddListener(() => {
            block = 0;
            OnValuesChange.Invoke();
        });

        unitCanvas.SetParent(null);
        unitCanvas.position = transform.position + (-Vector3.forward*5);

        WinLoseManager.OnCombatEnd.AddListener(() => Destroy(unitCanvas.gameObject));
    }

    public void GainArmour(int amount)
    {
        block += amount;
        TextPopup.Create($"{amount}", Color.cyan, textPopupLocation.position);

        AudioManager.Instance.PlayAtRandomPitch("Block", 0.2f);
        OnValuesChange.Invoke();
    }
    public void TakeDamage(int damage)
    {
        var hpBeforeHit = HP;

        block -= damage;

        if(block < 0)
        {
            HP -= Mathf.Abs(block);
            block = 0;
        }

        Color hitColor = Color.grey;
        transform.DOComplete();
        transform.DOPunchPosition(Vector3.down * 0.2f, 0.8f).SetEase(Ease.OutElastic);

        AudioManager.Instance.PlayAtRandomPitch("Hit", 0.2f);
        if (hpBeforeHit > HP)
        {
            hitColor = Color.red;
            transform.DOPunchScale((Vector3.back + Vector3.right) * 0.2f, 0.4f, 0);
        }

        TextPopup.Create($"-{damage}", hitColor, textPopupLocation.position);

        if (HP <= 0)
        {
            HP = 0;
            Death();
        }

        OnValuesChange.Invoke();
    }

    public void Death()
    {
        transform.DOMoveY(-15, 2f).SetEase(Ease.InBack);
        OnUnitDeath.Invoke();
    }
}