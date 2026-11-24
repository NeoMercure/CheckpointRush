using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private TMP_InputField IFName;
    [SerializeField] private TMP_InputField IFSurname;
    [SerializeField] private TMP_InputField IFEmail;
    [SerializeField] private TMP_InputField IFPhone;
    [SerializeField] private TMP_Dropdown DdGenderType;

    private PlayerData PlayerDataInfo { get; set; }

    private void Awake()
    {
        PlayerDataInfo = new PlayerData();
        PlayerDataInfo.Load();
    }

    // Buttons functions
    public void SaveButton()
    {
        PlayerDataInfo.PlayerName = IFName.text;
        PlayerDataInfo.SurnameStr = IFSurname.text;
        PlayerDataInfo.EmailStr = IFEmail.text;
        PlayerDataInfo.PhoneNumberStr = IFPhone.text;
        PlayerDataInfo.GenderType = (GenderType) DdGenderType.value;
    }

    public void LoadButton()
    {
        PlayerDataInfo = PlayerDataInfo.Load();
        IFName.text = PlayerDataInfo.PlayerName;
        IFSurname.text = PlayerDataInfo.SurnameStr;
        IFEmail.text = PlayerDataInfo.EmailStr;
        IFPhone.text = PlayerDataInfo.PhoneNumberStr;
        DdGenderType.value = (int)PlayerDataInfo.GenderType;
    }
}
