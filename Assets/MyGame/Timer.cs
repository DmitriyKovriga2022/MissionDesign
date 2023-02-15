using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Timer
{
    public float Dt;
    public bool Started;
    public bool Finished;
    public float Duration;

    private float _currentTime = 0f;
    
    public Action TimerFinish;

    public Timer()
    {
    }

    public void Start(float duration)
    {
        Duration = duration;
        Started = true;
    }

    public void Update(float dt)
    {
        if (Finished || !Started)
            return;
        
        _currentTime += dt;
        if (_currentTime >= Duration)
        {
            Finish();
        }
    }

    public void Finish()
    {
        TimerFinish?.Invoke();
        Finished = true;
    }
}
