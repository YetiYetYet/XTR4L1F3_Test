using CotcSdk;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    private Cloud Cloud;
    [SerializeField] private Canvas canvasLogin;
    [SerializeField] private Canvas canvasSuccedLogin;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_Text cantLogin;
    

    private void Start()
    {
        Startup();
        cantLogin.gameObject.SetActive(false);
    }
    
    void Startup() {
        var cb = FindObjectOfType<CotcGameObject>();
        if (!cb) return;
        cb.GetCloud().Done(cloud => {
            
            Cloud = cloud;
        }, exception =>
        {
            Debug.Log(exception.Message);
        });
    }

    public void OnPressLogin()
    {
        Debug.Log("Email and password have a good forrmat, try to login");
        LoginNetwork(emailInputField.text, passwordInputField.text);
        return;
    }
    

    public void LoginNetwork(string email, string passworld)
    {
        var cotc = FindObjectOfType<CotcGameObject>();

        cotc.GetCloud().Done(cloud => {
            Cloud.Login(
                    network: "email",
                    networkId: email,
                    networkSecret: passworld)
                .Done(gamer => {
                    Debug.Log("Signed in succeeded (ID = " + gamer.GamerId + ")");
                    Debug.Log("Login data: " + gamer);
                    Debug.Log("Server time: " + gamer["servertime"]);
                    cantLogin.gameObject.SetActive(false);
                    canvasLogin.gameObject.SetActive(false);
                    canvasSuccedLogin.gameObject.SetActive(true);
                }, ex => {
                    // The exception should always be CotcException
                    CotcException error = (CotcException)ex;
                    Debug.LogError("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                    cantLogin.gameObject.SetActive(true);
                });
        });  
    }
}
