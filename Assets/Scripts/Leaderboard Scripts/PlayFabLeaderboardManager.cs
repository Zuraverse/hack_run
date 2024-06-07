using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using Thirdweb;
using TMPro;

public class PlayFabLeaderboardManager : MonoBehaviour
{
    public Text positionText;
    public Text displayNameText;
    public Text statValueText;
    public Text leaderboardText;
    public Text CurrentPlayerpositionText;
    public Text CurrentPlayerdisplayNameText;
    public Text CurrentPlayerstatValueText;


    public GameObject playerPrefab;
    public GameObject CurrentplayerPrefab;
    public GameObject WeeklyRankerPrefab;
    public Transform scrollViewContent;
    public Transform CurrentscrollViewContent;
    public Transform WeeklyRankerscrollViewContent;


    //WeekyLeaderboard tex
    public Text rankText;
    public Text displayNameText1;
    public Text scoreText;
    private bool hasShownWinnerPopup = false;
    private int[] prizeTable = new int[] { 50, 20, 20 };
    [SerializeField] public GameObject PopUpPanel;
    [SerializeField] public Text PopUpText;

    public TMPro.TextMeshProUGUI highScore;


    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Loginn", 2);
    }


    //void OnSucces(LoginResult result)
    //{
    //    Debug.Log("Congrats Your Account is created");
    //}


    void OnError(PlayFabError error)
    {
        Debug.Log("Failure");
        Debug.LogError(error.GenerateErrorReport());
    }

    void Update()
    {

    }

    //public async void Loginn()
    //{
    //    //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2fe");
    //    //string walletAdress = PlayerPrefs.GetString("WalletAddress");
    //    var walletAdress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
    //    Debug.Log(walletAdress);
    //    if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
    //    {
    //        PlayFabSettings.TitleId = "A9293";
    //    }

    //    var request = new LoginWithCustomIDRequest
    //    {
    //        CustomId = walletAdress,
    //        CreateAccount = true
    //    };
    //    PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    //}

    public void SendLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "CosmicSerfer", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }


    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "CosmicSerfer",
            StartPosition = 0,
            MaxResultsCount = 20
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnError);
    }

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");

        //for (int i = 0; i < 4; i++)
        //{
        //    UpdateDisplayName();
        //    GetLeaderboard();
        //}
        //UpdateDisplayName();
    }


    //public async void UpdateDisplayName()
    //{
    //    //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f");
    //    //string walletAdres = PlayerPrefs.GetString("WalletAddress");
    //    var walletAdress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
    //    //if (!PlayerPrefs.HasKey("WalletAddress"))
    //    //{
    //    //    PlayerPrefs.SetString("WalletAddress", walletAdres);
    //    //}
    //    string name = walletAdress.Substring(0, 4) + "..." + walletAdress.Substring(walletAdress.Length - 4);
    //    var request = new UpdateUserTitleDisplayNameRequest
    //    {
    //        DisplayName = name
    //    };
    //    PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnError);
    //}


    //void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    //{
    //    Debug.Log("Display name updated successfully");
    //}


    //void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    //{
    //    Debug.Log("Leaderboard fetched completed");
    //    string positionString = "";
    //    string DisplayNameString = "";
    //    string statValueString = "";
    //    foreach (var item in result.Leaderboard)
    //    {
    //        int rank = item.Position + 1;
    //        positionString += "<color=#FF0000>" + rank + "</color>\n\n\n";
    //        DisplayNameString += item.DisplayName + "\n\n\n";
    //        statValueString += "<color=#0000FF>" + item.StatValue.ToString() + "</color>\n\n\n";
    //    }
    //    positionText.text = positionString;
    //    displayNameText.text = DisplayNameString;
    //    statValueText.text = statValueString;
    //}


    void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContent);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();
        }
    }

    public void OnHomeButtonClick()
    {
        // Iterate through the child objects of the scroll view's content
        foreach (Transform child in scrollViewContent)
        {
            // Destroy each child object
            Destroy(child.gameObject);
        }
        foreach (Transform child in CurrentscrollViewContent)
        {
            // Destroy each child object
            Destroy(child.gameObject);
        }

        foreach (Transform child in WeeklyRankerscrollViewContent)
        {
            // Destroy each child object
            Destroy(child.gameObject);
        }
    }


    //public void GetLeaderboardIterate()
    //{
    //    for(int i = 0; i < 4; i++)
    //    {
    //        GetLeaderboard();
    //    }
    //}

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "CosmicSerfer", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    //{
    //    string CurrentPlayerpositionString = "";
    //    string CurrentPlayerDisplayNameString = "";
    //    string CurrentPlayerstatValueString = "";
    //    foreach (var item in result.Leaderboard)
    //    {
    //        int rank = item.Position + 1;
    //        CurrentPlayerpositionString += "<color=#FF0000>" + rank + "</color>\n\n\n";
    //        CurrentPlayerDisplayNameString += item.DisplayName + "\n\n\n";
    //        CurrentPlayerstatValueString += "<color=#0000FF>" + item.StatValue.ToString() + "</color>\n\n\n";
    //    }
    //    CurrentPlayerpositionText.text = CurrentPlayerpositionString;
    //    CurrentPlayerdisplayNameText.text = CurrentPlayerDisplayNameString;
    //    CurrentPlayerstatValueText.text = CurrentPlayerstatValueString;
    //}
    void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log("CurrentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(CurrentplayerPrefab, CurrentscrollViewContent);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();
        }
    }


    void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }


//public void GetWeeklyLeaderboard()
//{
//    GetCurrentLeaderboardVersion(currentVersion =>
//    {
//        int previousVersion = currentVersion - 1;
//        var request = new GetLeaderboardRequest
//        {
//            StatisticName = "PlayerScore",
//            StartPosition = 0,
//            MaxResultsCount = 3,
//            Version = previousVersion
//        };

//        PlayFabClientAPI.GetLeaderboard(request, result =>
//        {
//            Debug.Log("Leaderboard retrieved successfully");
//            foreach (var item in result.Leaderboard)
//            {
//                // Instantiate a new instance of the player prefab
//                GameObject playerInstance = Instantiate(WeeklyRankerPrefab, WeeklyRankerscrollViewContent);

//                // Set the text properties of the prefab's child text objects
//                int rank = item.Position + 1;
//                playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
//                playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
//                playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();
//            }

//            if (!hasShownWinnerPopup)
//            {
//                PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest
//                {
//                    StatisticName = "PlayerScore",
//                    MaxResultsCount = 1,
//                    Version = previousVersion
//                }, result2 =>
//                {
//                    var playerPosition = result2.Leaderboard[0].Position;
//                    if (playerPosition < 3)
//                    {
//                        // Show popup to winner
//                        PopUpPanel.SetActive(true);
//                        if(PopUpText != null)
//                        {
//                            PopUpText.text = "Congratulations! You are in position " + (playerPosition + 1) + " on the weekly leaderboard and have won " + prizeTable[playerPosition] + "KarmaPoint!";
//                        }
//                        hasShownWinnerPopup = true;
//                    }
//                }, error => Debug.LogError(error.GenerateErrorReport()));
//            }
//        }, error =>
//        {
//            Debug.LogError(error.GenerateErrorReport());
//        });
//    });
//}

    public void GetCurrentLeaderboardVersion(Action<int> callback)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerScore",
            StartPosition = 0,
            MaxResultsCount = 1,
            Version = null
        };

        PlayFabClientAPI.GetLeaderboard(request, result =>
        {
            int currentVersion = result.Version;
            callback(currentVersion);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}


