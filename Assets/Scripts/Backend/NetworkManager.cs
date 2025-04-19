using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Text;

public class NetworkManager : MonoBehaviour
{
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

    public IEnumerator RegisterUser(string username, string email, string password, System.Action<string, bool> callback)
    {
        string jsonData = $"{{\"username\":\"{username}\", \"email\":\"{email}\", \"password\":\"{password}\"}}";
        Debug.Log("Sending Register JSON: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/register", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
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

    


    public IEnumerator LoginUser(string emailOrUsername, string password, Action<string, bool> callback)
    {
        string jsonData = $"{{\"emailOrUsername\":\"{emailOrUsername}\", \"password\":\"{password}\"}}";
        Debug.Log("Sending Login JSON: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/login", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
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

    [Serializable]
    public class LoggedInUser
    {
        public User user;
    }

    [Serializable]
    public class User
    {
        public int id;
        public string username;
        public string email;
    }

    public IEnumerator GetLeaderboard(string levelName, Action<LeaderboardManager.LeaderboardEntry[]> callback)
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

    public IEnumerator SubmitScore(string levelName, int rating, float time, Action<string> callback)
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
            Debug.Log("Score response: " + response);
        }));
    }
}
