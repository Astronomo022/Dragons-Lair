using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script to define all combatants within the turn based combat. Including the player.
public class Fighter : MonoBehaviour
{
    public string fightName;
    public int fightLevel;

    public int damage;

    public int maxHp;
    public int currentHp;

    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;   
        if(currentHp <= 0)
            return true;
        else
            return false; 
    }

}
