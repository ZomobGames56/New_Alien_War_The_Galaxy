using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource soundSouce;
    public Slider musicSlider; // Reference to the music volume slider
    public Slider soundEffectsSlider; // Reference to the sound effects volume slider

    private void Start()
    {
        SetValue(0);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        musicSlider.onValueChanged.AddListener(SetValue);
        soundEffectsSlider.onValueChanged.AddListener(SetValue);

    }
    private void SetValue(float value)
    {
            musicSource.volume = musicSlider.value;
            soundSouce.volume = soundEffectsSlider.value;
            PlayerPrefs.SetFloat("SoundValue", soundEffectsSlider.value);
            PlayerPrefs.SetFloat("MusicValue", musicSlider.value);
    }

}
