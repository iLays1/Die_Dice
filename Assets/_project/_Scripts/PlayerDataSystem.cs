using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSystem : SingletonPersistent<PlayerDataSystem>
{
    public List<DieData> dice;

    public int playerAttackPower = 1;
    public int playerBlockPower = 1;
}
