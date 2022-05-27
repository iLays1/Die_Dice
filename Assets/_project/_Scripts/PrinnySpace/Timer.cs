using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float resetTime = 2f;
    public float timer = 0f;
    public bool startOnAwake = true;
    public bool loop = true;

    public UnityEvent OnTimer = new UnityEvent();
    bool going = false;

    private void Awake()
    {
        if(startOnAwake)
            StartTimer();
    }

    public void StartTimer()
    {
        going = true;
        timer = 0f;
    }
    private void Update()
    {
        if (!going) return;

        timer += Time.deltaTime;

        if(timer > resetTime)
        {
            OnTimer.Invoke();
            timer = 0f;

            if (!loop)
                going = false;
        }
    }
}
