using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    // This creates the "slot" you are looking for!
    public GameObject textCanvas; 

    void Start()
    {
        // Hide the text when the game starts
        if (textCanvas != null) textCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing touching the circle is the Player
        if (other.CompareTag("Player"))
        {
            textCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textCanvas.SetActive(false);
        }
    }
}