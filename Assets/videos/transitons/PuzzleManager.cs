using UnityEngine;


public class PuzzleManager : MonoBehaviour
{
    public int totalShapes = 3;
    private int correctlyPlacedShapes = 0;
    public LevelLoader levelLoader; // Drag your LevelLoader object here

    public void ShapePlacedCorrectly()
    {
        correctlyPlacedShapes++;

        if (correctlyPlacedShapes >= totalShapes)
        {
            // All shapes are in! Start the transition.
            levelLoader.TriggerNextLevel();
        }
    }
}