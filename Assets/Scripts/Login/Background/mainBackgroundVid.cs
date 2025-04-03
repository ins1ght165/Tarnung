using UnityEngine;
using UnityEngine.Video;

public class mainBackgroundVid : MonoBehaviour
{
    // Making it possible to assing a video from the inspector
    public VideoClip videoClip;
    void Start()
    {
        // Attaching videoplayer to camera
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer targeting backplane of camera
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
    
        // Assigning video fetched from the inspector
        videoPlayer.clip = videoClip;
        
        // Looping the video when it's done
        videoPlayer.isLooping = true;
        
        // Starting playback
        videoPlayer.Play();
    }
}