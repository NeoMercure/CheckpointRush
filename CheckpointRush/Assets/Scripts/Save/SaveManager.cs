using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;

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
        // ? : Encrypt raw data
        data = EncryptionData(data);
        // 7 : Write data to the file
        File.WriteAllText(path, data);
    }

    // 10 : Return data read from file
    public string LoadFromFile(string path)
    {
        string loadData = string.Empty;

        // 10.5 : Check if the file exists
        if (File.Exists(path))
        {
            loadData = File.ReadAllText(path);
            Debug.Log($"Get data: {loadData}");
            loadData = DecryptionData(loadData);
            return loadData;
        }

        Debug.LogError("No File founds in " + path);
        return loadData;
    }

    // Encryption
    private const string KEY = "#nfhX@Dswf1";
    private string EncryptionData(string data)
    {
        byte[] byteData = Encoding.UTF8.GetBytes(data);
        using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new())
        {
            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
            using (TripleDES triple = TripleDES.Create())
            {
                triple.Key = key;
                triple.Mode = CipherMode.ECB;
                triple.Padding = PaddingMode.PKCS7;

                ICryptoTransform cryptoTransform = triple.CreateEncryptor();
                byte[] result = cryptoTransform.TransformFinalBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
    }

    // Decryption
    private string DecryptionData(string data)
    {
        if (data == string.Empty)
        {
            return data;
        }

        byte[] byteData = Convert.FromBase64String(data);
        using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new())
        {
            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
            using (TripleDES triple = TripleDES.Create())
            {
                triple.Key = key;
                triple.Mode = CipherMode.ECB;
                triple.Padding = PaddingMode.PKCS7;

                ICryptoTransform cryptoTransform = triple.CreateDecryptor();
                byte[] result = cryptoTransform.TransformFinalBlock(byteData, 0, byteData.Length);
                return Encoding.UTF8.GetString(result);
            }
        }
    }
}