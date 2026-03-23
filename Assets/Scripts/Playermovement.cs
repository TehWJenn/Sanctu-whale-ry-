using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    Vector2 lastClickedPos;
    bool moving;

    // Added the Animator reference called 'player'
    Animator player;
    Vector3 initialScale;

    private void Awake()
    {
        // Link the animator component
        player = GetComponent<Animator>();
        // Store the scale so the player stays the correct size when flipping
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;

            // Flip logic using initialScale to prevent the "growing" bug
            if (lastClickedPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }
            else if (lastClickedPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            }
        }

        // Movement logic
        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            
            // Tell the 'player' component to play the walk animation
            player.SetBool("isWalking", true);
        }
        else
        {
            moving = false;
            // Tell the 'player' component to play the idle animation
            player.SetBool("isWalking", false);
        }
    } 

   public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
    
    }
}

//Part2 
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Playermovement : MonoBehaviour
// {
//     [SerializeField] float speed = 2f;
//     Vector2 lastClickedPos;
//     bool moving;

//     // Added Animator reference
//     Animator animator;

//     private void Awake()
//     {
//         animator = GetComponent<Animator>();
//     }

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             moving = true;

//             // Flip logic
//             if (lastClickedPos.x < transform.position.x)
//             {
//                 transform.localScale = new Vector3(-1, 1, 1);
//             }
//             else if (lastClickedPos.x > transform.position.x)
//             {
//                 transform.localScale = new Vector3(1, 1, 1);
//             }
//         }

//         // Check if we are still far enough from the target to keep moving
//         if (moving && Vector2.Distance(transform.position, lastClickedPos) > 0.05f)
//         {
//             float step = speed * Time.deltaTime;
//             transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            
//             // Tell the animator we are walking
//             animator.SetBool("isWalking", true);
//         }
//         else
//         {
//             moving = false;
//             // Tell the animator we have stopped
//             animator.SetBool("isWalking", false);
//         }
//     } 
// }









//BASE CODE
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Playermovement : MonoBehaviour
// {
// float speed = 2f;
//    Vector2 lastClickedPos;

//    bool moving;

//    private void Update()
//     {
//         if (Input.GetMouseButtonDown(0)){
//             lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             moving = true;
//         }

//         if (moving && (Vector2)transform.position!= lastClickedPos){
//             float step = speed * Time.deltaTime;
//             transform.position = Vector2.MoveTowards(transform.position, lastClickedPos,
//             step);
//         }else{
//             moving = false;
//         }

//     } 
// }

  
  


