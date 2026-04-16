using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue Data")]
    public NPCDialogue dialogueData;
    
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private bool canSkip; 
    
    // This ensures the dialogue only happens once
    private bool hasPlayed = false; 

    public bool CanInteract()
    {
        // Only allow interaction if not active and hasn't played yet
        return !isDialogueActive && !hasPlayed;
    }

    public void Interact()
    {
        if(dialogueData == null || hasPlayed) return; 
        
        if (isDialogueActive)
        {
            if (canSkip) NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    // This handles the automatic popup when crossing the collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger if it's the Player and it hasn't played yet
        if (other.CompareTag("Player") && !hasPlayed && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueData == null) return;

        isDialogueActive = true;
        canSkip = false; 
        dialogueIndex = 0;
        
        // Stop player movement
        Playermovement.canMove = false; 

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialogueText.SetText(""); 

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
        StartCoroutine(EnableSkipAfterDelay());
        
        // Ensure cursor is visible when dialogue starts
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
            canSkip = true; 
        }
        else 
        {
            // If you only have 1 line, this ends it. 
            // If you had more lines, you'd add logic here to increment dialogueIndex
            EndDialogue();
        } 
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        yield return new WaitForEndOfFrame();

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(dialogueData.typingSpeed); 
        }

        isTyping = false;
    }

    IEnumerator EnableSkipAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        canSkip = true;
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        hasPlayed = true; // Mark as done forever so it won't pop up again

        dialoguePanel.SetActive(false);
        
        // Unpause the game
        PauseController.SetPause(false);
        Playermovement.canMove = true; 

        // Set cursor back
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if (UnityEngine.EventSystems.EventSystem.current != null)
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; 

// public class NPC : MonoBehaviour, IInteractable
// {
//     public NPCDialogue dialogueData;
//     public GameObject dialoguePanel;
//     public TMP_Text dialogueText, nameText;
//     public Image portraitImage;

//     private int dialogueIndex;
//     private bool isTyping, isDialogueActive;
//     private bool canSkip; 
    
//     // This ensures the dialogue only happens once
//     private bool hasPlayed = false; 

//     public bool CanInteract()
//     {
//         // Only allow interaction if not active and hasn't played yet
//         return !isDialogueActive && !hasPlayed;
//     }

//     public void Interact()
//     {
//         if(dialogueData == null || hasPlayed) return; 
        
//         if (isDialogueActive)
//         {
//             if (canSkip) NextLine();
//         }
//         else
//         {
//             StartDialogue();
//         }
//     }

//     void StartDialogue()
//     {
//         isDialogueActive = true;
//         canSkip = false; 
//         dialogueIndex = 0;
        
//         Playermovement.canMove = false; 

//         nameText.SetText(dialogueData.npcName);
//         portraitImage.sprite = dialogueData.npcPortrait;
//         dialogueText.SetText(""); 

//         dialoguePanel.SetActive(true);
//         PauseController.SetPause(true);

//         StartCoroutine(TypeLine());
//         StartCoroutine(EnableSkipAfterDelay());
        
//         // Ensure cursor is visible when dialogue starts
//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;
//     }

//     void NextLine()
//     {
//         if (isTyping)
//         {
//             StopAllCoroutines();
//             dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//             isTyping = false;
//             canSkip = true; 
//         }
//         else 
//         {
//             // Closes the box on the next click after text is full
//             EndDialogue();
//         } 
//     }

//     IEnumerator TypeLine()
//     {
//         isTyping = true;
//         dialogueText.SetText("");
//         yield return new WaitForEndOfFrame();

//         foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSecondsRealtime(dialogueData.typingSpeed); 
//         }

//         isTyping = false;
//     }

//     IEnumerator EnableSkipAfterDelay()
//     {
//         yield return new WaitForSecondsRealtime(0.3f);
//         canSkip = true;
//     }

//     public void EndDialogue()
//     {
//         StopAllCoroutines();
//         isDialogueActive = false;
//         hasPlayed = true; // Mark as done forever

//         dialoguePanel.SetActive(false);
        
//         // Unpause the game first
//         PauseController.SetPause(false);
//         Playermovement.canMove = true; 

//         // FORCE cursor back to visible AFTER unpausing
//         // This stops other scripts from hiding it
//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;
        
//         if (UnityEngine.EventSystems.EventSystem.current != null)
//             UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
//     }

//     void OnTriggerEnter2D(Collider2D other)
// {
//     // Check if the thing hitting the soundwaves is tagged "Player"
//     // AND check if the dialogue hasn't played yet
//     if (other.CompareTag("Player") && !hasPlayed)
//     {
//         StartDialogue();
//     }
// }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; 

// public class NPC : MonoBehaviour, IInteractable
// {
//     public NPCDialogue dialogueData;
//     public GameObject dialoguePanel;
//     public TMP_Text dialogueText, nameText;
//     public Image portraitImage;

//     private int dialogueIndex;
//     private bool isTyping, isDialogueActive;
    
