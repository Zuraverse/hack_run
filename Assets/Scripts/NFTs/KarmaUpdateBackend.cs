using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserProfile;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class KarmaUpdateBackend : MonoBehaviour
{

    public string urlForKP = "https://zuramarket.xyz/api/karma/update";
    //this block of code Giving Error in webgl 
    public async Task SendUserData(int Value)
    {
        Debug.Log("Updating Data On Database with Key : " + Value);
        RequestObject requestObject = new RequestObject();
        requestObject.karmabalance = Value;

        string jsonBody = JsonUtility.ToJson(requestObject);

        UnityWebRequest www = UnityWebRequest.Put(urlForKP, jsonBody);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            Debug.Log(responseBody);
            Debug.Log("Retrieved Info");
        }
    }
}
