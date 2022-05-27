using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDice : MonoBehaviour
{
    public Die die;

    int[] gifFaces = new int[3];
    int face = -1;

    private void Awake()
    {
        for (int i = 0; i < gifFaces.Length; i++)
        {
            gifFaces[i] = Random.Range(0, 6);
        }
        NextFace();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextFace();
        }
    }

    void NextFace()
    {
        face++;
        if (face >= gifFaces.Length)
            face = 0;

        die.RollToFace(gifFaces[face]);
    }
}
