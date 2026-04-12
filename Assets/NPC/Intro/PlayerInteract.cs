using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f; // How close you need to be

    void Update()
    {
        // When you press 'E' (or whatever key you prefer)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Create a small circle around the player to find NPCs
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
            
            foreach (Collider2D col in colliders)
            {
                // Check if the thing we hit has our NPC script
                if (col.TryGetComponent(out NPC npc))
                {
                    npc.Interact(); // This triggers your dialogue code!
                    return; // Stop looking once we find one NPC
                }
            }
        }
    }

    // This lets you see the interaction range in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}