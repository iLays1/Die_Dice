using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieData_", menuName = "Scriptable Objects/new Die Type")]
public class DieData : ScriptableObject
{
    public DiceFaceData[] faces;
    public Color color;

    public void Load(Die die)
    {
        int c = 0;
        foreach (var face in die.faceRenderers)
        {
            face.material.mainTexture = faces[c].faceSprite.texture;
            face.material.color = color;
            c++;
        }
    }
}
