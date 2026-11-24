using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Singleton
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Instance = FindAnyObjectByType<SaveManager>();
            }

            return _instance;
        }

        set
        {
            if (_instance != null)
            {
                return;
            }
            _instance = value;
        }
    }

    private void Awake()
    {
        Instance.GetComponent<SaveManager>();
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Singleton Error. Found duplicates of type: {GetType()} on {Instance.name} & {name}");
        }
    }

    //________________________________________
    public void Save(string path, string data)
    {
        // 7 : Write data to the file
        File.WriteAllText(path, data);
        Debug.Log("Yes SAVE");
    }

    // 10 : Return data read from file
    public string LoadFromFile(string path)
    {
        string loadData = string.Empty;

        // 10.5 : Check if the file exists
        if (File.Exists(path))
        {
            Debug.Log("Get data");
            loadData = File.ReadAllText(path);
            Debug.Log($"Get data: {loadData}");
            return loadData;
        }

        Debug.LogError("No File founds in " + path);
        return loadData;
    }
}