using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginView : MonoBehaviour
{
    public TMP_InputField emailOrUsernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;

    private LoginViewModel viewModel;

    void Start()
    {
        viewModel = new LoginViewModel(this);
    }

    // This function is called by the Login button
    public void OnLoginButtonPressed()
    {
        string input = emailOrUsernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        viewModel.Login(input, password);
    }

    // Helper function to update result text
    public void ShowResultMessage(string message)
    {
        resultText.text = message;
    }
}
