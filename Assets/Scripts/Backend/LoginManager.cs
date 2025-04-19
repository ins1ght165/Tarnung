using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class LoggedInUser
{
    public User user;
}

[System.Serializable]
public class User
{
    public int id;
    public string username;
    public string email;
}

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailOrUsernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;

    public void Login()
    {
        string input = emailOrUsernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
        {
            resultText.text = "All fields required.";
            return;
        }

        StartCoroutine(NetworkManager.Instance.LoginUser(input, password, (response, success) =>
        {
            if (!success || response.Contains("error"))
            {
                resultText.text = "Login failed. Please check your internet connection.";
            }
            else
            {
                // ✅ Save the logged-in user info
                LoggedInUser user = JsonUtility.FromJson<LoggedInUser>(response);
                PlayerPrefs.SetInt("userID", user.user.id);
                PlayerPrefs.SetString("username", user.user.username);
                PlayerPrefs.SetString("email", user.user.email);
                PlayerPrefs.Save();

                resultText.text = "Login successful!";
                PlayerPrefs.SetInt("isGuest", 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            }
        }));
    }
}