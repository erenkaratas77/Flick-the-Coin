using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using UnityEngine;

public static class DataController
{
    static string saveName = "savedGame";
    static string path = Application.persistentDataPath;
    public static SaveGameData gameData;

    public static void Save()
    {
        // To save in a directory, it must be created first
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // The formatter will convert our unity data type into a binary file
        BinaryFormatter formatter = new BinaryFormatter();

        // Choose the save location
        FileStream saveFile = File.Create(path + "/" + saveName + ".bin");

        // Write our C# Unity game data type to a binary file
        formatter.Serialize(saveFile, gameData);

        saveFile.Close();

    }

    public static void Load()
    {
        if (GameManager.FirstTimeLoad == 0)
        {
            FirstTimeLoad();
        }
        Debug.Log(path);
        // Converts binary file back into readable data for Unity game
        BinaryFormatter formatter = new BinaryFormatter();

        // Choosing the saved file to open
        FileStream saveFile = File.Open(path + "/" + saveName + ".bin", FileMode.Open);

        // Convert the file data into SaveGameData format for use in game
        SaveGameData loadData = (SaveGameData)formatter.Deserialize(saveFile);

        // Print all of the data (normally you would feed this data into other loaded objects that need it like the Player script)

        gameData = loadData;
        //Debug.Log("~~~ LOADED GAME DATA ~~~");

        saveFile.Close();
        if (GameManager.FieldCount < gameData.GetType().GetFields().Length)
        {
            SyncNewData();
        }

    }
    static void FirstTimeLoad()
    {
        GameManager.FirstTimeLoad++;
        gameData = Resources.Load<DataBases>("Scriptables/DataBases").baseDatas;
        GameManager.FieldCount = gameData.GetType().GetFields().Length;
        Save();
    }
    static void SyncNewData()
    {
        int temp = GameManager.FieldCount;
        GameManager.FieldCount = gameData.GetType().GetFields().Length;
        SaveGameData baseDatas = Resources.Load<DataBases>("Scriptables/Databases").baseDatas;
        SaveGameData updatedData = new SaveGameData();

        // Box the value once
        object boxed = updatedData;

        for (int i = 0; i < updatedData.GetType().GetFields().Length; i++)
        {
            if (i < temp)
            {
                updatedData.GetType().GetFields()[i].SetValue(boxed, gameData.GetType().GetFields()[i].GetValue(gameData));
            }
            else
            {
                updatedData.GetType().GetFields()[i].SetValue(boxed, baseDatas.GetType().GetFields()[i].GetValue(baseDatas));
            }
        }
        updatedData = (SaveGameData)boxed;
        gameData = updatedData;
        Save();
    }
}