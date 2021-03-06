using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullHP : MonoBehaviour
{
    [SerializeField] SkullHPImage[] images;
    int index = 0;
    int dicePoped = 0;

    private void Awake()
    {
        CombatManager.OnTurnStart.AddListener(ShowAll);
        CombatManager.OnTurnEnd.AddListener(HideAll);
        PlayerDiceManager.OnUpdateDiceValues.AddListener(CheckValuesForPop);
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
    public void HideAll()
    {
        foreach (var i in images)
        {
            if (!i.hidden)
                i.Hide();
        }
    }

    void CheckValuesForPop()
    {
        var skullsRolled = PlayerDiceManager.Instance.skullsRolled;

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
