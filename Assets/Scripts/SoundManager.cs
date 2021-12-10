using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    bool audioEnabled = true;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip) {
        if (!audioEnabled) {
            return;
        }

        audioSource.PlayOneShot(clip);
    }

    public void SetClipAndPlay(AudioClip clip)
    {
        if (!audioEnabled) {
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Stop()
    {
        if (!audioEnabled) {
            return;
        }
        
        audioSource.Stop();
    }

    public float GetTime()
    {
        return audioSource.time;
    }

    public void SetAudioEnabled(bool enabled) {
        audioEnabled = enabled;
    }
}
