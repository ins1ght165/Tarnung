using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatDisplayView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private TextMeshProUGUI newRecordText;

    private StatDisplayViewModel viewModel;

    void Start()
    {
        viewModel = new StatDisplayViewModel(this);
        viewModel.LoadAndDisplayResults();
    }

    public void UpdateTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = $"Completion Time: {minutes:00}:{seconds:00}";
    }

    public void UpdateStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < count ? fullStar : emptyStar;
        }
    }

    public void ShowNewRecord()
    {
        newRecordText.text = "New Record!";
        newRecordText.color = Color.red;
    }
}

