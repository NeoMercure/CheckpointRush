using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField surnameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField phoneNumberInputField;
    [SerializeField] private TMP_Dropdown genderTypeDropdown;

    [Space]
    [Header("Arrow Selector")]
    [SerializeField] private TMP_Text profileText;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;

    private PlayerData PlayerDataInstance { get; set; }
    private int currentIdx = 0;

    private void Awake()
    {
        PlayerDataInstance = new PlayerData();
        PlayerDataInstance.Load();

        profileText.text = "0";
    }

    // Buttons functions
    public void SaveButton()
    {
        PlayerInfoData profile = new(nameInputField.text, surnameInputField.text, emailInputField.text, phoneNumberInputField.text, (GenderType)genderTypeDropdown.value);

        if (PlayerDataInstance.playerInfoDataInstance.Count == 0)
        {
            PlayerDataInstance.Add(profile);
        }
        else
        {
            PlayerDataInstance.Update(currentIdx, profile);
        }

    }

    public void LoadButton()
    {
        PlayerDataInstance = PlayerDataInstance.Load();
        UpdateInfos();
    }

    // << |
    public void LeftArrowButton()
    {
        if (PlayerDataInstance.playerInfoDataInstance.Count == 0) return;

        currentIdx = (currentIdx - 1) % PlayerDataInstance.playerInfoDataInstance.Count;
        if (currentIdx < 0)
        {
            currentIdx = PlayerDataInstance.playerInfoDataInstance.Count - 1;
        }

        UpdateInfos();
    }

    // | >>
    public void RightArrowButton()
    {
        if (PlayerDataInstance.playerInfoDataInstance.Count == 0) return;

        currentIdx = (currentIdx + 1) % PlayerDataInstance.playerInfoDataInstance.Count;
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        PlayerInfoData profile = PlayerDataInstance.Get(currentIdx);

        nameInputField.text = profile.name;
        surnameInputField.text = profile.surname;
        emailInputField.text = profile.email;
        phoneNumberInputField.text = profile.phoneNumber;
        genderTypeDropdown.value = (int)profile.genderType;

        profileText.text = $"{currentIdx + 1} / {PlayerDataInstance.playerInfoDataInstance.Count}";
    }

    public void AddProfile()
    {
        PlayerInfoData profile = new(nameInputField.text, surnameInputField.text, emailInputField.text, phoneNumberInputField.text, (GenderType)genderTypeDropdown.value);
        PlayerDataInstance.Add(profile);
        currentIdx = PlayerDataInstance.playerInfoDataInstance.Count - 1;

        UpdateInfos();
    }
}