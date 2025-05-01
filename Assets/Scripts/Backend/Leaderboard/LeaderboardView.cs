using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardView : MonoBehaviour
{
    [Header("Leaderboard UI")]
    public Transform leaderboardParent;
    public GameObject leaderboardEntryPrefab;

    private LeaderboardViewModel viewModel;

    private void Awake()
    {
        viewModel = new LeaderboardViewModel(this);
    }

    // Called from Button or other script
    public void OnShowLeaderboard(string levelName)
    {
        viewModel.ShowLeaderboard(levelName);
    }

    public void PopulateLeaderboard(LeaderboardEntry[] entries)
    {
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }
        

        for (int i = 0; i < entries.Length; i++)
        {
            string timeDisplay;
            
            int minutes = Mathf.FloorToInt(entries[i].time / 60f);
            int seconds = Mathf.FloorToInt(entries[i].time % 60f);
            timeDisplay = $"{minutes:00}:{seconds:00}";
            
            
            
            
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardParent);
            TMP_Text text = entry.GetComponent<TMP_Text>();
            text.text = $"{i + 1}. {entries[i].username} - {entries[i].rating} stars - {timeDisplay}";

        }
    }
}
