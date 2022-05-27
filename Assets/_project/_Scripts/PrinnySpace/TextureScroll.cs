using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;

    Vector2 offset;
    Material mat;

    private void Awake()
    {
        var mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    private void Update()
    {
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime * xSpeed;
        offset.y += Time.deltaTime * ySpeed;
        mat.mainTextureOffset = offset;
    }
}
