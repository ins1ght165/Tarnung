using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;




public class NetworkManager : MonoBehaviour
{
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private static NetworkManager _instance;
    private string serverUrl = "http://localhost:3000"; 

    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("NetworkManager");
                _instance = obj.AddComponent<NetworkManager>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

// Register a new user
    public IEnumerator RegisterUser(string username, string email, string password, System.Action<string> callback)
    {
        string jsonData = $"{{\"username\":\"{username}\", \"email\":\"{email}\", \"password\":\"{password}\"}}";
        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/register", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            callback(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : request.error);
        }
    }
    
    public IEnumerator LoginUser(string emailOrUsername, string password, Action<string, bool> callback)
    {
        string jsonData = $"{{\"emailOrUsername\":\"{emailOrUsername}\", \"password\":\"{password}\"}}";
        Debug.Log("Sending Login JSON: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/login", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                callback($"HTTP Error {request.responseCode}: {request.error}", false);
            }
            else
            {
                callback(request.downloadHandler.text, true);
            }
        }
    }

    
    
    
//to register new user

    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_Text resultText;
    public TMP_InputField passwordInput;
    
    // Login a user in
    
    public TMP_InputField loginNameOrEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_Text loginResultText;


    public void RegisterUser()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text.Trim();

        StartCoroutine(NetworkManager.Instance.RegisterUser(username, email, password, (response) =>
        {
            if (response.Contains("error") || response.Contains("Failed to connect") || response.Contains("Cannot connect"))
            {
                resultText.text = "Failed to register (server offline?)";
                Debug.LogError("Register failed: " + response);
            }
            else
            {
                resultText.text = "Register Success";
                Debug.Log("Register Success: " + response);
            }
        }));
    }
    
    public void LoginUser()
    {
        string input = loginNameOrEmailInput.text.Trim();
        string password = loginPasswordInput.text.Trim();

        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
        {
            loginResultText.text = "All fields required";
            return;
        }

        StartCoroutine(LoginUser(input, password, (response, success) =>
        {
            if (!success || response.Contains("error"))
            {
                loginResultText.text = "Login failed: " + response;
                Debug.LogError("Login failed: " + response);
            }
            else
            {
                loginResultText.text = "Login successful!";
                Debug.Log("Login success: " + response);
                
                // Parse the user info
                LoggedInUser user = JsonUtility.FromJson<LoggedInUser>(response);
                PlayerPrefs.SetInt("userID", user.user.id); 
                PlayerPrefs.SetString("username", user.user.username);
                PlayerPrefs.SetString("email", user.user.email);

               
                // Loading next scene if succesfull
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu"); 
            }
        }));
    }
    
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

    
    // Leaderboard
    public IEnumerator GetLeaderboard(string levelName, System.Action<LeaderboardManager.LeaderboardEntry[]> callback)
    {
        string url = $"{serverUrl}/leaderboard/{levelName}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Leaderboard error: " + request.error);
                callback(null);
            }
            else
            {
                string wrappedJson = "{\"entries\":" + request.downloadHandler.text + "}";
                LeaderboardManager.LeaderboardList list = JsonUtility.FromJson<LeaderboardManager.LeaderboardList>(wrappedJson);
                callback(list.entries);
            }
        }
    }
    
    // Score 
    public IEnumerator SubmitScore(string levelName, int rating, float time, System.Action<string> callback)
    {
        int userId = PlayerPrefs.GetInt("userID", -1);
        if (userId == -1)
        {
            callback("User not logged in");
            yield break;
        }

        string jsonData = $"{{\"user_id\":{userId}, \"level_name\":\"{levelName}\", \"rating\":{rating}, \"time\":{time}}}";
        Debug.Log("Submitting score JSON: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/submit-score", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                callback($"HTTP Error {request.responseCode}: {request.error}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }
    public void SubmitPlayerScore(string levelName, int rating, float time)
    {
        StartCoroutine(SubmitScore(levelName, rating, time, (response) =>
        {
            Debug.Log("🔥 Score response: " + response);
        }));
    }

}