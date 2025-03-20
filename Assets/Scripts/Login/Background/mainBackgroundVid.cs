using UnityEngine;
using UnityEngine.Video;

public class mainBackgroundVid : MonoBehaviour
{
    void Start()
    {
        // Attaching videoplayer to camera
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer targeting backplane of camera
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
    
        // Specifying the path of the video
        videoPlayer.url = "/Users/yousefnaiem/Desktop/Tarnung/Background/MGS title menu background animation.mp4";
        
        // Looping the video when it's done
        videoPlayer.isLooping = true;
        
        // Starting playback
        videoPlayer.Play();
    }
}
