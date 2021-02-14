using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CotcSdk;
using TMPro;
using UnityEngine;

public class LoginAnonyme : MonoBehaviour
{
    [SerializeField] private Canvas canvasLogin;
    [SerializeField] private Canvas canvasSuccedLogin;
    private Cloud Cloud;
    private void Start()
    {
        Startup();
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
                    canvasLogin.gameObject.SetActive(false);
                    canvasSuccedLogin.gameObject.SetActive(true);
                }, ex => {
                    // The exception should always be CotcException
                    CotcException error = (CotcException)ex;
                    Debug.LogError("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                });
        });  
    }
}
