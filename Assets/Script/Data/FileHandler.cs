using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class FileHandler
{
    private string pathSaveName = Path.Combine(Application.dataPath, "GameDataSave.json");

    public GameData Load()
    {
        GameData loadData = null;

        if (File.Exists(pathSaveName))
        {
            try
            {
                string saveJSON = File.ReadAllText(pathSaveName);
                loadData = JsonConvert.DeserializeObject<GameData>(saveJSON);
                //Debug.Log(JsonConvert.DeserializeObject<GameData>(saveJSON).furnitures);

                return loadData;
            }
            catch (Exception error)
            {
                //Debug.LogError("Error on Load file : " + error.Message);
                Debug.LogException(error);
            }
        }
        return loadData;
    }

    public void Save(GameData data)
    {
        try
        {
            string strSave = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Debug.Log("File Handler Save");

            File.WriteAllText(pathSaveName, strSave);
        }
        catch (Exception error)
        {
            Debug.LogError("Error on save file : " + error.Message);
        }
    }
}
