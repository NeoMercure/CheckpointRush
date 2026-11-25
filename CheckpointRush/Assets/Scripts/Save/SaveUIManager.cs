using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

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

    [Space]
    [Header("Invalid Text")]
    [SerializeField] private TMP_Text nameInvalidText;
    [SerializeField] private TMP_Text surnameInvalidText;
    [SerializeField] private TMP_Text emailInvalidText;
    [SerializeField] private TMP_Text phoneNumberInvalidText;

    private PlayerData PlayerDataInstance { get; set; }
    private int currentIdx = 0;

    private readonly Regex nameRegex = new("^(?=(?:.*\\p{L}){2,})\\p{L}+(?:['’ -]\\p{L}+)*$");
    private readonly Regex emailRegex = new("([a-zA-Z0-9]{1,20})[@]([a-zA-Z0-9]{2,10})[.]([a-zA-Z0-9]{2,10})");
    private readonly Regex phoneNumberRegex = new("^(\\+(356)\\s?)?\\d{4}[\\s.-]?\\d{4}$");
    bool isNameValid = false;
    bool isSurnameValid = false;
    bool isEmailValid = false;
    bool isPhoneNumberValid = false;

    private void Awake()
    {
        PlayerDataInstance = new PlayerData();
        PlayerDataInstance.Load();

        profileText.text = "0";
    }

    private void OnEnable()
    {
        nameInputField.onEndEdit.AddListener(OnNameEndEdit);
        surnameInputField.onEndEdit.AddListener(OnSurnameEndEdit);
        emailInputField.onEndEdit.AddListener(OnEmailEndEdits);
        phoneNumberInputField.onEndEdit.AddListener(OnPhoneNumberEndEdit);
    }

    private void OnDisable()
    {
        nameInputField.onEndEdit.RemoveListener(OnNameEndEdit);
        surnameInputField.onEndEdit.RemoveListener(OnSurnameEndEdit);
        emailInputField.onEndEdit?.RemoveListener(OnPhoneNumberEndEdit);
        phoneNumberInputField.onEndEdit.RemoveListener(OnPhoneNumberEndEdit);
    }

    private void OnNameEndEdit(string arg0)
    {
        bool isValid = nameRegex.IsMatch(arg0);
        isNameValid = isValid;
        nameInvalidText.enabled = !isValid;
    }

    private void OnSurnameEndEdit(string arg0)
    {
        bool isValid = nameRegex.IsMatch(arg0);
        isSurnameValid = isValid;
        surnameInvalidText.enabled = !isValid;
    }

    private void OnEmailEndEdits(string arg0)
    {
        bool isValid = emailRegex.IsMatch(arg0);
        isEmailValid = isValid;
        Debug.Log($"<color=cyan>Email : {isEmailValid}</color>");
        emailInvalidText.enabled = !isValid;
    }

    private void OnPhoneNumberEndEdit(string arg0)
    {
        bool isValid = phoneNumberRegex.IsMatch(arg0);
        isPhoneNumberValid = isValid;
        Debug.Log($"<color=magenta>Phone {isPhoneNumberValid}");
        phoneNumberInvalidText.enabled = !isValid;
    }

    private bool IsAllValid()
    {
        if (isNameValid == false || isSurnameValid == false || isEmailValid == false || isPhoneNumberValid == false)
        {
            return false;
        }
        return true;
    }

    // Buttons functions
    public void SaveButton()
    {
        if (IsAllValid())
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
        else
        {
            Debug.Log($"<color=red> Y'a au mois un faux</color>");
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
        PlayerInfoData profile = DefaultValues();
        PlayerDataInstance.Add(profile);
        currentIdx = PlayerDataInstance.playerInfoDataInstance.Count - 1;

        UpdateInfos();
    }

    public void DeleteProfileAtIdx()
    {
        PlayerDataInstance.playerInfoDataInstance.RemoveAt(currentIdx);
        currentIdx--;
        UpdateInfos();
    }

    private PlayerInfoData DefaultValues()
    {
        PlayerInfoData profile = new(string.Empty, string.Empty, string.Empty, string.Empty, GenderType.Woman);

        return profile;
    }
}