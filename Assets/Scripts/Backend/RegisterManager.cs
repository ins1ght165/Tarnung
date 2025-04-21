using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class RegisteredUser
{
    public int id;
    public string username;
    public string email;
}

public class RegisterManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;

    public void Register()
    {
        Debug.Log("Register button pressed");
        string username = usernameInput.text.Trim();
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            resultText.text = "All fields required.";
            return;
        }

        StartCoroutine(NetworkManager.Instance.RegisterUser(username, email, password, (response, success) =>
        {
            if (!success || response.Contains("error"))
            {
                if (response.Contains("Username already exists"))
                {
                    resultText.text = "Username is already taken.";
                }
                else if (response.Contains("Email already exists"))
                {
                    resultText.text = "Email is already registered.";
                }
                else
                {
                    resultText.text = "Register failed. Please check your internet connection.";
                }

                Debug.LogError("Register failed: " + response);
            }
            else
            {
                // Parse and save user info to PlayerPrefs
                RegisteredUser user = JsonUtility.FromJson<RegisteredUser>(response);
                PlayerPrefs.SetInt("userID", user.id);
                PlayerPrefs.SetString("username", user.username);
                PlayerPrefs.SetString("email", user.email);
                PlayerPrefs.Save();

                resultText.text = "Registered successfully!";
                PlayerPrefs.SetInt("isGuest", 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            }
        }));
    }
}