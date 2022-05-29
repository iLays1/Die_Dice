using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullHP : MonoBehaviour
{
    public SkullHPImage[] images;
    int index = 0;
    int dicePoped = 0;

    private void Awake()
    {
        CombatManager.OnTurnStart.AddListener(ShowAll);
        DiceManager.OnUpdateDiceValues.AddListener(CheckValuesForPop);
    }

    public void ShowAll()
    {
        dicePoped = 0;
        index = 0;
        foreach (var i in images)
        {
            if (i.hidden)
                i.Show();
        }
    }

    void CheckValuesForPop()
    {
        var skullsRolled = DiceManager.Instance.skullsRolled;

        while(skullsRolled > dicePoped)
        {
            Pop();
            dicePoped++;
        }
    }

    public void Pop()
    {
        if (index >= images.Length)
            return;

        
        images[index].Hide();
        index++;
    }
}
