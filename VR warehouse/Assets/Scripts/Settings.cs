using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    static float globalVolume;
    static float musicVolume = 1;
    static float soundsVolume = 1;
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
            StartCoroutine(BuildUpMusicVolume(musicPlayer, 0.02f, false));
        }
    }

    public IEnumerator BuildUpMusicVolume(AudioSource n_audioSource, float n_stepSize, bool n_isSound)
    {
        if (!n_isSound && n_audioSource.volume < Settings.musicVolume)
        {
            n_audioSource.volume += n_stepSize;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(BuildUpMusicVolume(n_audioSource, n_stepSize, false));
        }
        else if (n_isSound && n_audioSource.volume < Settings.soundsVolume)
        {
            n_audioSource.volume += n_stepSize;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(BuildUpMusicVolume(n_audioSource, n_stepSize, true));
        }
    }

    public IEnumerator TurnDownMusicVolume(AudioSource n_audioSource)
    {
        if (n_audioSource.volume > 0)
        {
            n_audioSource.volume -= 0.02f;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(TurnDownMusicVolume(n_audioSource));
        }
        else
        {
            n_audioSource.volume = 0;
        }
    }
}
