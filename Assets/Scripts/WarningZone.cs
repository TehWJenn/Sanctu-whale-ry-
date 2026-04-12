using UnityEngine;

public class WarningZone : MonoBehaviour 
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource dangerSound; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the danger zone!");

            // 1. Start the Anxious Animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", true);

            // 2. Start the Pulsing Sound
            if (dangerSound != null && !dangerSound.isPlaying)
            {
                dangerSound.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the danger zone.");

            // 1. Stop the Anxious Animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", false);

            // 2. Stop the Pulsing Sound
            if (dangerSound != null)
            {
                dangerSound.Stop();
            }
        }
    }
}