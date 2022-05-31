using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullHPImage : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    public bool hidden = false;

    Vector3 oScale;

    private void Awake()
    {
        oScale = transform.localScale;
    }

    public void Hide()
    {
        hidden = true;
        particles.Play();
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        AudioSystem.Instance.Play("SkullPoof");
    }
    public void Show()
    {
        hidden = false;
        transform.DOScale(oScale, 0.8f).SetEase(Ease.OutBack);
    }
}
