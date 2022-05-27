using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnHoverExpand : MonoBehaviour
{
    Vector3 oScale;
    Vector3 targetScale;

    public bool clickable;
    public float factor = 1f;
    public float clickFactor = 1f;
    public float time = 0.2f;

    private void Awake()
    {
        oScale = transform.localScale;
        targetScale = oScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, time);
    }

    private void OnMouseEnter()
    {
        targetScale = oScale * factor;
    }
    private void OnMouseOver()
    {
        if (clickable && Input.GetMouseButtonDown(0))
            targetScale = oScale * factor * clickFactor;

        if (clickable && Input.GetMouseButtonUp(0))
            targetScale = oScale * factor;
    }
    private void OnMouseExit()
    {
        targetScale = oScale;
    }
}
