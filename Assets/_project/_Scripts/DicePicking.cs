using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePicking : MonoBehaviour
{
    public Transform selectionImage;
    public Die selectedDie;
    public DisplayDieCanvasPos[] displayDice;
    bool dieConfirmed = false;

    private void Start()
    {
        var pool = PlayerDataSystem.Instance.dicePool;
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

    public void SelectDie(Die die)
    {
        selectedDie = die;
        selectionImage.transform.DOMove(selectedDie.transform.position+(Vector3.up * 1.2f), 0.1f);
    }

    public void ConfirmDie()
    {
        if (selectedDie == null) return;
        if (dieConfirmed) return;
        dieConfirmed = true;

        PlayerDataSystem.Instance.dice.Add(selectedDie.data);

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
