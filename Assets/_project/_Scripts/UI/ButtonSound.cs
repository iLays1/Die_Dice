using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    public void OnClicked()
    {
        AudioSystem.Instance.Play("ClickBeep");
    }
}
