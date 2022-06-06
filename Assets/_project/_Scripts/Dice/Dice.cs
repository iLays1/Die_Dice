using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    public DiceFaceData currentFace { get { return data.faces[face]; } }
    
    [HideInInspector] public int face = -1;
    [HideInInspector] public DiceData data;

    [SerializeField] MeshRenderer[] faceRenderers;

    private Vector3[] faceDirections = new Vector3[]
    {
        new Vector3(90,-270,-90),
        new Vector3(0,-270,0),
        new Vector3(0,-90,0),
        new Vector3(90,-270,-270),
        new Vector3(0,-180,0),
        new Vector3(0,0,0)
    };

    private void Start()
    {
        if(data != null)
            Load(data);
    }

    public void Load(DiceData dataToLoad)
    {
        int c = 0;
        foreach (var face in faceRenderers)
        {
            face.material.mainTexture = dataToLoad.faces[c].faceSprite.texture;
            face.material.color = dataToLoad.color;
            c++;
        }
    }

    public void RollRandom()
    {
        face = Random.Range(0, faceDirections.Length);
        RollToFace(face);
    }
    public void RollToFace(int i)
    {
        var newEnd = faceDirections[i];
        var dumb = new Vector3(
            Random.Range(180,270), 
            Random.Range(180, 270), 
            Random.Range(180, 270));

        Sequence s = DOTween.Sequence();

        Invoke("PlayRollNoise", 0.5f);
        transform.DOKill();
        s.Append(transform.DORotate((newEnd + dumb) * 5f, 1f*Random.Range(1f,1.3f), RotateMode.FastBeyond360).SetEase(Ease.InBack));
        s.Append(transform.DORotate(newEnd, 1f, RotateMode.FastBeyond360).SetEase(Ease.OutQuint));
    }
    void PlayRollNoise()
    {
        AudioSystem.Instance.Play("DieRoll");
    }
}
