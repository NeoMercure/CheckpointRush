using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<PlayerInfoData> playerInfoDataInstance;

    public PlayerData()
    {
        playerInfoDataInstance = new();
    }

    public void Add(PlayerInfoData data)
    {
        playerInfoDataInstance.Add(data);
        Save();
    }

    public void RemoveAtIdx(int index)
    {
        //Index is valid index in the list, if so remove
        if (index >= 0 && index <= playerInfoDataInstance.Count)
        {
            playerInfoDataInstance.RemoveAt(index);
            Save();
        }
        else
        {
            Debug.LogError("Remove Index invalid");
        }
    }

    public void Update(int index, PlayerInfoData data)
    {
        //Index is valid index in the list
        if (index >= 0 && index <= playerInfoDataInstance.Count)
        {
            playerInfoDataInstance[index] = data;
            Save();
        }
        else
        {
            Debug.LogError("Update Index invalid");
        }
    }

    public PlayerInfoData Get(int index)
    {
        //Index is valid index in the list
        if (index >= 0 && index <= playerInfoDataInstance.Count)
        {
            return playerInfoDataInstance[index];
        }
        else
        {
            Debug.LogError("Get Index invalid");
            return null;
        }
    }

    public string FilePath
    {
        get { return Path.Combine(Application.persistentDataPath, "Save.json"); }
    }

    private void Save()
    {
        // 6 : Create rawData in JSON
        string rawData = JsonUtility.ToJson(this);
        SaveManager.Instance.Save(FilePath, rawData);
    }

    public PlayerData Load()
    {
        // 8 : Retrieve the data from the file
        string loadRawData = SaveManager.Instance.LoadFromFile(FilePath);

        // 10 : Check whether the data is empty
        if (loadRawData == string.Empty)
        {
            Debug.Log("Create new PlayerData");
            return new PlayerData();
        }

        PlayerData loadData = JsonUtility.FromJson<PlayerData>(loadRawData);
        return loadData;
    }
}

// 1 : Data class
[Serializable]
public class PlayerInfoData
{
    public string name;
    public string surname;
    public string email;
    public string phoneNumber;
    public GenderType genderType;

    public PlayerInfoData(string name, string surname, string email, string phoneNumber, GenderType genderType)
    {
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.genderType = genderType;
    }
}

public enum GenderType
{
    Woman,
    Man,
    Other,
}