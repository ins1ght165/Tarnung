using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginViewModel
{
    private readonly LoginView view;

    public LoginViewModel(LoginView loginView)
    {
        view = loginView;
    }

    public void Login(string input, string password)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
        {
            view.ShowResultMessage("All fields required.");
            return;
        }

        view.StartCoroutine(LoginCoroutine(input, password));
    }

    private IEnumerator LoginCoroutine(string input, string password)
    {
        // This calls our NetworkManager to login the user
        yield return NetworkManager.Instance.LoginUser(input, password, (response, success) =>
        {
            if (!success || response.Contains("error"))
            {
                view.ShowResultMessage("Login failed. Please check your connection and verify the input.");
            }
            else
            {
                // Save the logged-in user info
                LoggedInUser user = JsonUtility.FromJson<LoggedInUser>(response);
                PlayerPrefs.SetInt("userID", user.user.id);
                PlayerPrefs.SetString("username", user.user.username);
                PlayerPrefs.SetString("email", user.user.email);
                PlayerPrefs.Save();
                
                

                view.ShowResultMessage("Login successful!");
                PlayerPrefs.SetInt("isGuest", 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            }
        });
    }
}

