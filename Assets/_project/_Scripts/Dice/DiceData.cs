using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieData_", menuName = "Scriptable Objects/new Die Type")]
public class DiceData : ScriptableObject
{
    public DiceFaceData[] faces;
    public Color color;
}
