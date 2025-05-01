using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        // If we don't have a saved value for the volume it will automaitcally be at the highest setting
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        
        // ensuring that the visual of our slider matches the current value
        volumeSlider.value = savedVolume;
        
        // Setting the volume globally for the entire game
        AudioListener.volume = savedVolume;

        // Adding a listener to "listen" for any change in the slider
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float value)
    {
        // Setting the volume value and saving it using playerprefs
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }
} 
