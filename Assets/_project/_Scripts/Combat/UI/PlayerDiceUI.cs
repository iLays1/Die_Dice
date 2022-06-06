using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDiceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dieCountText;
    [SerializeField] TextMeshProUGUI discardCountText;
    
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI blockText;

    [SerializeField] TextMeshProUGUI critText;

    int lastAttackPower = 0;
    int lastBlockPower = 0;
    int lastAttackRoll = 0;
    int lastBlockRoll = 0;

    private void Awake()
    {
        PlayerDiceManager.OnUpdateDiceValues.AddListener(UpdateUI);
    }

    public void UpdateUI()
    {
        PlayerDiceManager diceManager = PlayerDiceManager.Instance;

        float punchFactor = 20f;
        float punchTime = 0.5f;
        
        attackText.text = $":{diceManager.totalAttacksRolled}<size=30>x{GameDataSystem.Instance.attackPower}";
        if (lastAttackPower != GameDataSystem.Instance.attackPower ||
            lastAttackRoll != diceManager.totalAttacksRolled)
        {
            attackText.transform.parent.DOComplete();
            attackText.transform.parent.DOPunchScale(attackText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);
        }
        
        blockText.text = $":{diceManager.totalBlocksRolled}<size=30>x{GameDataSystem.Instance.blockPower}";
        if (lastBlockPower != GameDataSystem.Instance.blockPower || 
            lastBlockRoll != diceManager.totalBlocksRolled)
        {
            blockText.transform.parent.DOComplete();
            blockText.transform.parent.DOPunchScale(blockText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);
        }

        dieCountText.text = $"Dice\n{diceManager.diceInBag.Count}";
        discardCountText.text = $"Discard\n{diceManager.diceInDiscard.Count}";

        lastAttackPower = GameDataSystem.Instance.attackPower;
        lastBlockPower = GameDataSystem.Instance.blockPower;
        lastAttackRoll = diceManager.totalAttacksRolled;
        lastBlockRoll = diceManager.totalBlocksRolled;

        critText.text = (diceManager.critStored) ? "x2" : "";
    }
}
