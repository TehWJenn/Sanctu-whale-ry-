using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelLoader.TriggerNextLevel();
        }
    }
}