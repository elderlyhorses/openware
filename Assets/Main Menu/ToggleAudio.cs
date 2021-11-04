using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    public Sprite AudioOnSprite;
    public Sprite AudioOffSprite;
    public Image AudioImage;

    SoundManager SoundManager;
    bool audioEnabled;

    void Awake()
    {
        SoundManager = FindObjectOfType<SoundManager>();
        audioEnabled = PlayerPrefs.GetInt("AudioEnabled", 1) == 1;
        AudioImage.sprite = audioEnabled ? AudioOnSprite : AudioOffSprite;
        SoundManager.SetAudioEnabled(audioEnabled);
    }

    public void ToggleSound() {
        audioEnabled = !audioEnabled;
        PlayerPrefs.SetInt("AudioEnabled", audioEnabled ? 1 : 0);
        AudioImage.sprite = audioEnabled ? AudioOnSprite : AudioOffSprite;
        SoundManager.SetAudioEnabled(audioEnabled);
    }
}
