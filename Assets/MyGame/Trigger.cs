using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool Enabled;
    public Action Entered;
    
    void Start()
    {

    }
    
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Enabled)
            return;
        Entered?.Invoke();
        Debug.Log("Entered");
    }
}
