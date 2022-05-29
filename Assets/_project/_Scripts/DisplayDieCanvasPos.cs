using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDieCanvasPos : MonoBehaviour
{
    public GameObject displayDiePrefab;
    public DieData data;
    public Die displayDie;

    private void Start()
    {
        if(data == null)
        {
            Destroy(gameObject);
        }

        displayDie = Instantiate(displayDiePrefab, this.transform).GetComponentInChildren<Die>();
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
