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
    public TextMeshProUGUI useCountMesh;
    public TextMeshProUGUI descriptionMesh;
    public GameObject descriptionObject;
    public Image image;

    bool descShown = true;

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

        descShown = false;
        descriptionObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (CombatManager.Instance.state != CombatState.PlayerTurn)
            return;
        
        descShown = true;
        descriptionObject.SetActive(descShown);
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
        descShown = false;
        descriptionObject.SetActive(descShown);
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
