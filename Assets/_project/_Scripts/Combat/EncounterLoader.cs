using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterLoader : MonoBehaviour
{
    [SerializeField] Transform enemyPosition;
    public CombatUnit enemy;

    public void Awake()
    {
        var encounter = GameDataSystem.Instance.nextEncounter;
        var enemyGo = Instantiate(encounter.enemyprefab);
        enemyGo.transform.position = enemyPosition.position;
        enemy = enemyGo.GetComponent<CombatUnit>();
    }
}
