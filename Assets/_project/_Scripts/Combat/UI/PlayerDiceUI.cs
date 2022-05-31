using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDiceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dieCountText;
    [SerializeField] TextMeshProUGUI discardCountText;
    [SerializeField] TextMeshProUGUI attackCountText;
    [SerializeField] TextMeshProUGUI blockCountText;

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

        attackCountText.text = $": {diceManager.attacksRolled}<size=45>x{GameDataSystem.Instance.attackPower}";
        if (lastAttackPower != GameDataSystem.Instance.attackPower ||
            lastAttackRoll != diceManager.attacksRolled)
        {
            attackCountText.transform.parent.DOComplete();
            attackCountText.transform.parent.DOPunchScale(attackCountText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);
        }

        blockCountText.text = $": {diceManager.blocksRolled}<size=45>x{GameDataSystem.Instance.blockPower}";
        if (lastBlockPower != GameDataSystem.Instance.blockPower || 
            lastBlockRoll != diceManager.blocksRolled)
        {
            blockCountText.transform.parent.DOComplete();
            blockCountText.transform.parent.DOPunchScale(blockCountText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);
        }

        dieCountText.text = $"Dice\n{diceManager.diceInBag.Count}";
        discardCountText.text = $"Discard\n{diceManager.diceInDiscard.Count}";

        lastAttackPower = GameDataSystem.Instance.attackPower;
        lastBlockPower = GameDataSystem.Instance.blockPower;
        lastAttackRoll = diceManager.attacksRolled;
        lastBlockRoll = diceManager.blocksRolled;
    }
}
