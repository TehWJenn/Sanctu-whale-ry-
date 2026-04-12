using UnityEngine;

public class AnxietyZone : MonoBehaviour 
{
    // Drag the 'WarningSound' object here in the Inspector
    [SerializeField] private AudioSource dangerSound; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start Animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", true);

            // Start Pulsing Sound
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
            // Stop Animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", false);

            // Stop Pulsing Sound
            if (dangerSound != null)
            {
                dangerSound.Stop();
            }
        }
    }
}


// using UnityEngine;

// public class AnxietyZone : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         // When the whale enters the zone
//         if (other.CompareTag("Player"))
//         {
//             Animator anim = other.GetComponent<Animator>();
//             if (anim != null) anim.SetBool("isAnxious", true);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         // When the whale leaves the zone
//         if (other.CompareTag("Player"))
//         {
//             Animator anim = other.GetComponent<Animator>();
//             if (anim != null) anim.SetBool("isAnxious", false);
//         }
//     }
// }