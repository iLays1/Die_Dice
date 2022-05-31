using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] PlayerAbilityButton[] buttons;

    private void Start()
    {
        var abs = GameDataSystem.Instance.abilities;
        for (int i = 0; i < abs.Count; i++)
        {
            buttons[i].ability = abs[i];
        }
    }
}
