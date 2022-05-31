using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpouseUiOption : MonoBehaviour
{
    public static NameGenerator nameGenerator;

    public UnityEvent OnMarryButtonEvent = new UnityEvent();

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] DisplayDieCanvasPos[] dicePositions;

    [HideInInspector] public SpouseData spouse;

    private void Awake()
    {
        if (nameGenerator == null)
            nameGenerator = new NameGenerator("SpouseNames");

        for (int i = 0; i < dicePositions.Length; i++)
        {
            dicePositions[i].data = spouse.dice[i];
        }
        nameText.text = nameGenerator.Get();
    }

    public void SetSpouse(SpouseData data)
    {
        spouse = data;
    }

    public void OnMarryButton()
    {
        OnMarryButtonEvent.Invoke();
    }
}
