using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStringOnLoad : MonoBehaviour
{
    public DialogueString dialogue;

    void Start()
    {
        DialogueSystem.Instance.PlayString(dialogue);
    }
}
