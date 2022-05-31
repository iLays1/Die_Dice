using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDieCanvasPos : MonoBehaviour
{
    [SerializeField] GameObject displayDiePrefab;
    public DiceData data;

    [HideInInspector] public Dice displayDie;

    private void Start()
    {
        if(data == null)
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    private void Update()
    {
        displayDie.transform.parent.position = transform.position;    
    }

    public void LoadData()
    {
        displayDie.data = data;
    }
}
