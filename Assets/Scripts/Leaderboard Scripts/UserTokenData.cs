using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using System;

public class UserTokenData : MonoBehaviour
{


    public TMPro.TextMeshProUGUI[] UserDataText;
    public Button ShowMintingPanelButton;

    public ERC20KarmaPoints erc20KarmaPoints;

    public int KarmaPointsPrice;
    //private int tokenValue = 20;

    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject claimText;

    private void Start()
    {
        ShowMintingPanelButton.enabled = false;
        claimButton.SetActive(false);
    }


    //public void GetUserTokens(){
    //    PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    //}
    //public void SendLocalTokens(int tokenValue)
    //{
    //    var request = new UpdateUserDataRequest {
    //   Data = new Dictionary<string, string> {
    //        {"Tokens", tokenValue.ToString()}
    //    }
    //    };
    //    PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    //}

    //void OnDataSend(UpdateUserDataResult result){
    //    Debug.Log("data sent...");
    //}

    //void OnError(PlayFabError error){
    //    Debug.Log("Data Not sent error");
    //}

    //void OnDataRecieved(GetUserDataResult result){
    //    Debug.Log("User Data Recieved");
    //    if(result.Data != null && result.Data.ContainsKey("Tokens")){
    //        UserDataText.text = "$KP :  " + "<color=#00FF00>" + result.Data["Tokens"].Value + "</color>";
    //    }
    //}


    public void GetPlayerKarmaPoints()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
        erc20KarmaPoints.GetTokenBalance();
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int token = result.VirtualCurrency["KP"];
        UserDataText[0].text = "$KP :  " + "<color=#00FF00>" + token.ToString() + "</color>";
        UserDataText[1].text = "$KP :  " + "<color=#00FF00>" + token.ToString() + "</color>";
        ShowMintingPanelButton.enabled = true;
        if (token >= 1)
        {
            claimButton.SetActive(true);
            claimText.SetActive(false);
            KarmaPointsPrice = token;
            erc20KarmaPoints.GetTokenData(token);
        }
        else
        {
            claimButton.SetActive(false);
            claimText.SetActive(true);
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Data Not sent error");
    }


}
