using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject buttonDisplay;
    [SerializeField] float visibilityTime;
    float visibilityTimer;

    private void Update()
    {
        if (visibilityTimer >= 0)
        {
            visibilityTimer -= Time.deltaTime;
            buttonDisplay.SetActive(true);
            buttonDisplay.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, .5f, 0));
            buttonDisplay.transform.Rotate(90, 0, 0);
        }
        else
        {
            buttonDisplay.SetActive(false);
        }
    }

    public void ShowUXButton()
    {
        visibilityTimer = visibilityTime;
    }
}
