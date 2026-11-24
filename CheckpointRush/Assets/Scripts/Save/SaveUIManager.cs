using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SaveUIManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private TMP_InputField IFName;
    [SerializeField] private TMP_InputField IFSurname;
    [SerializeField] private TMP_InputField IFEmail;
    [SerializeField] private TMP_InputField IFPhone;
    [SerializeField] private TMP_Dropdown DdGenderType;

    // Buttons functions
    public void SaveButton()
    {
    }

    public void LoadButton()
    {

    }
}
