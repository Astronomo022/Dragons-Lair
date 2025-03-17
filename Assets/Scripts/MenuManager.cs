using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{   // Shows in inspector 
    [Header("Volume Control")]
    [SerializeField] private TMP_Text volumeTextValue = null; // changes the value on slider
    [SerializeField] private Slider volumeSlider = null; // allows us to pick a slider. 
    [SerializeField] private float defaultMasVol = 0.7f; // default volume for the audiolistener on Master Volume
    [Header("Confirmation")]
    [SerializeField] private GameObject applyPrompt = null; 
    
    [Header("Scene Management")]
    public string newGameScene; // We can change the scene we want to use in inspector
    //write more as it comes

    


    public void NewGameLoad()
    {
        
        SceneManager.LoadScene(newGameScene);
        DataPersistManager.instance.NewGame(); // Creating a GameData object
    }

    // Space for a Load Game function
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetMasterVol(float masVol) 
    {
        AudioListener.volume = masVol; // changes audio level in runtime
        masVol *= 100;
        volumeTextValue.text = masVol.ToString("0"); // formatting text value to decimal
    }

    public void MasterVolApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ApplyBox());
    }

    public void ResetOptions()
    {
        /*Code as needed, separate if sections are added.*/ 
        
        // Sets the following to the default
        AudioListener.volume = defaultMasVol;
        volumeSlider.value = defaultMasVol;
        // Code below multiplies float by 100 and then converts to string
        volumeTextValue.text = (defaultMasVol * 100).ToString("0");
        MasterVolApply();

    }
// This method causes an object to appear once the changes have been made, just to have a visual confirmation things have changed. 
    public IEnumerator ApplyBox()
    {
        applyPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        applyPrompt.SetActive(false);
    }

}
