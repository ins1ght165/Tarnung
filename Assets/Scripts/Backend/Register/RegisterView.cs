using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterView : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;

    private RegisterViewModel viewModel;
    public TMP_InputField confirmPasswordInput;


    void Start()
    {
        viewModel = new RegisterViewModel(this);
    }

    // This function is called by the Register button
    public void OnRegisterButtonPressed()
    {
        string username = usernameInput.text.Trim();
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();
        string confirmPassword = confirmPasswordInput.text.Trim();

        viewModel.Register(username, email, password, confirmPassword);
    }

    // Helper function to update result text
    public void ShowResultMessage(string message)
    {
        resultText.text = message;
    }
}
