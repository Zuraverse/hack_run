using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using Thirdweb;

public class LeaderboardLogin : MonoBehaviour
{
    // Start is called before the first frame update
    //public UserTokenData userTokenData;
    //public WinnersPopup winnersPopup;

    public async void Loginn()
    {
        //WalletAddress
        //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2fe");
        //string walletAdress = PlayerPrefs.GetString("WalletAddress");
        var results = await ThirdwebManager.Instance.SDK.wallet.GetAddress();

        Debug.Log(results);
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "A9293";
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = results,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);

    }

    void OnSucces(LoginResult result)
    {
        //userTokenData.SendLocalTokens();
        UpdateDisplayName();
        //userTokenData.GetPlayerKarmaPoints();
        //winnersPopup.CheckWinners();
        Debug.Log("Congrats Your Account is created");
    }


    void OnError(PlayFabError error)
    {
        Debug.Log("Failure");
        Debug.LogError(error.GenerateErrorReport());
    }


    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");

        //for (int i = 0; i < 4; i++)
        //{
        //    UpdateDisplayName();
        //    GetLeaderboard();
        //}
        UpdateDisplayName();
    }


    public async void UpdateDisplayName()
    {
        //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f");
        var results = await ThirdwebManager.Instance.SDK.wallet.GetAddress();

        //if (!PlayerPrefs.HasKey("WalletAddress"))
        //{
            //PlayerPrefs.SetString("WalletAddress", walletAdres);
        //}
        string name = PlayerPrefs.GetString("UserName  ") + results.Substring(0, 3) + ".." + results.Substring(results.Length - 3);
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnError);
    }


    void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name updated successfully");
    }
}
