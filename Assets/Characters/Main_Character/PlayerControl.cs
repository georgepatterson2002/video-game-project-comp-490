using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Animator animator;
    private string currentState;
    private bool facingRight = true;

    //Animation States
    const string idle_animation = "idle";
    const string down_walk_animation = "run";


    public float movementSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.Play(idle_animation);
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;

        //Prevent Diagonal Movement
        if (speedX != 0) {
            speedY = 0;
        }

        if (speedX > 0 && !facingRight)
        {
            Flip();
        }
        // If moving left and facing right, flip the character
        else if (speedX < 0 && facingRight)
        {
            Flip();
        }


        //Animation Switches
        if (speedX == 0 && speedY == 0)
        {
            ChangeAnimationState(idle_animation);

        } else
        {
            ChangeAnimationState(down_walk_animation);
        }

        rb.velocity = new Vector2(speedX, speedY);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    void Flip()
    {
        // Flip the character direction
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;  // Multiply x scale by -1 to flip
        transform.localScale = theScale;
    }
}
