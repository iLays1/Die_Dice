using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FaceType
{
    Blank,
    Skull,
    Attack,
    Block
}

[CreateAssetMenu(fileName = "DiceFace_", menuName = "Scriptable Objects/new Dice Face")]
public class DiceFaceData : ScriptableObject
{
    public Sprite faceSprite;
    public FaceType type;
    public int value;
}
