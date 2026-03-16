using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform player;
    GameManager gameManager; 

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

   public void GoToItem(ItemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);//call animation
        playerWalking = true; 
        TryGettingItem(item);
        StartCoroutine(UpdateSceneAfterAction(item));
    }


    private void TryGettingItem(ItemData item)
    {
        if(item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
            
            
        }
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item)
    {
        //wait for player reaching target
        while(playerWalking)
            yield return new WaitForSeconds(0.05f);
        foreach(GameObject g in item.objectsToRemove)
            Destroy(g);
        player.GetComponent<SpriteAnimator>().PlayAnimation(null); 
        Debug.Log("item collected");
        yield return null; 
    }
}
