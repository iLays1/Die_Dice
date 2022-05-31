using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePicking : MonoBehaviour
{
    [SerializeField] Transform selectionImage;
    [SerializeField] DisplayDieCanvasPos[] displayDice;

    [HideInInspector] public Dice selectedDie;
    bool dieConfirmed = false;

    private void Start()
    {
        var pool = GameDataSystem.Instance.dicePool;
        var intarray = CustomUtility.GetRandomIntArray(0, pool.Count);

        for (int i = 0; i < displayDice.Length; i++)
        {
            var diePosition = displayDice[i];
            diePosition.data = pool[intarray[i]];
            diePosition.LoadData();
            diePosition.displayDie.GetComponentInParent<DiceOnClick>().OnClick.
                AddListener(() => SelectDie(diePosition.displayDie));
        }

        gameObject.SetActive(false);
    }

    public void SelectDie(Dice die)
    {
        selectedDie = die;
        selectionImage.transform.DOMove(selectedDie.transform.position+(Vector3.up * 1.3f), 0.2f);
    }

    public void ConfirmDie()
    {
        if (selectedDie == null) return;
        if (dieConfirmed) return;
        dieConfirmed = true;

        GameDataSystem.Instance.dice.Add(selectedDie.data);

        float JUMPTIME = 0.8f;
        float JUMPFORCE = 4f;

        for (int i = 0; i < displayDice.Length; i++)
        {
            var die = displayDice[i];

            if(die != selectedDie)
            {
                die.transform.DOJump((Vector3.down*6f) + (Vector3.right*10), 
                    JUMPFORCE, 1, JUMPTIME * Random.Range(0.8f, 1.2f)).
                    SetEase(Ease.InBack);
            }
            else
            {

                die.transform.DOJump((Vector3.down * 6f) + (Vector3.left * 10), 
                    JUMPFORCE, 1, JUMPTIME * Random.Range(0.8f, 1.2f)).
                    SetEase(Ease.InBack);
            }
        }

        SceneSystem.Instance.NextLevel();
    }
}
