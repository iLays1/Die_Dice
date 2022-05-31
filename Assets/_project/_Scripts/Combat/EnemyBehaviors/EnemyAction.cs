using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAction
{
    EnemyBehavior userBehavior;

    public Sprite previewIcon;
    public Color previewIconColor;
    public int value = 3;
    public Type actionType;

    public enum Type
    {
        Attack,
        Block
    }

    public void Use(CombatUnit playerUnit, CombatUnit user)
    {
        if (userBehavior == null)
            userBehavior = user.GetComponent<EnemyBehavior>();

        switch (actionType)
        {
            case Type.Attack:
                userBehavior.Attack(value, playerUnit);
                break;
            case Type.Block:
                user.GainArmour(value);
                break;
        }
    }
}
