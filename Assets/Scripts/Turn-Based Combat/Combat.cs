using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// enum since we need to define different states in battle, and when to begin or end it. 
public enum CombatState{START, PLAYERTURN, ENEMYTURN, WON, LOST} 
public class CombatRun : MonoBehaviour
{
    // These game objects to slide in existing objects in the scene 
    public GameObject player;
    public GameObject foe1, foe2, foe3;

    // public transforms that may need to change depending on the sprite's size. Sets the sprite at a specific position
    public Transform foePos1, foePos2, foePos3;

    public CombatState state; // reference variable to enum

    // Lets us collect a reference to the actual objects we want
    Fighter playerCombatant;
    Fighter enemyCombatant1, enemyCombatant2, enemyCombatant3;

    // Reference to the huds
    public BattleHUD knightHUD;
    public BattleHUD enemyHUD1, enemyHUD2, enemyHUD3;

    // Reference to the dialogue text object we're gonna use to update the text
    public TMP_Text dialogueText;

    bool PlayerisDead, Foe1isDead, Foe2isDead, Foe3isDead;
    void Start()
    {
        state = CombatState.START;
        StartCoroutine(SetMatch());
    }

    // The meat and potatoes of the machine. It sets up the scene with each fighter, each position, each hud. 
    IEnumerator SetMatch()
    {
        // each block for each opponent's instantiation 
        GameObject playerGO = Instantiate(player);
        playerCombatant = playerGO.GetComponent<Fighter>();

        GameObject foe1GO = Instantiate(foe1, foePos1);
        enemyCombatant1 = foe1GO.GetComponent<Fighter>();

        GameObject foe2GO = Instantiate(foe2, foePos2);
        enemyCombatant2 = foe2GO.GetComponent<Fighter>();

        GameObject foe3GO = Instantiate(foe3, foePos3);
        enemyCombatant3 = foe3GO.GetComponent<Fighter>();

        // Sets the dialogue text to this output
        dialogueText.text = "Ambushed by a " + enemyCombatant1.fightName + " and their goons, " + enemyCombatant2.fightName + " and " + enemyCombatant3.fightName + "!";

        // This sends the Fighter type unit to BattleHud to read into the UI, once again, done in blocks.
        knightHUD.SetPlayerHud(playerCombatant);

        enemyHUD1.SetEnemyHud(enemyCombatant1);
        enemyHUD2.SetEnemyHud(enemyCombatant2);
        enemyHUD3.SetEnemyHud(enemyCombatant3);

        // yield return to start a coroutine so that there is a palpable time to wait in code
        yield return new WaitForSeconds(2);

        state = CombatState.PLAYERTURN;
        PlayerTurn();




    }

    void PlayerTurn()
    {
        if(Foe1isDead == true && Foe2isDead == true && Foe3isDead == true)
        {
            state = CombatState.WON;
            EndBattle();
        }
        else
        dialogueText.text = "What are you going to do?(Choose on the left)";
    }


/* NOTE: This is very much inefficient but as of now I am running out of time and options so screw the semantics and its now your problem
(Whoever decides to work on this in the future, probably me again but still, READ.)*/
// Duplicated AttackFoe1Button so that each targetting option can be accurate. 

    public void AttackFoe1Button()
    {
        // checking to make sure player only attacks when its their turn
        if (state != CombatState.PLAYERTURN)
            StartCoroutine(ScoldPlayer());
        
        // Coroutine once again so player can process attack
        StartCoroutine(PlayerFoe1Attack());



    }

    public void AttackFoe2Button()
    {
        // checking to make sure player only attacks when its their turn
        if (state != CombatState.PLAYERTURN)
            StartCoroutine(ScoldPlayer());
        
        // Coroutine once again so player can process attack
        StartCoroutine(PlayerFoe2Attack());



    }

    public void AttackFoe3Button()
    {
        // checking to make sure player only attacks when its their turn
        if (state != CombatState.PLAYERTURN)
            StartCoroutine(ScoldPlayer());
        
        // Coroutine once again so player can process attack
        StartCoroutine(PlayerFoe3Attack());



    }

    //

    IEnumerator ScoldPlayer() // this function doesnt seem to work in the way I want it to
    {
        dialogueText.text = "Wait your turn!!";
        yield return new WaitForSeconds(2);
        PlayerTurn();
    }

