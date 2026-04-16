using UnityEngine;
using UnityEngine.Video;

public class VideoLevelTransitor : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public LevelLoader levelLoader;

    void OnEnable()
    {
        // Subscribe to the event that fires when the video ends
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent errors when the object is destroyed
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (levelLoader != null)
        {
            levelLoader.TriggerNextLevel();
        }
    }
}