using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogueSystem : SingletonPersistent<DialogueSystem>
{
    public TextMeshProUGUI textMesh;

    protected override void Awake()
    {
        base.Awake();
        
        textMesh.text = "";
    }

    public void PlayString(DialogueString dstring)
    {
        StopAllCoroutines();
        textMesh.DOKill();
        textMesh.text = "";
        StartCoroutine(StringCoroutine(dstring));
    }
    IEnumerator StringCoroutine(DialogueString dstring)
    {
        foreach(var sentence in dstring.sentences)
        {
            textMesh.text = "";
            StartCoroutine(PlayTextCoroutine(sentence.text, sentence.speed, sentence.holdTime));
            
            yield return new WaitForSeconds(sentence.speed + sentence.holdTime + 0.3f);
        }
    }
    public void PlayText(string text, float speed, float holdTime = 1f)
    {
        StopAllCoroutines();
        textMesh.DOKill();
        textMesh.text = "";
        StartCoroutine(PlayTextCoroutine(text,speed, holdTime));
    }
    IEnumerator PlayTextCoroutine(string text, float speed, float holdTime)
    {
        StartCoroutine(DisplayText(text,speed));
        yield return new WaitForSeconds(speed + holdTime);
        textMesh.text = "";
    }
    IEnumerator DisplayText(string text, float speed)
    {
        float finalSpeed = speed / text.Length;
        string result = "";
        var charArray = text.ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            result += charArray[i];
            if(charArray[i] != ' ')
                textMesh.text = result;

            AudioManager.Instance.PlayAtRandomPitch("Heh", 0.5f);
            yield return new WaitForSeconds(finalSpeed);
        }
    }
}
