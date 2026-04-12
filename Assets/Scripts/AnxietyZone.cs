using UnityEngine;

public class AnxietyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the whale enters the zone
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When the whale leaves the zone
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isAnxious", false);
        }
    }
}