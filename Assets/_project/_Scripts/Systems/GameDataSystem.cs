using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSystem : SingletonPersistent<GameDataSystem>
{
    public List<DiceData> dicePool;
    public List<DiceData> dice;
    public List<PlayerAbility> abilities;
    [Space]
    public List<SpouseData> SpousePool;

    [HideInInspector] public SpouseData lastSpouse;
    [HideInInspector] public EncounterData nextEncounter;

    public int baseAttackPower = 1;
    public int baseBlockPower = 1;

    [HideInInspector] public int tempAttackPower = 0;
    [HideInInspector] public int tempBlockPower = 0;

    public int attackPower { get { return baseAttackPower + tempAttackPower; } }
    public int blockPower { get { return baseBlockPower + tempBlockPower; } }

    protected override void Awake()
    {
        base.Awake();

        DOTween.SetTweensCapacity(500, 100);

        CombatManager.OnTurnStart.AddListener(() =>
        {
            tempAttackPower = 0;
            tempBlockPower = 0;
        });
    }
}
