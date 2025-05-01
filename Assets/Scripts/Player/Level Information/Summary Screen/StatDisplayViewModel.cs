using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatDisplayViewModel
{
    private readonly StatDisplayView view;
    private LevelResultModel result;

    public StatDisplayViewModel(StatDisplayView view)
    {
        this.view = view;
    }

    public void LoadAndDisplayResults()
    {
        result = new LevelResultModel
        {
            CompletionTime = LevelStats.completionTime,
            LevelName = PlayerPrefs.GetString("lastPlayedLevel", "UnknownLevel"),
            StarCount = CalculateStars(LevelStats.completionTime)
        };

        view.UpdateTimeText(result.CompletionTime);
        view.UpdateStars(result.StarCount);

        view.StartCoroutine(SubmitScore(result.LevelName, result.StarCount, result.CompletionTime));
    }

    private int CalculateStars(float time)
    {
        if (time < 60f) return 3;
        if (time < 80f) return 2;
        return 1;
    }

    private IEnumerator SubmitScore(string levelName, int stars, float time)
    {
        yield return NetworkManager.Instance.SubmitScore(levelName, stars, time, (response) =>
        {
            SaveLocally.SaveLevelProgress(levelName, stars, time);
            if (response.Contains("Score updated"))
            {
                view.ShowNewRecord();
            }
        });
    }
}
