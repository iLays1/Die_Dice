﻿using DG.Tweening;
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
    }

    public void GainArmour(int amount)
    {
        block += amount;
        TextPopup.Create($"+{amount}", Color.cyan, textPopupLocation.position);
        
        OnValuesChange.Invoke();
    }
    public void TakeDamage(int damage)
    {
        block -= damage;

        if(block < 0)
        {
            HP -= Mathf.Abs(block);
            block = 0;
        }

        TextPopup.Create($"-{damage}", Color.red, textPopupLocation.position);

        transform.DOComplete();
        transform.DOPunchPosition(Vector3.down * 0.4f, 0.8f).SetEase(Ease.OutElastic);

        if(HP <= 0)
        {
            HP = 0;
            Death();
        }

        OnValuesChange.Invoke();
    }

    public void Death()
    {
        OnUnitDeath.Invoke();
    }
}