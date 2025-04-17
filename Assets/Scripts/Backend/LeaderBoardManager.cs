using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [Header("Leaderboard UI")]
    public Transform leaderboardParent;
    public GameObject leaderboardEntryPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string username;
        public int rating;
        public float time;
    }

    [System.Serializable]
    public class LeaderboardList
    {
        public LeaderboardEntry[] entries;
    }

    public void ShowLeaderboard(string levelName)
    {
        StartCoroutine(NetworkManager.Instance.GetLeaderboard(levelName, (entries) =>
        {
            if (entries == null)
            {
                Debug.Log("No leaderboard data received. Loading local data instead.");
                ShowLocalLeaderboard(levelName);
                return;
            }

            PopulateLeaderboard(entries);
        }));
    }

    private void PopulateLeaderboard(LeaderboardEntry[] entries)
    {
        // Clear old entries
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < entries.Length; i++)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardParent);
            TMP_Text text = entry.GetComponent<TMP_Text>();
            text.text = $"{i + 1}. {entries[i].username} - {entries[i].rating} stars - {entries[i].time:F1} sec";
        }
    }

    private void ShowLocalLeaderboard(string levelName)
    {
        var localProgress = SaveLocally.GetLevelProgress(levelName);

        // Clear old entries
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }

        GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardParent);
        TMP_Text text = entry.GetComponent<TMP_Text>();
        text.text = $"Local Record - {localProgress.stars} stars - {localProgress.time:F1} sec";
    }
} 
