using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [Header("Assign the function here")]
    public UnityEvent buttonFunction;
    [Header("Optional values")]
    [SerializeField] GameObject palletPrefab;
    [SerializeField] Vector3 palletPrefabLocationSpawn;

    public void ActivateButtonFunction()
    {
        buttonFunction.Invoke();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ShowSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SpawnPallet()
    {
        Debug.Log("HOIOIOIOIOIOIOI");
        Instantiate(palletPrefab, palletPrefabLocationSpawn, Quaternion.identity);
    }
}