using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    // 2 : Data instance
    public PlayerInfoData playerInfoDataInstance;

    // 3 : Initialize the nested data class
    public PlayerData()
    {
        playerInfoDataInstance = new PlayerInfoData();
    }

    // 4 : Getter and setter with automatic save
    public string PlayerName
    {
        get { return playerInfoDataInstance.name; }
        set
        {
            playerInfoDataInstance.name = value;
            Save();
        }
    }

    public string SurnameStr
    {
        get { return playerInfoDataInstance.surname; }
        set
        {
            playerInfoDataInstance.surname = value;
            Save();
        }
    }

    public string EmailStr
    {
        get { return playerInfoDataInstance.email; }
        set
        {
            playerInfoDataInstance.email = value;
            Save();
        }
    }

    public string PhoneNumberStr
    {
        get { return playerInfoDataInstance.phoneNumber; }
        set
        {
            playerInfoDataInstance.phoneNumber = value;
            Save();
        }
    }

    public GenderType GenderType
    {
        get { return playerInfoDataInstance.genderType; }
        set
        {
            playerInfoDataInstance.genderType = value;
            Save();
        }
    }

    // 5 : File Path
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
}

public enum GenderType
{
    Woman,
    Man,
    Other,
}