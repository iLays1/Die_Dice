using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterLoader : MonoBehaviour
{
    public Transform enemyPosition;
    public CombatUnit enemy;

    public void Awake()
    {
        var encounter = PlayerDataSystem.Instance.nextEncounter;
        var enemyGo = Instantiate(encounter.enemyprefab);
        enemyGo.transform.position = enemyPosition.position;
        enemy = enemyGo.GetComponent<CombatUnit>();

        if(encounter.dialogue != null)
            DialogueSystem.Instance.PlayString(encounter.dialogue);

        Debug.Log(encounter.name + " has begun");
    }
}