    IEnumerator PlayerFoe1Attack()
    {
        Foe1isDead = enemyCombatant1.TakeDamage(playerCombatant.damage); // take damage and check if they're dead
        enemyHUD1.SetHP(enemyCombatant1.currentHp, enemyCombatant1.maxHp); // set enemy hp in UI
        dialogueText.text = "You attack " + enemyCombatant1 + "!";
        yield return new WaitForSeconds(1); 
        Debug.Log("Successful.");

        // checks if enemy is dead
        if(Foe1isDead)
        {
            dialogueText.text = enemyCombatant1 + " Perishes with a final strike! You take this surge of energy to go again!";
            yield return new WaitForSeconds(1);
            foe1.SetActive(false); // makes the foe that died disappear
            foePos1.gameObject.SetActive(false); // Put here since we run into the issue where for some reason sometimes prefabs dont disappear
            enemyHUD1.gameObject.SetActive(false); // also hiding hud but can still be selected. Shouldn't break the game, however. 
            state = CombatState.PLAYERTURN;
            PlayerTurn();

        }
        else 
        {
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerFoe2Attack()
    {
        Foe2isDead = enemyCombatant2.TakeDamage(playerCombatant.damage); // take damage and check if they're dead
        enemyHUD2.SetHP(enemyCombatant2.currentHp, enemyCombatant2.maxHp); // set enemy hp in UI
        dialogueText.text = "You attack " + enemyCombatant2 + "!";
        yield return new WaitForSeconds(1); 
        Debug.Log("Successful.");

        // checks if enemy is dead
        if(Foe2isDead)
        {
            dialogueText.text = enemyCombatant2 + " Perishes with a final strike! You take this surge of energy to go again!";
            yield return new WaitForSeconds(1);
            foe2.SetActive(false); // makes the foe that died disappear
            foePos2.gameObject.SetActive(false); // Put here since we run into the issue where for some reason sometimes prefabs dont disappear
            enemyHUD2.gameObject.SetActive(false); // also hiding hud but can still be selected. Shouldn't break the game, however. 
            state = CombatState.PLAYERTURN;
            PlayerTurn();

        }
        else 
        {
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerFoe3Attack()
    {
        Foe3isDead = enemyCombatant3.TakeDamage(playerCombatant.damage); // take damage and check if they're dead
        enemyHUD3.SetHP(enemyCombatant3.currentHp, enemyCombatant3.maxHp); // set enemy hp in UI
        dialogueText.text = "You attack " + enemyCombatant3 + "!";
        yield return new WaitForSeconds(1); 
        Debug.Log("Successful.");

        // checks if enemy is dead
        if(Foe3isDead)
        {
            dialogueText.text = enemyCombatant3 + " Perishes with a final strike! You take this surge of energy to go again!";
            yield return new WaitForSeconds(1);
            foe3.SetActive(false); // makes the foe that died disappear
            foePos3.gameObject.SetActive(false); // Put here since we run into the issue where for some reason sometimes prefabs dont disappear
            enemyHUD3.gameObject.SetActive(false); // also hiding hud but can still be selected. Shouldn't break the game, however. 
            state = CombatState.PLAYERTURN;
            PlayerTurn();

        }
        else 
        {
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    

    IEnumerator EnemyTurn() // This is making think that I should've made this an array but perhaps for another time
    {
        if(Foe1isDead == true && Foe2isDead == true && Foe3isDead == true)
        {
            state = CombatState.WON;
            EndBattle();
        }
        if (Foe1isDead) // I'm too tired right now to go any farther than this, and its just going to make this program more needlessly complicated. 
        {
            dialogueText.text = "They attack you like mindless chickens! ";

            yield return new WaitForSeconds(2);

            // Take damage 2 times.
            PlayerisDead = playerCombatant.TakeDamage(enemyCombatant2.damage);
            PlayerisDead = playerCombatant.TakeDamage(enemyCombatant3.damage);

            // Update player hud
            knightHUD.SetHP(playerCombatant.currentHp, playerCombatant.maxHp);

            //Checks if player is dead.
            if(PlayerisDead)
            {
                state = CombatState.LOST;
                EndBattle();
            }
            else
            {
                state = CombatState.PLAYERTURN;
                PlayerTurn();
            }





        }
        if (!Foe1isDead)
        {
        dialogueText.text = "They attack you with their full force! ";

            yield return new WaitForSeconds(2);

            // Take damage 3 times.
            PlayerisDead = playerCombatant.TakeDamage(enemyCombatant1.damage);
            PlayerisDead = playerCombatant.TakeDamage(enemyCombatant2.damage);
            PlayerisDead = playerCombatant.TakeDamage(enemyCombatant3.damage);

            // Update player hud
            knightHUD.SetHP(playerCombatant.currentHp, playerCombatant.maxHp);

            //Check if player is dead. 
            if(PlayerisDead)
            {
                state = CombatState.LOST;
                EndBattle();
            }
            else
            {
                state = CombatState.PLAYERTURN;
                PlayerTurn();
            }

        }
        

    }

    void EndBattle()
    {
        // Checks your state at the end of battle.
        if(state == CombatState.WON) 
        {
            dialogueText.text = "This battle has been won!";
            StartCoroutine(OverworldReturn());
        } else if (state == CombatState.LOST)

        {
            dialogueText.text = "You have succumbed to weakness. This battle is lost.";
            StartCoroutine(GameOverReturn());
        }
    }

    IEnumerator OverworldReturn()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LairDungeon");
    }

    IEnumerator GameOverReturn()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }

    public void RunAway()
    {
        SceneManager.LoadScene("LairDungeon");
    }
}
