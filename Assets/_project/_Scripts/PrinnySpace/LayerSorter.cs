using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    public int offset;
    public bool isStatic = true;

    SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        rend.sortingOrder = 100 + -(Mathf.FloorToInt(transform.position.y * 100) + offset);
        if (isStatic) Destroy(this);
    }
    private void Update()
    {
        rend.sortingOrder = 100 + -(Mathf.FloorToInt(transform.position.y * 100) + offset);
    }
}
