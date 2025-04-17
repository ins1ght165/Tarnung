using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class RumbleToggle : MonoBehaviour
{
    public Toggle vibrationToggle;

    void Start()
    {
        // Load the saved state to check if user enable rumbe
        bool isVibrationOn = PlayerPrefs.GetInt("vibration", 1) == 1;
        vibrationToggle.isOn = isVibrationOn;

        vibrationToggle.onValueChanged.AddListener(onToggle);
    }

    void onToggle(bool isOn)
    {
        PlayerPrefs.SetInt("vibration", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

}
