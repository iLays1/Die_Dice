using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSortingOrder : MonoBehaviour
{
    public int sortingOrder;
    public string layerName = string.Empty;

    private void Awake()
    {
        var renderer = GetComponent<Renderer>();
        if (layerName != string.Empty)
        {
            renderer.sortingLayerName = layerName;
            renderer.sortingOrder = sortingOrder;
        }
    }
}
