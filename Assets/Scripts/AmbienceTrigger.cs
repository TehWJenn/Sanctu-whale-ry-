using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    public AudioSource warningSource; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the object entering is the Player
        if (other.CompareTag("Player"))
        {
            // 2. Play the sound
            if (warningSource != null && !warningSource.isPlaying)
            {
                warningSource.Play();
            }

            // 3. Set the animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 4. Stop the sound
            if (warningSource != null)
            {
                warningSource.Stop();
            }

            // 5. Reset the animation
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", false);
        }
    }
}