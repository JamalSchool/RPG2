using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; 
    
    public bool isMoving; //to tell you if the player is moving

    public Vector2 input; //position of the player holds the x y values
    
    private Animator animator;

    private void Awake() //as soon as i load my player it will load up the animations.
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() //updates during each frame
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); //stores the keys you use in that variable such as the a key to move left it will store it in that variable
            input.y = Input.GetAxisRaw("Vertical");
                        
            Debug.Log("This is input.X"+ input.x);
            Debug.Log("This is input.Y" + input.y);

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero )
            {
                animator.SetFloat("moveX", input.x); // positions the animations for the characters.
                animator.SetFloat("moveY", input.y);

                var targetPos= transform.position; //stores a position
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));//running constantly in my game so movement is smooth.
            }
            bool flipped = input.x < 0;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        }
        animator.SetBool("isMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos- transform.position).sqrMagnitude >Mathf.Epsilon) //if the player moves a tiny bit it will know.
        {
            transform.position= Vector3.MoveTowards(transform.position, targetPos, moveSpeed *Time.deltaTime); //taking the orignal position and moving towards the target position
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
}
