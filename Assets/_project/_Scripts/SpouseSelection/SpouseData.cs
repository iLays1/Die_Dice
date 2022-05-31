using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Spouse_", menuName = "Scriptable Objects/new Spouse")]
public class SpouseData : ScriptableObject
{
    public DiceData[] dice;
}