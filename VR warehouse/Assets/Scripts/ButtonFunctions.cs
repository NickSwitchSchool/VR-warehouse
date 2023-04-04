using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonFunctions : MonoBehaviour
{
    public UnityEvent buttonFunction;

    public void ActivateButtonFunction()
    {
        buttonFunction.Invoke();
    }
}