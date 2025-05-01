using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterViewModel
{
    private readonly RegisterView view;

    public RegisterViewModel(RegisterView registerView)
    {
        view = registerView;
    }

    public void Register(string username, string email, string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || 
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            view.ShowResultMessage("All fields are required.");
            return;
        }

        if (password != confirmPassword)
        {
            view.ShowResultMessage("Passwords do not match.");
            return;
        }

        view.StartCoroutine(RegisterCoroutine(username, email, password));
    }


    private IEnumerator RegisterCoroutine(string username, string email, string password)
    {
        // This calls our NetworkManager to register the user
        yield return NetworkManager.Instance.RegisterUser(username, email, password, (response, success) =>
        {
            if (!success || response.Contains("error"))
            {
                if (response.Contains("Username already exists"))
                {
                    view.ShowResultMessage("Username is already taken.");
                }
                else if (response.Contains("Email already exists"))
                {
                    view.ShowResultMessage("Email is already registered.");
                }
                else
                {
                    view.ShowResultMessage("Register failed. Please check your internet connection.");
                }
            }
            else
            {
                // Parse and save user info to PlayerPrefs
                UserModel user = JsonUtility.FromJson<UserModel>(response);
                PlayerPrefs.SetInt("userID", user.id);
                PlayerPrefs.SetString("username", user.username);
                PlayerPrefs.SetString("email", user.email);
                PlayerPrefs.Save();

                view.ShowResultMessage("Registered successfully!");
                PlayerPrefs.SetInt("isGuest", 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            }
        });
    }
}
