using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();

    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public AnimationData[] playerAnimations; 

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position; //set direction
        while (positionDifference.magnitude > moveAccuracy) //stop when near the position
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null; //move in direction frame after frame
        }
        myObject.position = point;

        if(myObject == FindObjectOfType<ClickManager>().player)
            FindObjectOfType<ClickManager>().playerWalking = false; 
        yield return null;
    }   
}

//1