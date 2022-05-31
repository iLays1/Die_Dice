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
        GainTempAttackPower,
        GainTempBlockPower,
        SkipEnemyAction, //CombatManger = singleton ez (NI)
        DiscardDice,
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
                GameDataSystem.Instance.tempAttackPower++;
                PlayerDiceManager.OnUpdateDiceValues.Invoke();
                break;
            case Ability.GainTempBlockPower:
                GameDataSystem.Instance.tempBlockPower++;
                PlayerDiceManager.OnUpdateDiceValues.Invoke();
                break;
        }
    }

    public void DiscardDice()
    {
        PlayerDiceManager.Instance.DiscardThenDraw();
    }
}
