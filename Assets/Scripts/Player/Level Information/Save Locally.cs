using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocally : MonoBehaviour
{
    // Save local score for a level
    public static void SaveLevelProgress(string levelName, int stars, float time)
    {
        string starKey = levelName + "_Stars";
        string timeKey = levelName + "_Time";

        int previousStars = PlayerPrefs.GetInt(starKey, 0);
        float previousTime = PlayerPrefs.GetFloat(timeKey, float.MaxValue);

        // Only overwrite if better: more stars, or same stars with faster time or if no record even exists to begin with
        if (!HasSavedProgress(levelName) || stars > previousStars || (stars == previousStars && time < previousTime))
        {
            PlayerPrefs.SetInt(starKey, stars);
            PlayerPrefs.SetFloat(timeKey, time);
            PlayerPrefs.Save();
        }
    }

    // Get saved local score
    public static (int stars, float time) GetLevelProgress(string levelName)
    {
        int stars = PlayerPrefs.GetInt(levelName + "_Stars", 0);
        float time = PlayerPrefs.GetFloat(levelName + "_Time", float.MaxValue);
        return (stars, time);
    }

    // checking if we already have a saved score
    public static bool HasSavedProgress(string levelName)
    {
        return PlayerPrefs.HasKey(levelName + "_Stars") && PlayerPrefs.HasKey(levelName + "_Time");
    }
} 
