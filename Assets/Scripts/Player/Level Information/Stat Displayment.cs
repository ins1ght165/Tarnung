using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplayment : MonoBehaviour
{

    // In the inspector we can assign our text and image components so we can design it in the scene
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image[] stars; 
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;

    void Start()
    {
        // Fetching the final time from the "Level Stats" script
        float finalTime = LevelStats.completionTime;

        // We want to convert our time into minutes and seconds 
        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        timeText.text = $"Completion Time: {minutes:00}:{seconds:00}";

        // Condition for the amount of stars awarded based on the time completion
        int starCount = 1;
        if (finalTime < 60f) starCount = 3;
        else if (finalTime < 80f) starCount = 2;
        
        
        // This for loop will automatically loop trough each star
        // If we our index is still shorter than the stars earned the we will assign a full star otherwise it will just be a empty star
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < starCount ? fullStar : emptyStar;
        }
    }
}

