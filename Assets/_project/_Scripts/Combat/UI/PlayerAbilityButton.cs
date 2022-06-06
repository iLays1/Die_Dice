using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityButton : MonoBehaviour
{
    public PlayerAbility ability;
    public int uses;

    [Space]
    [SerializeField] TextMeshProUGUI useCountMesh;
    [SerializeField] TextMeshProUGUI descriptionMesh;
    [SerializeField] GameObject descriptionObject;
    [SerializeField] ParticleSystem useParticle;
    [SerializeField] Image image;
    
    private void Start()
    {
        if (ability == null)
        {
            Destroy(gameObject);
            return;
        }

        uses = ability.uses;
        image.sprite = ability.icon;
        descriptionMesh.text = ability.description;
        UpdateVisuals();
        
        descriptionObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (CombatManager.Instance.state != CombatState.PlayerTurn)
            return;
        
        descriptionObject.SetActive(true);
    }
    private void OnMouseOver()
    {
        if (CombatManager.Instance.state != CombatState.PlayerTurn)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            HideDesc();

            if (uses > 0)
            {
                useParticle.Play();
                ability.Use();
                uses--;
                UpdateVisuals();
            }
            else
            {
                Debug.Log("no uses left");
            }
        }
    }
    private void OnMouseExit()
    {
        HideDesc();
    }
    void HideDesc()
    {
        descriptionObject.SetActive(false);
    }

    public void UpdateVisuals()
    {
        useCountMesh.text = uses.ToString();
        if(uses <= 0)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.white;
        }

        transform.DOComplete();
        transform.DOPunchScale(transform.lossyScale + (Vector3.one * 0.1f), 0.3f);
    }
}
