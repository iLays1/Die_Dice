using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter_", menuName = "Scriptable Objects/new Encounter")]
public class EncounterData : ScriptableObject
{
    public EnemyBehavior enemyprefab;
}
