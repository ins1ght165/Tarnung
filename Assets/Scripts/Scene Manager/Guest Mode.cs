using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuestMode : MonoBehaviour
{
    public void PlayAsGuest()
    {
        // Creating an integer value for guest mode so that we don't update the leaderboard or confuse the guest for a previously logged in player
        PlayerPrefs.SetInt("isGuest", 1);     
        // Invalidate the ID
        PlayerPrefs.SetInt("userID", -1);  
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main Menu");
    }
}

