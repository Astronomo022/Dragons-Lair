using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {Idle, Walking}

    public Rigidbody2D body; // Set a rigidbody in this field
    public Animator animator; // allows a reference to an animator for state machine
    PlayerState state;
    private Vector2 input; 
    public float speed;
    float xInput;
    float yInput;
    bool stateComplete;
    bool moving;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); // player controls
        /* This is a more direct approach that can change the movement. If the above feels wrong, we can remove it. 
        Vector2 direction = new Vector2(xInput,yInput).normalized;
        body.velocity = direction * speed;
        */
        if(stateComplete) // runs if true
        {
            SelectState(); // allows a new state to be chosen
        }
        UpdateState(); // updates the state
    }

    void CheckInput()
    {
        // x and y axis values based on the old input of unity.
        // Will move the player based on the Input Manager's key register. In this case, its default is wasd or arrow keys.
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // Movement checking so it doesnt update EVERY frame. 
        if(Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput*speed, body.velocity.y);
        }
        
        if(Mathf.Abs(yInput) > 0)
        {
            body.velocity = new Vector2(body.velocity.x, yInput*speed);
        }
        input = new Vector2(xInput, yInput).normalized;
    }
    #region State Machine
    void UpdateState() // This method for each save state that needs to be updated
    {
        switch(state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Walking:
                UpdateWalking();
                break;


        }
    }

    void UpdateIdle()
    {
        if(xInput != 0 || yInput != 0)
        {
            stateComplete = true;
        }
    }

    void UpdateWalking()
    {
        if(xInput == 0 || yInput == 0)
        stateComplete = true;
    }

    void SelectState()
    {

        stateComplete = false; // sets the state to false again for the update

        if(input.magnitude > 0.1f || input.magnitude < -0.1f) // Vector2 determines the magnitude at which the body is moving
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if(moving) // If bool is flag, sets the values to the animator's parameter values to properly animate movement
        {
            animator.SetFloat("X", xInput);
            animator.SetFloat("Y", yInput);
        }

        animator.SetBool("Moving", moving); // sends the flag to animator to determine states
       /* Did something different
        if(xInput == 0 || yInput == 0) // if theres no input, set to idle
        {
            state = PlayerState.Idle;
            StartIdle();
            
        }else{ // walking state will run unless player is idle
            state = PlayerState.Walking;
            StartWalking();
        }
        */
    }

    // Subsequent functions that actually call the animator 

    /*Redundant methods, since I chose to go another way, and for our purposes, it works, so that's all that matters. */
    void StartIdle()
    {
        
        animator.Play("Idle");
    }

    void StartWalking()
    {
       
        animator.Play("Walking");
        
    }
    #endregion
}
