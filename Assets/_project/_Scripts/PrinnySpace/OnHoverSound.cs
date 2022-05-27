using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverSound : MonoBehaviour
{
    public AudioSource hoverSource;
    public bool clickable;
    public AudioSource clickSource;
    private void OnMouseEnter()
    {
        hoverSource.Play();
    }
    private void OnMouseOver()
    {
        if (clickable && Input.GetMouseButtonDown(0))
            clickSource.Play();
    }
}
