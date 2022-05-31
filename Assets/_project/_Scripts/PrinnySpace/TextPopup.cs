using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public static TextPopup Create(string text, Color color, Vector3 position)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("TextPopup"));
        TextPopup popup = go.GetComponent<TextPopup>();

        go.transform.position = position;
        popup.text.color = color;
        popup.text.text = text;

        return popup;
    }

    public TextMeshPro text;

    private void Start()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(transform.position.y + 1f, 1.5f).SetEase(Ease.OutExpo));
        s.Join(text.DOFade(0f, 1.3f).SetEase(Ease.InExpo));
        s.AppendCallback(() => Destroy(gameObject));
    }
}
