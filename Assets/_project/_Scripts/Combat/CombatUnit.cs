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

    [SerializeField] Transform unitCanvas;
    [SerializeField] Transform textPopupLocation;
    [SerializeField] ParticleSystem hitParticle;

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

        AudioSystem.Instance.Play("Block");
        OnValuesChange.Invoke();
    }
    public void TakeDamage(int damage)
    {
        block -= damage;
        var trueDamage = 0;

        if(block < 0)
        {
            trueDamage = Mathf.Abs(block);
            HP -= trueDamage;
            block = 0;
        }

        Color hitColor = Color.grey;
        transform.DOComplete();
        transform.DOPunchPosition(Vector3.down * 0.2f, 0.8f).SetEase(Ease.OutElastic);

        AudioSystem.Instance.Play("Hit");
        if (trueDamage > 0)
        {
            hitColor = Color.red;
            hitParticle.Play();
            transform.DOPunchScale((Vector3.back + Vector3.right) * 0.2f, 0.4f, 0);
        }
        
        TextPopup.Create($"-{trueDamage}", hitColor, textPopupLocation.position);

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