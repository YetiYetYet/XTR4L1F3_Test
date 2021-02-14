using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CheckValidInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_Text wrongMailInputText;
    [SerializeField] private TMP_Text wrongPasswordInputText;
    [SerializeField] private Button LoginButton;
    public bool goodMailInput;
    public bool goodPasswordInput;
    
    
    
    // Regular expression, which is used to validate an E-Mail address.
    [TextArea, SerializeField] private string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    
    // Minimum eight characters, at least one letter and one number:
    [TextArea, SerializeField] private string MatchPasswordPattern =
        @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

    public void Start()
    {
        emailInputField.text = "";
        passwordInputField.text = "";
        wrongMailInputText.gameObject.SetActive(false);
        wrongPasswordInputText.gameObject.SetActive(false);
        LoginButton.interactable = false;
    }

    // Start is called before the first frame update
    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address and write error if not valid.
    /// </summary>
    /// <returns>True, when Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public void CheckMailEntry()
    {
        string email = emailInputField.text;
        bool isValid = email != null && Regex.IsMatch(email, MatchEmailPattern);
        wrongMailInputText.gameObject.SetActive(!isValid);
        goodMailInput = isValid;
        SwitchLoginButton();
    }
    
    /// <summary>
    /// Checks whether the given password-Parameter is a valid and write error if not valid.
    /// </summary>
    /// <returns>True, when Parameter-string is not null and contains a password;
    /// otherwise false.</returns>
    public void CheckPassWordEntry()
    {
        string password = passwordInputField.text;
        bool isValid = password != null && Regex.IsMatch(password, MatchPasswordPattern);
        wrongPasswordInputText.gameObject.SetActive(!isValid);
        goodPasswordInput = isValid;
        SwitchLoginButton();
    }

    public void OnSelectMail()
    {
        wrongMailInputText.gameObject.SetActive(false);
    }
    
    public void OnSelectPassword()
    {
        wrongPasswordInputText.gameObject.SetActive(false);
    }

    public void SwitchLoginButton()
    {
        LoginButton.interactable = goodMailInput && goodPasswordInput;
    }
}
