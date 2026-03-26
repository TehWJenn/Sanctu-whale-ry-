using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    
    private Vector2 lastClickedPos; 
    private bool isMoving;
    private Vector3 initialScale;
    private Animator animator;

    // This static variable allows other scripts (like your NPC) to stop movement
    public static bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
        lastClickedPos = transform.position;
        canMove = true; 
    }

    void Update()
    {
        // If an interaction is happening, stop moving and don't process clicks
        if (!canMove)
        {
            isMoving = false;
            if (animator != null) animator.SetBool("isWalking", false);
            return;
        }

        // Handle the actual movement toward the target
        if (isMoving && Vector2.Distance(transform.position, lastClickedPos) > 0.1f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            if (animator != null) animator.SetBool("isWalking", true);
        }
        else
        {
            isMoving = false;
            if (animator != null) animator.SetBool("isWalking", false);
        }
    }

    // This function is triggered by the "Move" event in your Player Input component
    public void Move(InputAction.CallbackContext context)
    {
        // Only trigger if we are allowed to move and the button was JUST pressed
        if (canMove && context.performed)
        {
            // Ignore the click if we are clicking on a UI button (like "Close")
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
            
            // Flip the whale based on direction
            if (lastClickedPos.x < transform.position.x)
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            else
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }
}














// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class Playermovement : MonoBehaviour
// {
//     [SerializeField] float speed = 3f;
//     Vector2 lastClickedPos;
//     bool moving;

//     // Added the Animator reference called 'player'
//     Animator player;
//     Vector3 initialScale;

//     private void Awake()
//     {
//         // Link the animator component
//         player = GetComponent<Animator>();
//         // Store the scale so the player stays the correct size when flipping
//         initialScale = transform.localScale;
//     }

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             moving = true;

//             // Flip logic using initialScale to prevent the "growing" bug
//             if (lastClickedPos.x < transform.position.x)
//             {
//                 transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
//             }
//             else if (lastClickedPos.x > transform.position.x)
//             {
//                 transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
//             }
//         }

//         // Movement logic
//         if (moving && (Vector2)transform.position != lastClickedPos)
//         {
//             float step = speed * Time.deltaTime;
//             transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            
//             // Tell the 'player' component to play the walk animation
//             player.SetBool("isWalking", true);
//         }
//         else
//         {
//             moving = false;
//             // Tell the 'player' component to play the idle animation
//             player.SetBool("isWalking", false);
//         }
//     } 

//    public void Move(InputAction.CallbackContext context)
//     {
//         Vector2 inputVector = context.ReadValue<Vector2>();
    
//     }
// }

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

  
  


