using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDiceManager : Singleton<PlayerDiceManager>
{
    public static UnityEvent OnUpdateDiceValues = new UnityEvent();

    [SerializeField] Transform[] dicePositions;
    [SerializeField] GameObject diePrefab;

    [Space]
    [SerializeField] Transform dieSpawnPos;
    [SerializeField] Transform dieDiscardPos;
    
    [Space]
    public AnimationCurve jumpEase;

    [HideInInspector] public List<Dice> dice;
    [HideInInspector] public List<DiceData> diceInBag;
    [HideInInspector] public List<DiceData> diceInDiscard;
    [HideInInspector] public int skullsRolled;
    [HideInInspector] public int totalAttacksRolled;
    [HideInInspector] public int totalBlocksRolled;
    [HideInInspector] public bool critStored;

    const float JUMPTIME = 0.8f;
    const float JUMPFORCE = 4f;
    

    protected override void Awake()
    {
        base.Awake();

        OnUpdateDiceValues.Invoke();

        CombatManager.OnTurnStart.AddListener(() =>
        {
            totalAttacksRolled = 0;
            totalBlocksRolled = 0;
            skullsRolled = 0;

            OnUpdateDiceValues.Invoke();

            DrawDice();
        });
        CombatManager.OnTurnEnd.AddListener(() => DiscardDice());

        foreach (var d in GameDataSystem.Instance.dice)
        {
            diceInBag.Add(d);
        }
        CustomUtility.ShuffleList(ref diceInBag);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
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

                AudioSystem.Instance.Play("Shuffle");
                CustomUtility.ShuffleList(ref diceInBag);
            }
            if (diceInBag.Count <= 0)
                break;

            var dieGo = Instantiate(diePrefab, dicePositions[i]);
            var die = dieGo.GetComponent<Dice>();
            dieGo.transform.position = dieSpawnPos.position;

            die.data = diceInBag[0];
            diceInBag.RemoveAt(0);
            OnUpdateDiceValues.Invoke();

            dice.Add(die);

            dieGo.transform.DOJump(dicePositions[i].position, JUMPFORCE, 1, JUMPTIME * Random.Range(1f,1.4f)).SetEase(jumpEase);
        }

        yield return new WaitForSeconds(JUMPTIME);
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
            OnUpdateDiceValues.Invoke();

            s.Append(die.transform.DOJump(dieDiscardPos.position, JUMPFORCE, 1, JUMPTIME * Random.Range(0.8f, 1.2f)).SetEase(jumpEase));
            s.AppendCallback(() => Destroy(die.gameObject));
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
        List<DiceFaceData> facesRolled = new List<DiceFaceData>();
        foreach(var die in dice)
        {
            die.RollRandom();
            facesRolled.Add(die.currentFace);
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

        bool allMatching = true;
        foreach(var d in facesRolled)
        {
            if (d != facesRolled[0])
            {
                allMatching = false;
            }
        }

        yield return new WaitForSeconds(2f);

        if (critStored)
        {
            attacks *= 2;
            blocks *= 2;
            critStored = false;
        }

        totalAttacksRolled += attacks;
        totalBlocksRolled += blocks;
        
        if(!allMatching)
            skullsRolled += skulls;

        OnUpdateDiceValues.Invoke();

        if (allMatching)
        {
            TextPopup.Create("Matched 3", Color.yellow, transform.position);
            critStored = true;
            Debug.Log($"CRIT");
        }

        yield return new WaitForSeconds(1f);

        DiscardDice();

        if (skullsRolled >= 3)
        {
            totalAttacksRolled = 0;
            totalBlocksRolled = 0;

            OnUpdateDiceValues.Invoke();
            
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
    }
}
