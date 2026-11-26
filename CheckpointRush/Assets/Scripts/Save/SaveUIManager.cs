using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Unity.VisualScripting;
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
    [SerializeField] private TMP_Dropdown countryDropDown;

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
        nameInputField.onValueChanged.AddListener(OnNameValueChanged);
        surnameInputField.onValueChanged.AddListener(OnSurnameValueChanged);
        emailInputField.onValueChanged.AddListener(OnEmailValueChanged);
        countryDropDown.onValueChanged.AddListener(OnCountryValueChange);
        phoneNumberInputField.onValueChanged.AddListener(OnPhoneNumberValueChanged);
    }

    private void OnDisable()
    {
        nameInputField.onValueChanged.RemoveListener(OnNameValueChanged);
        surnameInputField.onValueChanged.RemoveListener(OnSurnameValueChanged);
        emailInputField.onValueChanged?.RemoveListener(OnPhoneNumberValueChanged);
        countryDropDown.onValueChanged.RemoveListener(OnCountryValueChange);
        phoneNumberInputField.onValueChanged.RemoveListener(OnPhoneNumberValueChanged);
    }

    private void OnNameValueChanged(string arg0)
    {
        bool isValid = nameRegex.IsMatch(arg0);
        isNameValid = isValid;
        nameInvalidText.enabled = !isValid;
    }

    private void OnSurnameValueChanged(string arg0)
    {
        bool isValid = nameRegex.IsMatch(arg0);
        isSurnameValid = isValid;
        surnameInvalidText.enabled = !isValid;
    }

    private void OnEmailValueChanged(string arg0)
    {
        bool isValid = emailRegex.IsMatch(arg0);
        isEmailValid = isValid;
        emailInvalidText.enabled = !isValid;
    }

    private void OnCountryValueChange(int arg0)
    {
        bool isValid = RightRegexPatern((CountryCode)arg0).IsMatch(phoneNumberInputField.text);
        isPhoneNumberValid = isValid;
        phoneNumberInvalidText.enabled = !isValid;
    }

    private void OnPhoneNumberValueChanged(string arg0)
    {
        CountryCode countryValue = (CountryCode)countryDropDown.value;

        bool isValid = RightRegexPatern(countryValue).IsMatch(arg0);
        isPhoneNumberValid = isValid;
        phoneNumberInvalidText.enabled = !isValid;
    }

    private Regex RightRegexPatern(CountryCode country)
    {
        Regex regex = phoneNumberRegex;

        switch (country)
        {
            case CountryCode.None:
                regex = new("^\\+(?:[1-9]\\d{0,2})(?:[\\s.-]?\\d{2,3}){2,5}$");
                break;
            case CountryCode.Malta:
                regex = new("^\\d{4}[\\s.-]?\\d{4}$");
                break;
            case CountryCode.France:
                regex = new("^\\d{1,2}(?:[\\s.-]?\\d{2}){4}$");
                break;
            default:
                break;
        }

        return regex;
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
            PlayerInfoData profile = new(nameInputField.text, surnameInputField.text, emailInputField.text, (CountryCode)countryDropDown.value, phoneNumberInputField.text, (GenderType)genderTypeDropdown.value);

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
            nameInvalidText.enabled = isNameValid;
            surnameInvalidText.enabled = isSurnameValid;
            emailInvalidText.enabled = isEmailValid;
            phoneNumberInvalidText.enabled = isPhoneNumberValid;
            Debug.Log($"<color=red>Can't save with invalid info</color>");
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
        countryDropDown.value = (int)profile.countryCode;
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
        PlayerInfoData profile = new(string.Empty, string.Empty, string.Empty, CountryCode.None, string.Empty, GenderType.Woman);

        return profile;
    }
}