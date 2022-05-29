using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : Singleton<DiceManager>
{
    public static UnityEvent OnUpdateDiceValues = new UnityEvent();

    public Transform[] dicePositions;
    public GameObject diePrefab;
    public List<Die> dice;
    public List<DieData> diceInBag;
    public List<DieData> diceInDiscard;

    [Space]
    public Transform dieSpawnPos;
    public Transform dieDiscardPos;
    public TextMeshProUGUI dieCountText;
    public TextMeshProUGUI discardCountText;

    [Space]
    public TextMeshProUGUI skullCountText;
    public TextMeshProUGUI attackCountText;
    public TextMeshProUGUI blockCountText;

    public int skullsRolled;
    public int attacksRolled;
    public int blocksRolled;

    [Space]
    public AnimationCurve jumpEase;
    const float JUMPTIME = 0.8f;
    const float JUMPFORCE = 4f;

    protected override void Awake()
    {
        base.Awake();
        UpdateUI();

        CombatManager.OnTurnStart.AddListener(() =>
        {
            attacksRolled = 0;
            blocksRolled = 0;
            skullsRolled = 0;

            UpdateUI();

            DrawDice();
        });
        CombatManager.OnTurnEnd.AddListener(() => DiscardDice());

        foreach (var d in PlayerDataSystem.Instance.dice)
        {
            diceInBag.Add(d);
        }
        CustomUtility.ShuffleList(ref diceInBag);
    }

    public void DrawDice() => StartCoroutine(DrawDiceCoroutine());
    IEnumerator DrawDiceCoroutine()
    {
        CombatManager.Instance.state = CombatState.Animation;
        for (int i = 0; i < dicePositions.Length; i++)
        {
            if (diceInBag.Count <= 0)
            {
                yield return new WaitForSeconds(1f);
                foreach (var discardedDie in diceInDiscard)
                {
                    diceInBag.Add(discardedDie);
                }
                diceInDiscard.Clear();

                CustomUtility.ShuffleList(ref diceInBag);
            }
            if (diceInBag.Count <= 0)
                break;

            var dieGo = Instantiate(diePrefab, dicePositions[i]);
            var die = dieGo.GetComponentInChildren<Die>();
            dieGo.transform.position = dieSpawnPos.position;

            die.data = diceInBag[0];
            diceInBag.RemoveAt(0);
            UpdateDiceCounts();

            dice.Add(die);

            dieGo.transform.DOJump(dicePositions[i].position, JUMPFORCE, 1, JUMPTIME * Random.Range(0.8f,1.2f)).SetEase(jumpEase);
        }

        CombatManager.Instance.state = CombatState.PlayerTurn;
    }

    public void DiscardDice() => StartCoroutine(DiscardDiceCoroutine());
    IEnumerator DiscardDiceCoroutine()
    {
        CombatManager.Instance.state = CombatState.Animation;
        foreach (var die in dice.ToArray())
        {
            Sequence s = DOTween.Sequence();

            dice.Remove(die);
            diceInDiscard.Add(die.data);
            UpdateDiceCounts();

            s.Append(die.transform.DOJump(dieDiscardPos.position, JUMPFORCE, 1, JUMPTIME * Random.Range(0.8f, 1.2f)).SetEase(jumpEase));
            s.AppendCallback(() => Destroy(die.transform.parent.gameObject));
        }

        yield return null;
    }

    public void RollDice()
    {
        if (CombatManager.Instance.state != CombatState.PlayerTurn)
            return;

        StartCoroutine(RollDiceCoroutine());
    }
    public IEnumerator RollDiceCoroutine()
    {
        CombatManager.Instance.state = CombatState.Rolling;

        int skulls = 0;
        int attacks = 0;
        int blocks = 0;

        foreach(var die in dice)
        {
            die.RollRandom();
            
            var face = die.currentFace;
            switch (face.type)
            {
                case FaceType.Attack:
                    attacks += face.value;
                    break;
                case FaceType.Block:
                    blocks += face.value;
                    break;
                case FaceType.Skull:
                    skulls += face.value;
                    break;
            }
        }

        yield return new WaitForSeconds(2f);

        attacksRolled += attacks;
        blocksRolled += blocks;
        skullsRolled += skulls;
        OnUpdateDiceValues.Invoke();
        UpdateUI();
        
        yield return new WaitForSeconds(1f);

        DiscardDice();

        if (skullsRolled >= 3)
        {
            attacksRolled = 0;
            blocksRolled = 0;

            OnUpdateDiceValues.Invoke();
            UpdateUI();

            CombatManager.Instance.state = CombatState.PlayerTurn;
            CombatManager.Instance.EndTurn();
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            DrawDice();
        }
    }

    public void DiscardThenDraw()
    {
        StartCoroutine(DiscardThenDrawCoroutine());
    }
    IEnumerator DiscardThenDrawCoroutine()
    {
        CombatManager.Instance.state = CombatState.Animation;
        yield return new WaitForSeconds(0.05f);
        DiscardDice();
        yield return new WaitForSeconds(0.3f);
        DrawDice();
        CombatManager.Instance.state = CombatState.PlayerTurn;
    }

    public void UpdateUI()
    {
        float punchFactor = 40f;
        float punchTime = 0.5f;
        
        attackCountText.text = $": {attacksRolled}<size=45>x{PlayerDataSystem.Instance.attackPower}";
        attackCountText.transform.parent.DOComplete();
        attackCountText.transform.parent.DOPunchScale(attackCountText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);

        blockCountText.text = $": {blocksRolled}<size=45>x{PlayerDataSystem.Instance.blockPower}";
        blockCountText.transform.parent.DOComplete();
        blockCountText.transform.parent.DOPunchScale(blockCountText.transform.parent.lossyScale * punchFactor, punchTime, 0, 0.5f);
    }

    void UpdateDiceCounts()
    {
        dieCountText.text = $"Dice\n{diceInBag.Count}";
        discardCountText.text = $"Discard\n{diceInDiscard.Count}";
    }
}
