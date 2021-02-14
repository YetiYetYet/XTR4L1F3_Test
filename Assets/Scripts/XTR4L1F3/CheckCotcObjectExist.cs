using CotcSdk;
using UnityEngine;

public class CheckCotcObjectExist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cb = FindObjectOfType<CotcGameObject>();
        if (cb == null) {
            Debug.LogError("Please put a Clan of the Cloud prefab in your scene!");
        }
    }
}
