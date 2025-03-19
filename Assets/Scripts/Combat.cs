using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatRun : MonoBehaviour
{

    public void FightThem()
    {
        // put code here for the combat
    }
    public void RunAway()
    {
        SceneManager.LoadScene("LairDungeon");
    }
}
