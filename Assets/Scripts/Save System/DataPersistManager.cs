using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Helps to find all scripts that implement persistence in our scene

public class DataPersistManager : MonoBehaviour
{
    [Header ("File Storage Config")]
    [SerializeField] private string fileName; // the file name we want to save our data to
    private GameData gameData;
    private List<IfcDataPersist> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    
    // Singleton Instance
    public static DataPersistManager instance {get; private set;} 

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("There exists more than one Data Persistence Manager in the Scene.");
        }
        instance = this;

    }

    private void Start()
    {
        
        // persistentDataPath gives the Operating System the standard directory for persisting data in unity.
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDPObjects(); // Method to find all Data Persistent Objects
    }

    // When this method is called creates a new GameData object. 
    public void NewGame() 
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Loads any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // If there is no save data, it'll just initialize to a new game. 
        if(this.gameData == null)
        {
            Debug.Log("No save data was found, but somehow the user made an unsafe entrance. Data set to defaults.");
            NewGame();
        }

        // Takes the data to all the other scripts that need it. 
        foreach (IfcDataPersist dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Waiting to load something.");// remove these once you've gone through testing

    }

    public void SaveGame()
    {
        foreach (IfcDataPersist dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        Debug.Log("Waiting to save something."); // remove these once you've gone through testing

        //saves the data to a file
        dataHandler.Save(gameData);
    }

    private List<IfcDataPersist> FindAllDPObjects()
    {
        // Finds all scripts that implement persistence in our scene
        IEnumerable<IfcDataPersist> dataPersistentObjects = FindObjectsOfType<MonoBehaviour>().OfType<IfcDataPersist>();

        // Returns a new list, passing the result of previous call to initialize the new list
        return new List<IfcDataPersist>(dataPersistentObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
        Debug.Log("Function ran successfully.");
    }
}
