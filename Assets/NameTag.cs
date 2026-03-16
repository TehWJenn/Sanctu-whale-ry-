using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using System.Collections.Generic;

public class NameTag : MonoBehaviour
{
    Vector2 resolution, resolutionInWorldUnits = new Vector2(17.8f,10); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolution = new Vector2 (Screen.width, Screen.height); 
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse(); 
    }

    private void FollowMouse()
    {
        
        transform.position = Input.mousePosition/resolution*resolutionInWorldUnits; 
    }
}
