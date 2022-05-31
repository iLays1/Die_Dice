using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpouseSelection : MonoBehaviour
{
    [SerializeField] DisplayDieCanvasPos[] heroDisplayDice;
    [SerializeField] DiceData[] consistentDice;
    [SerializeField] SpouseUiOption[] spouseButtons;
    bool selected = false;

    private void Awake()
    {
        var sPool = GameDataSystem.Instance.SpousePool;
        int[] intarray = CustomUtility.GetRandomIntArray(0, sPool.Count);

        for (int i = 0; i < spouseButtons.Length; i++)
        {
            spouseButtons[i].SetSpouse(sPool[intarray[i]]);
            var s = spouseButtons[i].spouse;
            spouseButtons[i].OnMarryButtonEvent.AddListener(() => SpouseSelected(s));
        }

        var heroDice = consistentDice;
        if (GameDataSystem.Instance.lastSpouse != null)
            heroDice = GameDataSystem.Instance.lastSpouse.dice;

        for (int i = 0; i < heroDisplayDice.Length; i++)
            heroDisplayDice[i].data = heroDice[i];
    }

    public void SpouseSelected(SpouseData spouse)
    {
        if (selected) return;
        selected = true;

        var heroDice = consistentDice;
        if(GameDataSystem.Instance.lastSpouse != null)
            heroDice = GameDataSystem.Instance.lastSpouse.dice;

        var dice = GameDataSystem.Instance.dice;
        dice.Clear();

        for (int i = 0; i < spouse.dice.Length; i++)
            dice.Add(spouse.dice[i]);

        for (int i = 0; i < heroDice.Length; i++)
            dice.Add(heroDice[i]);
        
        GameDataSystem.Instance.lastSpouse = spouse;
        SceneSystem.Instance.LoadScene(0);
    }
}
