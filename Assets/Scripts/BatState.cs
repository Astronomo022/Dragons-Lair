using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatState : MonoBehaviour
{
    enum NPCState {BatIdle}

    public Rigidbody2D body; // Set a rigidbody in this field
    public Animator animator; // allows a reference to an animator for state machine

    public string Scene2Load;
    NPCState state; // redundant just in case


    

/*    void StartIdle()
    {
        // We just want this guy to stay still and play an animation, forever and ever. 
        animator.Play("Idle");
    }
    */

    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            StartCoroutine(CombatTransition());
        }

    }

    IEnumerator CombatTransition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Scene2Load);
    }
}
