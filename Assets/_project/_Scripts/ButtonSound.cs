using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnClicked()
    {
        AudioManager.Instance.PlayAtRandomPitch("ClickBeep", 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayAtRandomPitch("MenuBeep", 0.3f);
    }
}
