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

    public void SetAudioEnabled(bool enabled) {
        audioEnabled = enabled;
    }
}
