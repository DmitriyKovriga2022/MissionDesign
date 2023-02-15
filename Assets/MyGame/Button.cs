using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Action Pressed;

    public void Press()
    {
        Pressed?.Invoke();
        Debug.Log("Button pressed");
    }
}
