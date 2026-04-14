using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;
        
        // This stops time in the game (0 = frozen, 1 = normal speed)
        Time.timeScale = pause ? 0f : 1f;
        
        // Optional: Hide/Show the mouse cursor
        Cursor.visible = pause;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
    }
}