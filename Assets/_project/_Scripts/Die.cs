using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Die : MonoBehaviour
{
    public DiceFaceData currentFace { get { return data.faces[face]; } }
    
    public int face = -1;
    public MeshRenderer[] faceRenderers;
    public DieData data;

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
            data.Load(this);
    }

    public void RollRandom()
    {
        face = Random.Range(0, faceDirections.Length);
        RollToFace(face);
    }
    public void NextFace()
    {
        face++;
        if (face >= faceDirections.Length)
            face = 0;
        RollToFace(face);
    }
    public void RollToFace(int i)
    {
        var newEnd = faceDirections[i];
        var dumb = new Vector3(
            Random.Range(200,300), 
            Random.Range(200, 300), 
            Random.Range(200, 300));

        Sequence s = DOTween.Sequence();

        transform.DOKill();
        s.Append(transform.DORotate((newEnd + dumb) * 3f, 1.2f*Random.Range(0.8f,1.2f), RotateMode.FastBeyond360).SetEase(Ease.InBack));
        s.Append(transform.DORotate(newEnd, 1f, RotateMode.FastBeyond360).SetEase(Ease.OutElastic));
    }
}
