using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        public Sprite portrait;
        public string text;
    }

    public DialogueLine[] lines;
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;

    private int currentLine = 0;

    void Start()
    {
        ShowLine(currentLine);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            if (currentLine < lines.Length)
            {
                ShowLine(currentLine);
            }
            else
            {
                // Load next scene or resume gameplay
                UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene");
            }
        }
    }

    void ShowLine(int index)
    {
        portraitImage.sprite = lines[index].portrait;
        dialogueText.text = lines[index].text;
    }
}

