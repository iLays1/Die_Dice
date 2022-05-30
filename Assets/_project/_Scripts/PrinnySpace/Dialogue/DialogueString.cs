using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueString : ScriptableObject
{
    [System.Serializable]
    public class DS_Sentence
    {
        [TextArea]
        public string text;
        public float speed;
        public float holdTime;
    }

    public DS_Sentence[] sentences;
    public float fullTime
    {
        get
        {
            float time = 0;
            foreach(var s in sentences)
            {
                time += s.speed + s.holdTime;
            }
            return time;
        }
    }
}
