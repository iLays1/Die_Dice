using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotate : MonoBehaviour
{
    public float speed = 0.8f;
    
    private void Awake()
    {
        Sequence s = DOTween.Sequence();
        transform.DOLocalRotate(new Vector3(45, 360, 45), speed, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
