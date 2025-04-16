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
        StartCoroutine(NetworkManager.Instance.GetLeaderboard(levelName, OnLeaderboardReceived));
    }

    private void OnLeaderboardReceived(LeaderboardEntry[] entries)
    {
        if (entries == null)
        {
            Debug.Log("No leaderboard data received.");
            return;
        }

        // Clear old entries
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }

        // Populate leaderboard
        for (int i = 0; i < entries.Length; i++)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardParent);
            TMP_Text text = entry.GetComponent<TMP_Text>();
            text.text = $"{i + 1}. {entries[i].username} - {entries[i].rating} stars - {entries[i].time} sec";
        }
    }
}