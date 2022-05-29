using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbility_", menuName = "Scriptable Objects/new Player Ability")]
public class PlayerAbility : ScriptableObject
{
    public string abilityName;
    [TextArea]
    public string description;
    public Sprite icon;
    public int uses;

    public Ability function;

    public enum Ability
    {
        GainTempAttackPower, //PlayerManager = singleton ez (NI)
        GainTempBlockPower, //PlayerManager = singleton ez (NI)
        SkipEnemyAction, //CombatManger = singleton ez (NI)
        DiscardDice, //DiceManger = singleton ez (NI)
        DoublesBlockLoseAllAttackRolls, //DiceManger = singleton ez (NI)
        NextRollTripled, //DiceManger = singleton ez (NI)
        AllAttacksBecomeBlocks, //DiceManger = singleton ez (NI)
        HealBlocksRolled //PlayerUnit (NI)
    }

    public void Use()
    {
        switch(function)
        {
            case Ability.DiscardDice:
                DiscardDice();
                break;
            case Ability.GainTempAttackPower:
                PlayerDataSystem.Instance.tempAttackPower++;
                DiceManager.Instance.UpdateUI();
                break;
            case Ability.GainTempBlockPower:
                PlayerDataSystem.Instance.tempBlockPower++;
                DiceManager.Instance.UpdateUI();
                break;
        }
    }

    public void DiscardDice()
    {
        DiceManager.Instance.DiscardThenDraw();
    }
}
