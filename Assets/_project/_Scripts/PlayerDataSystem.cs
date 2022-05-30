using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSystem : SingletonPersistent<PlayerDataSystem>
{
    public List<DieData> dicePool;
    public List<DieData> dice;
    public List<PlayerAbility> abilities;
    [Space]
    public List<SpouseData> SpousePool;
    public SpouseData lastSpouse;
    [Space]
    public EncounterData nextEncounter;

    public int baseAttackPower = 1;
    public int baseBlockPower = 1;

    public int tempAttackPower = 0;
    public int tempBlockPower = 0;

    public int attackPower { get { return baseAttackPower + tempAttackPower; } }
    public int blockPower { get { return baseBlockPower + tempBlockPower; } }

    protected override void Awake()
    {
        base.Awake();

        CombatManager.OnTurnStart.AddListener(() =>
        {
            tempAttackPower = 0;
            tempBlockPower = 0;
        });
    }
}
