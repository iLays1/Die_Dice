using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePulsate : MonoBehaviour
{
    public float speed = 0.8f;
    Vector3 oScale;
    private void Awake()
    {
        oScale = transform.localScale;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(oScale * 0.95f, speed));
        s.Append(transform.DOScale(oScale, speed));
        s.SetLoops(-1);
    }
}
