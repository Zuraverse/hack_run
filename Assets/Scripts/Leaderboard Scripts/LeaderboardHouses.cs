using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using TMPro;
using Thirdweb;

public class LeaderboardHouses : MonoBehaviour
{
    //public Text positionText;
    //public Text displayNameText;
    //public Text statValueText;
    //public Text leaderboardText;
    //public Text CurrentPlayerpositionText;
    //public Text CurrentPlayerdisplayNameText;
    //public Text CurrentPlayerstatValueText;
    public static LeaderboardHouses Instance { get; private set; }

    public Text CurrentPlayerPostion;
    public Text CurrentPlayerScore;
    public Text CurrentPlayerName;

    public Text CurrentPlayerMainMenuScore;
    [SerializeField] private HouseRankLeaderboard houseRankLeaderboard;
    /// <summary>
    /// Houses Text and Refrences
    /// </summary>
    public Text CurrentPlayerPostionDatura;
    public Text CurrentPlayerScoreDatura;
    public Text CurrentPlayerNameDatura;

    public Text CurrentPlayerPostionIboga;
    public Text CurrentPlayerScoreIboga;
    public Text CurrentPlayerNameIboga;

    public Text CurrentPlayerPostionAyahuasca;
    public Text CurrentPlayerScoreAyahuasca;
    public Text CurrentPlayerNameAyahuasca;

    public Text CurrentPlayerPostionKava;
    public Text CurrentPlayerScoreKava;
    public Text CurrentPlayerNameKava;

    public Text CurrentPlayerPostionPeyote;
    public Text CurrentPlayerScorePeyote;
    public Text CurrentPlayerNamePeyote;
    /// <summary>
    /// Ends
    /// </summary>


    private int PlayerTotalScore;
    [SerializeField] private Text DaturaTotalScoreText;
    [SerializeField] private Text IbogaTotalScoreText;
    [SerializeField] private Text AyahuascaTotalScoreText;
    [SerializeField] private Text KavaTotalScoreText;
    [SerializeField] private Text PeyoteTotalScoreText;
    [SerializeField] private Text CaptainDaturaTotalScoreText;
    [SerializeField] private Text CaptainIbogaTotalScoreText;
    [SerializeField] private Text CaptainAyahuascaTotalScoreText;
    [SerializeField] private Text CaptainKavaTotalScoreText;
    [SerializeField] private Text CaptainPeyoteTotalScoreText;
    [SerializeField] private Text CaptainDaturaNameText;
    [SerializeField] private Text CaptainIbogaNameText;
    [SerializeField] private Text CaptainAyahuascaNameText;
    [SerializeField] private Text CaptainKavaNameText;
    [SerializeField] private Text CaptainPeyoteNameText;

    //public Text PlayerLevel;

    public GameObject playerPrefab;
    public GameObject CurrentplayerPrefab;
    //public GameObject WeeklyRankerPrefab;
    public Transform scrollViewContent;
    public Transform scrollViewContentDatura;
    public Transform scrollViewContentIboga;
    public Transform scrollViewContentAyahuasca;
    public Transform scrollViewContentKava;
    public Transform scrollViewContentPeyote;

    [SerializeField]private LevelWiseScore levelWiseScore;
    [SerializeField] private CheckLevels checkLevels;
    [SerializeField] private TMP_Text MainMenuScoreText;
    //public Transform CurrentscrollViewContent;
    //public Transform WeeklyRankerscrollViewContent;


    ////WeekyLeaderboard tex
    //public Text rankText;
    //public Text displayNameText1;
    //public Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GetLeaderboard();
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

    //public void Loginn()
    //{
    //    //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2fe");
    //    string walletAdress = PlayerPrefs.GetString("WalletAddress");
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


    /// <summary>
    /// Leaderboard Of House members
    /// </summary>
    /// 

