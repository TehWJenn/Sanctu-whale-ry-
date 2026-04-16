using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public LevelLoader levelLoader;

    // This function runs automatically when something enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that touched this has the "Player" tag
        if (other.CompareTag("Player"))
        {
            if (levelLoader != null)
            {
                levelLoader.TriggerNextLevel();
            }
            else
            {
                Debug.LogError("LevelLoader not assigned to GoalTrigger!");
            }
        }
    }
}