//     // The "Shield" that stops the instant-freeze/double-click
//     private bool canSkip; 

//     public bool CanInteract()
//     {
//         return !isDialogueActive;
//     }

//     public void Interact()
//     {
//         if(dialogueData == null) return; 
        
//         if (isDialogueActive)
//         {
//             // Only skip or go to next line if the "Skip Gate" is open
//             if (canSkip)
//             {
//                 NextLine();
//             }
//         }
//         else
//         {
//             StartDialogue();
//         }
//     }

//     void StartDialogue()
//     {
//         isDialogueActive = true;
//         canSkip = false; 
//         dialogueIndex = 0;
        
//         Playermovement.canMove = false; 

//         nameText.SetText(dialogueData.npcName);
//         portraitImage.sprite = dialogueData.npcPortrait;
//         dialogueText.SetText(""); 

//         dialoguePanel.SetActive(true);
        
//         // Fixed: Talking to PauseController directly
//         PauseController.SetPause(true);

//         // This ensures the first line starts typing WITHOUT a second click
//         StartCoroutine(TypeLine());
//         StartCoroutine(EnableSkipAfterDelay());
        
//         // Force the mouse to stay visible
//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;
//     }

//     void NextLine()
//     {
//         if (isTyping)
//         {
//             StopAllCoroutines();
//             dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//             isTyping = false;
//             canSkip = true; 
//         }
//         else if (++dialogueIndex < dialogueData.dialogueLines.Length)
//         {
//             canSkip = false; 
//             StartCoroutine(TypeLine());
//             StartCoroutine(EnableSkipAfterDelay());
//         }
//         else
//         {
//             EndDialogue();
//         } 
//     }

//     IEnumerator TypeLine()
//     {
//         isTyping = true;
//         dialogueText.SetText("");

//         // Wait a frame to clear the 'Interact' click
//         yield return new WaitForEndOfFrame();

//         foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
//         {
//             dialogueText.text += letter;
//             // Use Realtime so it types while the game is paused
//             yield return new WaitForSecondsRealtime(dialogueData.typingSpeed); 
//         }

//         isTyping = false;
//     }

//     IEnumerator EnableSkipAfterDelay()
//     {
//         // Shield the text for 0.3 seconds
//         yield return new WaitForSecondsRealtime(0.3f);
//         canSkip = true;
//     }

//     public void EndDialogue()
//     {
//         StopAllCoroutines();
//         isDialogueActive = false;
//         dialogueText.SetText("");
//         dialoguePanel.SetActive(false);
        
//         // Unpause the game
//         PauseController.SetPause(false);
        
//         Playermovement.canMove = true; 

//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;
        
//         if (UnityEngine.EventSystems.EventSystem.current != null)
//             UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
//     }
// }




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; 

// public class NPC : MonoBehaviour, IInteractable
// {
//     public NPCDialogue dialogueData;
//     public GameObject dialoguePanel;
//     public TMP_Text dialogueText, nameText;
//     public Image portraitImage;

//     private int dialogueIndex;
//     private bool isTyping, isDialogueActive;

//     public bool CanInteract()
//     {
//         return !isDialogueActive;
//     }


//     public void Interact()
//     {
//         //if no dialogue data or the game is paused and no dialogue is active 
//         if(dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
//             return; 
        
//         if (isDialogueActive)
//         {
//            NextLine();
//         }
//         else
//         {
//             StartDialogue();
//         }
//     }

//     void StartDialogue()
//     {
//         isDialogueActive = true;
//         dialogueIndex = 0;

//         nameText.SetText(dialogueData.npcName);
//         portraitImage.sprite = dialogueData.npcPortrait;
//     // Clear the text box so it's empty before typing starts
//         dialogueText.SetText(""); 

//         dialoguePanel.SetActive(true);
//         PauseController.SetPause(true);

//         // This IS the correct place to trigger the first line
//         StartCoroutine(TypeLine());
//     }

//     void NextLine()
//     {
//         if (isTyping)
//         {
//             StopAllCoroutines();
//             dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//             isTyping = false;
//         }
//         else if (++dialogueIndex < dialogueData.dialogueLines.Length)
//         {
//             //if another line, type next line
//             StartCoroutine(TypeLine());
//         }
//         else
//         {
//             EndDialogue();
//         } 
//     }

//     IEnumerator TypeLine()
//     {
//         isTyping = true;
//         dialogueText.SetText("");

//         foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(dialogueData.typingSpeed); 
//         }

//         isTyping = false;

//         if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
//         {
//             yield return new WaitForSeconds(dialogueData.autoProgressDelay);
//             NextLine();
//         }

//     }

//     public void EndDialogue()
//     {
//         StopAllCoroutines();
//         isDialogueActive = false;
//         dialogueText.SetText("");
//         dialoguePanel.SetActive(false);
//         PauseController.SetPause(false);

//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;
        
//         if (UnityEngine.EventSystems.EventSystem.current != null)
//             UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

//         Playermovement.canMove = true;
//     }


// }
