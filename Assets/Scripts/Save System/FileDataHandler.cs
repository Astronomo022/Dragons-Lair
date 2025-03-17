using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = ""; // path of where you want to save that data.
    private string dataFileName = ""; // name of the file to save to.

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // Path.Combine accounts for different OSes, not that we'd be expecting them, but efficiency is nice.
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {   string dataToLoad = "";
                // This will take in the file and open it
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); // loads the file's text into dataToLoad
                    }
                    
                }
                // deserializing the JSON back into C# Object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to save data to a file: " + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // Hey, you saw this before!
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        // try catch because writing to files are not foolproof
        try
        {
            // Makes the directory the file will be written to, if it doesn't exist.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //True serializes the gamedata object into JSON
            string dataToStore = JsonUtility.ToJson(data,true);

            //the final step, writing to file.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // ok so I promise this is it
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to a file: " + fullPath + "\n" + e);
        }
    }

}
