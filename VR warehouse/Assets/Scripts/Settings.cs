using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    static float globalVolume;
    static float musicVolume = 1;
    static float soundsVolume;
    [SerializeField] AudioSource musicPlayer;

    public IEnumerator SwitchMusic(AudioClip n_music)
    {
        if (musicPlayer.volume > 0)
        {
            musicPlayer.volume -= 0.02f;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(SwitchMusic(n_music));
        }
        else
        {
            musicPlayer.clip = n_music;
            musicPlayer.Play();
            StartCoroutine(BuildUpMusicVolume());
        }
    }

    public IEnumerator BuildUpMusicVolume()
    {
        if (musicPlayer.volume < Settings.musicVolume)
        {
            musicPlayer.volume += 0.02f;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(BuildUpMusicVolume());
        }
    }
}
