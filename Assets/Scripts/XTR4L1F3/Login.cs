using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CotcSdk;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    private Cloud Cloud;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    
    // Regular expression, which is used to validate an E-Mail address.
    [TextArea, SerializeField] private string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    
    // Minimum eight characters, at least one letter and one number:
    [TextArea, SerializeField] private string MatchPasswordPattern =
        @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
    
    
    private void Start()
    {
        Startup();
    }

    public bool OnPressLogin()
    {
        if (!IsEmail(emailInputField.text))
        {
            Debug.LogError("Bad email format");
            return false;
        }
        
        if (!CheckPassWordEntry(passwordInputField.text))
        {
            Debug.LogError("BadPassWordFormat");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address.
    /// </summary>
    /// <param name="email">Parameter-string that contains an E-Mail address.</param>
    /// <returns>True, when Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public bool IsEmail(string email)
    {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
    
    /// <summary>
    /// Checks whether the given password-Parameter is a valid.
    /// </summary>
    /// <param name="password">Parameter-string that contains an valid password.</param>
    /// <returns>True, when Parameter-string is not null and contains a password;
    /// otherwise false.</returns>
    public bool CheckPassWordEntry(string password)
    {
        if (password != null) return Regex.IsMatch(password, MatchPasswordPattern);
        else return false;
    }
    
    
    void Startup() {
        var cb = FindObjectOfType<CotcGameObject>();
        Debug.Log(cb ? "Find object + : " + cb.name : "Don't Find Any object of type " + cb.GetType());
        cb.GetCloud().Done(cloud => {
            Cloud = cloud;
        });
    }
    
    public void LoginAnonymous()
    {
        var cotc = FindObjectOfType<CotcGameObject>();

        cotc.GetCloud().Done(cloud => {
            cloud.LoginAnonymously()
                .Done(gamer => {
                    Debug.Log("Signed in succeeded (ID = " + gamer.GamerId + ")");
                    Debug.Log("Login data: " + gamer);
                    Debug.Log("Server time: " + gamer["servertime"]);
                }, ex => {
                    // The exception should always be CotcException
                    CotcException error = (CotcException)ex;
                    Debug.LogError("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                });
        });  
    }
    
    public void LoginNetwork()
    {
        var cotc = FindObjectOfType<CotcGameObject>();

        cotc.GetCloud().Done(cloud => {
            Cloud.Login(
                    network: "email",
                    networkId: "myEmail@gmail.com",
                    networkSecret: "myPassword")
                .Done(gamer => {
                    Debug.Log("Signed in succeeded (ID = " + gamer.GamerId + ")");
                    Debug.Log("Login data: " + gamer);
                    Debug.Log("Server time: " + gamer["servertime"]);
                }, ex => {
                    // The exception should always be CotcException
                    CotcException error = (CotcException)ex;
                    Debug.LogError("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                });
        });  
    }
}
