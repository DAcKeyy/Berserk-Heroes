using System;
using System.Collections.Generic;
using UnityEngine;

internal class UnityMainThread : MonoBehaviour
{
    internal static UnityMainThread wkr;
    Queue<Action> jobs = new Queue<Action>();

    void Awake() {
        if (wkr == null) wkr = this;   
        else if (wkr == this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    void Update() {
        while (jobs.Count > 0) 
            jobs.Dequeue().Invoke();
    }

    internal void AddJob(Action newJob) {
        jobs.Enqueue(newJob);
    }
}