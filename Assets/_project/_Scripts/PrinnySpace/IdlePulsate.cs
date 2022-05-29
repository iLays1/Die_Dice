using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePulsate : MonoBehaviour
{
    public float scale = 0.95f;
    public float speed = 0.8f;
    Vector3 oScale;
    private void Awake()
    {
        oScale = transform.localScale;

        Sequence s = DOTween.Sequence();
        transform.DOScale(oScale * scale, speed).SetLoops(-1, LoopType.Yoyo);
    }
}
