using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AccAuth : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;

    private bool correctInfo;

    public void onLoginPressed()
    {
        string name = nameField.text;
        string password = passwordField.text;

        string dummyName = "David";
        string dummyPassword = "Rex";
        
        if (name == dummyName && password == dummyPassword)
        {
            Debug.Log("Username and password are correct!");
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Debug.Log("Username and password are incorrect!");
        }
    }
}
