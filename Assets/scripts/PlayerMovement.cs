using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;


    Vector2 movementInput;

    Rigidbody rb;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // creates an empty list inputted into the collisions that is fined

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // stores the type into the rigidbody
    }


    private void FixedUpdate()
    {   
        if (movementInput != Vector2.zero) // if statement that shows if no input is being made, it will remain idle.
        {   
            int count = rb.BroadcastMessage(movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions 
                castCollisions, // the settings that determine where a collision can occur on such as layers to collide with
                movementFilter, // list of collisions to store the found collisions into after the cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // the amount to cast equal to the movement plus an offset
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        } 
        
    }




    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
