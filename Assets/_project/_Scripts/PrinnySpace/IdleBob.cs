using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBob : MonoBehaviour
{
    public float amount = 0.8f;
    public float speed = 0.8f;
    Vector3 opos;
    private void Awake()
    {
        opos = transform.localPosition;

        amount *= Random.Range(0.6f, 1.4f);

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(opos + (Vector3.forward * amount), speed));
        s.Append(transform.DOLocalMove(opos, speed));
        s.SetLoops(-1);
    }
}
