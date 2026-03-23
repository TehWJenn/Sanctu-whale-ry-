using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable  interactableInRange = null; //closet interactable
    public GameObject interactionIcon;
        void Start()
    {
        interactionIcon.SetActive (false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            interactableInRange?.Interact();
        }
    }


    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

     private void OnTriggerExit2D(Collider2D collison)
    {
        if(collison.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
