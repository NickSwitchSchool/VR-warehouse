using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLoadTruck : MonoBehaviour
{
    [Header("Stress UX")]
    bool stressing;
    [SerializeField] Light[] alarmLights;
    [SerializeField] Color defaultAreaColor;
    [SerializeField] Color stressAreaColor;
    [SerializeField] float fadeSpeed;
    float fadeAmount;
    [SerializeField] Settings settings;
    [SerializeField] AudioSource alarmSound;
    [SerializeField] AudioClip stressMusic;
    [SerializeField] AudioClip relaxedMusic;

    private void Start()
    {
        StartCoroutine(Chilling());
    }

    public void ActivateStressMode()
    {
        Debug.Log("Stress mode activated!");
        stressing = true;
        StartCoroutine(settings.BuildUpMusicVolume(alarmSound, 0.01f, true));
        StartCoroutine(settings.SwitchMusic(stressMusic));
    }

    public void DeactivateStressMode()
    {
        Debug.Log("Stress mode deactivated!");
        stressing = false;
        StartCoroutine(settings.TurnDownMusicVolume(alarmSound));
        StartCoroutine(settings.SwitchMusic(relaxedMusic));
    }

    IEnumerator Stressing(bool n_state)
    {
        Debug.Log(n_state);
        fadeAmount += fadeSpeed * Time.deltaTime;
        if (n_state)
        {
            foreach (Light n_alarmLight in alarmLights)
            {
                n_alarmLight.color = Color.Lerp(n_alarmLight.color, stressAreaColor, fadeAmount / 10);
            }
        }
        else
        {
            foreach (Light n_alarmLight in alarmLights)
            {
                n_alarmLight.color = Color.Lerp(n_alarmLight.color, defaultAreaColor, fadeAmount / 10);
            }
        }
        
        if (stressing)
        {
            if (fadeAmount >= fadeSpeed)
            {
                yield return new WaitForEndOfFrame();
                fadeAmount = 0;
                StartCoroutine(Stressing(!n_state));
            }
            else
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine(Stressing(n_state));
            }
        }
        else
        {
            StartCoroutine(Chilling());
        }
    }

    IEnumerator Chilling()
    {
        yield return new WaitForEndOfFrame();
        if (stressing)
        {
            fadeAmount = 0;
            StartCoroutine(Stressing(true));
        }
        else
        {
            fadeAmount += fadeSpeed * Time.deltaTime;
            foreach (Light n_alarmLight in alarmLights)
            {
                n_alarmLight.color = Color.Lerp(n_alarmLight.color, defaultAreaColor, fadeAmount / 10);
            }
            StartCoroutine(Chilling());
        }
    }
}
