using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText; // Shows the name of the Knight, which is probably going to be Knight
    public TMP_Text levelText; // shows the player's current level
    public Slider hpGauge; // controls a visual representation of the HP gauge
    public TMP_Text healthText;
    

    public void SetPlayerHud(Fighter fighter)// Get a parameter type of the player 
    {
        // Lists the information of the fighter's stats in a panel
        nameText.text = fighter.fightName;   
        levelText.text = "LvL " + fighter.fightLevel;
        hpGauge.maxValue = fighter.maxHp;
        hpGauge.value = fighter.currentHp;
        healthText.text = fighter.currentHp + "/" + fighter.currentHp;
    }

    public void SetHP(int hp, int xhp) // parameters for current hp and max hp
    {
        hpGauge.value = hp;
        healthText.text = hp + "/" + xhp  ;
    }

    //will maybe use this later for something 
    public void SetEnemyHud(Fighter fighter)
    {
        // copying from other method just to copy gauge instead of everything
        nameText.text = fighter.fightName;   
        levelText.text = "LvL " + fighter.fightLevel;
        hpGauge.maxValue = fighter.maxHp;
        hpGauge.value = fighter.currentHp;
    }

}
