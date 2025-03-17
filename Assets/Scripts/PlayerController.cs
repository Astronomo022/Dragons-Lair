using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body; // Set a rigidbody in this field
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // x and y axis values based on the old input of unity.
        // Will move the player based on the Input Manager's key register. In this case, its default is wasd or arrow keys.
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        // Movement checking so it doesnt update EVERY frame. 
        if(Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput*speed, body.velocity.y);
        }
        
        if(Mathf.Abs(yInput) > 0)
        {
            body.velocity = new Vector2(body.velocity.x, yInput*speed);
        }
        /* This is a more direct approach that can change the movement. If the above feels wrong, we can remove it. 
        Vector2 direction = new Vector2(xInput,yInput).normalized;
        body.velocity = direction * speed;
        */
    }
}
