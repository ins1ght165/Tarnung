using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardViewModel
{
    private readonly LeaderboardView view;

    public LeaderboardViewModel(LeaderboardView leaderboardView)
    {
        view = leaderboardView;
    }

    public void ShowLeaderboard(string levelName)
    {
        view.StartCoroutine(LoadLeaderboardCoroutine(levelName));
    }

    private IEnumerator LoadLeaderboardCoroutine(string levelName)
    {
        yield return NetworkManager.Instance.GetLeaderboard(levelName, (entries) =>
        {
            if (entries == null || entries.Length == 0)
            {
                ShowLocalLeaderboard(levelName);
                return;
            }

            view.PopulateLeaderboard(entries);
        });
    }
    
    private void ShowLocalLeaderboard(string levelName)
    {
        var localProgress = SaveLocally.GetLevelProgress(levelName);

        // Checking for invalid data so we can place placeholders instead
        // Since there was an issue with showing the max float value instead
        if (localProgress.time <= 0 || localProgress.time == float.MaxValue)
        {
            view.PopulateLeaderboard(new LeaderboardEntry[]
            {
                new LeaderboardEntry
                {
                    username = "No Local Record",
                    rating = 0,
                    time = 0f
                }
            });
            return;
        }

        // Valid progress data found
        LeaderboardEntry[] localEntries = new LeaderboardEntry[1];
        localEntries[0] = new LeaderboardEntry
        {
            username = "Local Record",
            rating = localProgress.stars,
            time = localProgress.time
        };

        view.PopulateLeaderboard(localEntries);
    }


}

