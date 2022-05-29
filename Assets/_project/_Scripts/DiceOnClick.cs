using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceOnClick : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    public Die die;

    private void Awake()
    {
        die = GetComponentInChildren<Die>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick.Invoke();
    }
}