    public void SendDaturaLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "DaturaHouse", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    public void SendIbogaLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "IbogaHouse", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    public void SendAyahuascaLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "AyahuascaHouse", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    public void SendKavaLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "KavaHouse", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    public void SendPeyoteLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "PeyoteHouse", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }


    public void GetDaturaLeaderboard()
    {
        Debug.Log("Datura LeaderboardCalled");
        var request = new GetLeaderboardRequest
        {
            StatisticName = "DaturaHouse",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccessDatura, OnError);
    }

    public void GetIbogaLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "IbogaHouse",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccessIboga, OnError);
    }

    public void GetAyahuascaLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "AyahuascaHouse",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccessAyahuasca, OnError);
    }

    public void GetKavaLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "KavaHouse",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccessKava, OnError);
    }

    public void GetPeyoteLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PeyoteHouse",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccessPeyote, OnError);
    }



    void OnGetLeaderboardSuccessDatura(GetLeaderboardResult result)
    {
        // Initialize total score variable
        int totalScore = 0;

        if (PlayerPrefs.GetInt("NFT_ID").Equals(0))
        {
            GetLeaderboardAroundPlayerDatura();
        }

        foreach (Transform child in scrollViewContentDatura)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScoreDatura.text = "";
            CurrentPlayerPostionDatura.text = "";
            CurrentPlayerNameDatura.text = "";
        }

        Debug.Log("DaturaHouse Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContentDatura);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

            totalScore += item.StatValue;
        }

        // Print the total score in the console
        Debug.Log("Total Score Of Datura House : " + totalScore);
        DaturaTotalScoreText.text = totalScore.ToString();
        CaptainDaturaTotalScoreText.text = result.Leaderboard[0].StatValue.ToString();
        CaptainDaturaNameText.text = result.Leaderboard[0].DisplayName;
        GetIbogaLeaderboard();
    }



    void OnGetLeaderboardSuccessIboga(GetLeaderboardResult result)
    {
        // Initialize total score variable
        int totalScore = 0;

        if (PlayerPrefs.GetInt("NFT_ID").Equals(1))
        {
            GetLeaderboardAroundPlayerIboga();
        }

        foreach (Transform child in scrollViewContentIboga)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScoreIboga.text = "";
            CurrentPlayerPostionIboga.text = "";
            CurrentPlayerNameIboga.text = "";
        }

        Debug.Log("IbogaHouse Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContentIboga);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

            totalScore += item.StatValue;
        }

        Debug.Log("Total Score Of Iboga House : " + totalScore);
        IbogaTotalScoreText.text = totalScore.ToString();
        CaptainIbogaTotalScoreText.text = result.Leaderboard[0].StatValue.ToString();
        CaptainIbogaNameText.text = result.Leaderboard[0].DisplayName;
        GetAyahuascaLeaderboard();
    }


    void OnGetLeaderboardSuccessAyahuasca(GetLeaderboardResult result)
    {
        // Initialize total score variable
        int totalScore = 0;

        if (PlayerPrefs.GetInt("NFT_ID").Equals(3))
        {
            GetLeaderboardAroundPlayerAyahuasca();
        }

        foreach (Transform child in scrollViewContentAyahuasca)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScoreAyahuasca.text = "";
            CurrentPlayerPostionAyahuasca.text = "";
            CurrentPlayerNameAyahuasca.text = "";
        }

        Debug.Log("AyahuascaHouse Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContentAyahuasca);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

            totalScore += item.StatValue;
        }

        Debug.Log("Total Score Of Ayahuasca House : " + totalScore);
        AyahuascaTotalScoreText.text = totalScore.ToString();
        CaptainAyahuascaTotalScoreText.text = result.Leaderboard[0].StatValue.ToString();
        CaptainAyahuascaNameText.text = result.Leaderboard[0].DisplayName;
        GetKavaLeaderboard();
    }


    void OnGetLeaderboardSuccessKava(GetLeaderboardResult result)
    {
        // Initialize total score variable
        int totalScore = 0;

        if (PlayerPrefs.GetInt("NFT_ID").Equals(4))
        {
            GetLeaderboardAroundPlayerKava();
        }

        foreach (Transform child in scrollViewContentKava)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScoreKava.text = "";
            CurrentPlayerPostionKava.text = "";
            CurrentPlayerNameKava.text = "";
        }

        Debug.Log("PeyoteHouse Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContentKava);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

            totalScore += item.StatValue;
        }

        Debug.Log("Total Score Of kava House : " + totalScore);
        KavaTotalScoreText.text = totalScore.ToString();
        CaptainKavaTotalScoreText.text = result.Leaderboard[0].StatValue.ToString();
        CaptainKavaNameText.text = result.Leaderboard[0].DisplayName;
        GetPeyoteLeaderboard();
    }


    void OnGetLeaderboardSuccessPeyote(GetLeaderboardResult result)
    {
        // Initialize total score variable
        int totalScore = 0;

        if (PlayerPrefs.GetInt("NFT_ID").Equals(2))
        {
            GetLeaderboardAroundPlayerPeyote();
        }
        // Iterate through the child objects of the scroll view's content
        foreach (Transform child in scrollViewContentPeyote)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScorePeyote.text = "";
            CurrentPlayerPostionPeyote.text = "";
            CurrentPlayerNamePeyote.text = "";
        }

        Debug.Log("Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContentPeyote);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

            totalScore += item.StatValue;
        }

        Debug.Log("Total Score Of Peyote House : " + totalScore);
        PeyoteTotalScoreText.text = totalScore.ToString();
        CaptainPeyoteTotalScoreText.text = result.Leaderboard[0].StatValue.ToString();
        CaptainPeyoteNameText.text = result.Leaderboard[0].DisplayName;
    }



    public void GetLeaderboardAroundPlayerDatura()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "DaturaHouse", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccessDatura, OnGetLeaderboardAroundPlayerFailure);
    }

    public void GetLeaderboardAroundPlayerIboga()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "IbogaHouse", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccessIboga, OnGetLeaderboardAroundPlayerFailure);
    }

    public void GetLeaderboardAroundPlayerAyahuasca()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "AyahuascaHouse", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccessAyahuasca, OnGetLeaderboardAroundPlayerFailure);
    }

    public void GetLeaderboardAroundPlayerKava()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "KavaHouse", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccessKava, OnGetLeaderboardAroundPlayerFailure);
    }

    public void GetLeaderboardAroundPlayerPeyote()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "PeyoteHouse", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccessPeyote, OnGetLeaderboardAroundPlayerFailure);
    }


    void OnGetLeaderboardAroundPlayerSuccessDatura(GetLeaderboardAroundPlayerResult result)
    {
        //if (PlayerPrefs.GetInt("NFT_ID").Equals(0))
        //{
        int totalScore = 0;
        Debug.Log("CurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            CurrentPlayerPostionDatura.text = rank.ToString();
            CurrentPlayerNameDatura.text = item.DisplayName;
            CurrentPlayerScoreDatura.text = item.StatValue.ToString();

            totalScore += item.StatValue;
            //CaptainDaturaNameText.text = item.DisplayName;
        }

        //CaptainDaturaTotalScoreText.text = totalScore.ToString();
        //}
    }

    void OnGetLeaderboardAroundPlayerSuccessIboga(GetLeaderboardAroundPlayerResult result)
    {
        int totalScore = 0;
        Debug.Log("CurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("Inside foreach loop");
            int rank = item.Position + 1;
            Debug.Log("Rank calculated: " + rank);
            CurrentPlayerPostionIboga.text = rank.ToString();
            Debug.Log("Position text set: " + rank.ToString());
            CurrentPlayerNameIboga.text = item.DisplayName;
            Debug.Log("Name text set: " + item.DisplayName);
            CurrentPlayerScoreIboga.text = item.StatValue.ToString();
            Debug.Log("Score text set: " + item.StatValue.ToString());
            totalScore += item.StatValue;
            Debug.Log("Total score updated: " + totalScore.ToString());
            CaptainIbogaNameText.text = item.DisplayName;
            Debug.Log("Captain name text set: " + item.DisplayName);
        }
        //CaptainIbogaTotalScoreText.text = totalScore.ToString();
        // Debug.Log("Captain total score text set: " + totalScore.ToString());
    }

    void OnGetLeaderboardAroundPlayerSuccessAyahuasca(GetLeaderboardAroundPlayerResult result)
    {
        int totalScore = 0;
        Debug.Log("CuurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            CurrentPlayerPostionAyahuasca.text = rank.ToString();
            CurrentPlayerNameAyahuasca.text = item.DisplayName;
            CurrentPlayerScoreAyahuasca.text = item.StatValue.ToString();
            totalScore += item.StatValue;
            //CaptainAyahuascaNameText.text = item.DisplayName;
        }
        //CaptainAyahuascaTotalScoreText.text = totalScore.ToString();
    }

    void OnGetLeaderboardAroundPlayerSuccessKava(GetLeaderboardAroundPlayerResult result)
    {
        int totalScore = 0;
        Debug.Log("CuurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            CurrentPlayerPostionKava.text = rank.ToString();
            CurrentPlayerNameKava.text = item.DisplayName;
            CurrentPlayerScoreKava.text = item.StatValue.ToString();
            totalScore += item.StatValue;
            //CaptainKavaNameText.text = item.DisplayName;
        }
        //CaptainKavaTotalScoreText.text = totalScore.ToString();
    }

    void OnGetLeaderboardAroundPlayerSuccessPeyote(GetLeaderboardAroundPlayerResult result)
    {
        int totalScore = 0;
        Debug.Log("CurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            CurrentPlayerPostionPeyote.text = rank.ToString();
            CurrentPlayerNamePeyote.text = item.DisplayName;
            CurrentPlayerScorePeyote.text = item.StatValue.ToString();
            totalScore += item.StatValue;
            CaptainPeyoteNameText.text = item.DisplayName;
        }
        //CaptainPeyoteTotalScoreText.text = totalScore.ToString();

        houseRankLeaderboard.startSorting();
    }
    /// <summary>
    /// End
    /// </summary>
    /// 

    public void InvokwLeaderboard()
    {
        Invoke("GetLeaderboard", 4);
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "CosmicSerfer",
            StartPosition = 0,
            MaxResultsCount = 40
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnError);
    }

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");

        //UpdateDisplayName();
    }



    public void UpdateDisplayName()
    {
        //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f");
        string walletAdres = PlayerPrefs.GetString("WalletAddress");
        if (!PlayerPrefs.HasKey("WalletAddress"))
        {
            PlayerPrefs.SetString("WalletAddress", walletAdres);
        }
        string name = walletAdres.Substring(0, 4) + "..." + walletAdres.Substring(walletAdres.Length - 4);
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




    void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        GetLeaderboardAroundPlayer();
        //ClaimKarma.Instance.GetKarmaPoints();

        foreach (Transform child in scrollViewContent)
        {
            // Destroy each child object
            Destroy(child.gameObject);
            CurrentPlayerScore.text = "";
            CurrentPlayerPostion.text = "";
            CurrentPlayerName.text = "";
        }
        Debug.Log("Leaderboard fetched completed");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            GameObject playerInstance = Instantiate(playerPrefab, scrollViewContent);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            //playerInstance.transform.Find("PositionText").GetComponent<Text>().text = rank.ToString();
            //playerInstance.transform.Find("DisplayNameText").GetComponent<Text>().text = item.DisplayName;
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
            CurrentPlayerScore.text = "";
            CurrentPlayerPostion.text = "";
            CurrentPlayerName.text = "";
        }

    }


    public void GetLeaderboardIterate()
    {
        for (int i = 0; i < 4; i++)
        {
            GetLeaderboard();
        }
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "CosmicSerfer", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log("CuurentPlayer rank retreived");
        foreach (var item in result.Leaderboard)
        {
            // Instantiate a new instance of the player prefab
            //GameObject playerInstance = Instantiate(playerPrefab, scrollViewContent);

            // Set the text properties of the prefab's child text objects
            int rank = item.Position + 1;
            CurrentPlayerPostion.text = rank.ToString();
            CurrentPlayerName.text = item.DisplayName;
            CurrentPlayerScore.text = item.StatValue.ToString();
            MainMenuScoreText.text = item.StatValue.ToString();
            //PlayerPrefs.SetFloat("PlayerHighScore", item.StatValue);
            CurrentPlayerMainMenuScore.text = item.StatValue.ToString();
            PlayerPrefs.SetString("PlayerName", item.DisplayName);
            PlayerPrefs.SetString("PlayerScore", item.StatValue.ToString());
            CalculateLevel(item.StatValue);

            SetUserHouseId("HouseID", PlayerPrefs.GetString("HouseId"));
        }
        GetDaturaLeaderboard();
    }


    void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetWeeklyLeaderboard()
    {
        GetCurrentLeaderboardVersion(currentVersion =>
        {
            int previousVersion = currentVersion - 1;
            var request = new GetLeaderboardRequest
            {
                StatisticName = "CosmicSerfer",
                StartPosition = 0,
                MaxResultsCount = 3,
                Version = previousVersion
            };

            PlayFabClientAPI.GetLeaderboard(request, result =>
            {
                Debug.Log("Leaderboard retrieved successfully");
                foreach (var item in result.Leaderboard)
                {
                    // Instantiate a new instance of the player prefab
                    //GameObject playerInstance = Instantiate(WeeklyRankerPrefab, WeeklyRankerscrollViewContent);

                    //// Set the text properties of the prefab's child text objects
                    //int rank = item.Position + 1;
                    //playerInstance.transform.Find("Panel/PositionText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
                    //playerInstance.transform.Find("Panel/DisplayNameText").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
                    //playerInstance.transform.Find("Panel/StatValueText").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();

                }
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
        });
    }

    public void GetCurrentLeaderboardVersion(Action<int> callback)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "CosmicSerfer",
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
    public void CalculateLevel(int PlayerTotalScore)
    {
        if (PlayerTotalScore >= 0 && PlayerTotalScore < 1000)
        {
            int level = 0;
            SetLevel(level.ToString());
            //KarmaClaim.Instance.ShowRewardPopup("HasShownLevel1PopUp", 25);
        }
        else if (PlayerTotalScore >= 1000 && PlayerTotalScore < 10000)
        {
            int level = 1;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel1PopUp", 25);
        }
        else if (PlayerTotalScore >= 10000 && PlayerTotalScore < 100000)
        {
            int level = 2;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel2PopUp", 50);
        }
        else if (PlayerTotalScore >= 100000 && PlayerTotalScore < 500000)
        {
            int level = 3;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel3PopUp", 100);
        }
        else if (PlayerTotalScore >= 500000 && PlayerTotalScore < 1000000)
        {
            int level = 4;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel4PopUp", 200);
        }
        else if (PlayerTotalScore >= 1000000 && PlayerTotalScore < 2000000)
        {
            int level = 5;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel5PopUp", 500);
        }
        else if (PlayerTotalScore >= 2000000 && PlayerTotalScore < 3000000)
        {
            int level = 6;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel6PopUp", 1200);
        }
        else if (PlayerTotalScore >= 3000000 && PlayerTotalScore < 5000000)
        {
            int level = 7;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel7PopUp", 3000);
        }
        else if (PlayerTotalScore >= 5000000 && PlayerTotalScore < 7000000)
        {
            int level = 8;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel8PopUp", 5000);
        }
        else if (PlayerTotalScore >= 7000000 && PlayerTotalScore < 10000000)
        {
            int level = 9;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel9PopUp", 7000);
        }
        else if (PlayerTotalScore >= 10000000 && PlayerTotalScore < 20000000)
        {
            int level = 10;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel10PopUp", 10000);
        }
        else if(PlayerTotalScore >= 20000000)
        {
            int level = 11;
            SetLevel(level.ToString());
            KarmaClaim.Instance.ShowRewardPopup("HasShownLevel11PopUp", 20000);
        }
    }


    public void SetLevel(string score)
    {
        string level = score;
        PlayerPrefs.SetString("PlayerLevel", level);
        PlayerPrefs.Save();
        levelWiseScore.ShowScoreOnTheLevelPanel();
        checkLevels.StartGlowStart();
    }


    private void SetUserHouseId(string key, string value)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { key, value } }
        };

        PlayFabClientAPI.UpdateUserData(request, OnIdDataUpdated, OnErrorIdUpload);
    }


    void OnIdDataUpdated(UpdateUserDataResult result)
    {
        Debug.Log("IdUploaded");
    }

    void OnErrorIdUpload(PlayFabError error)
    {
        Debug.Log("Id Not uploaded Error");
    }

}


