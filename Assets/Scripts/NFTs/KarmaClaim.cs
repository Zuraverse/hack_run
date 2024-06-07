using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using Thirdweb;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KarmaClaim : MonoBehaviour
{
    public static KarmaClaim Instance { get; private set; }

    public TMP_Text UserBalanceText;
    //string DROP_ERC20_CONTRACT = "0x2Da9f4137B6c8E8B6027c1156638E5bBcdD3dB44";
    string DROP_ERC20_CONTRACT = "0x67424D755a966A06501816B1f652e76f0965117F";

    public GameObject KarmaPointsRewardPanel;
    public TMP_Text KarmaPointsEarnedText;
    public TMP_Text MintingStatusText;

    public Button ClaimButton;

    public Button ClaimmButton;
    public Button ClaimedButton;

    public GameObject closeButton;

    [SerializeField]
    private LevelWiseScore levelWiseScore;

    [SerializeField]
    private ProfileInformation profileInformation;

    [SerializeField]
    private KarmaUpdateBackend karmaUpdateBackend;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        GetKarmaPoints();
    }

    public async void CliamKarmaPoints(string karmaReward, string HasShownPopUp)
    {

        string key = UpdateLevelTextAndSlider();

        string Levels = TakenReward();
        Debug.Log(key);
        try
        {
            var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            closeButton.SetActive(false);
            ClaimButton.interactable = false;
            MintingStatusText.text = "Claiming...";
            //await contract.ERC1155.MintTo(address, new NFTMetadataWithSupply()
            //{
            //    supply = 1,
            //    metadata = new NFTMetadata()
            //    {
            //        id = "1",
            //    }
            //});
            await contract.ERC20.Claim(karmaReward);

            // Create the request
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { Levels, "true" } }
            };

            // Send the request to update the user data
            PlayFabClientAPI.UpdateUserData(request, result =>
            {
                Debug.Log("User data updated successfully");
            }, error =>
            {
                Debug.Log("Error updating user data: " + error.GenerateErrorReport());
            });
            MintingStatusText.text = "Successfully Claimed.";
            LeaderboardHouses.Instance.GetLeaderboard();
            closeButton.SetActive(true);
            KarmaPointsRewardPanel.SetActive(false);
            levelWiseScore.CheckHasClaimedOrNot(Levels);
            GetKarmaPoints();
        }
        catch (System.Exception)
        {
            Debug.Log("Minting Error");
            MintingStatusText.text = "Minting Error";
            ClaimButton.interactable = true;
            closeButton.SetActive(true);
        }

    }

    public async void GetKarmaPoints()
    {
        var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        Debug.Log(address);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
        var data = await contract.ERC20.BalanceOf(address);
        Debug.Log(data);
        //PlayerPrefs.SetFloat("WalletBalance", + );
        float karmaPoints = 0;
        if (float.TryParse(data.displayValue, out karmaPoints))
        {
            UserBalanceText.text = "$KP : " + karmaPoints.ToString();
            PlayerPrefs.SetString("karmaPoints", "$KP " + karmaPoints.ToString());
            profileInformation.ShowKarmaPointsOnMainScene();
            await karmaUpdateBackend.SendUserData((int)karmaPoints);
        }
        else
        {
            Debug.LogError("Invalid karma points value: " + data.displayValue);
        }

    }



    public void ShowRewardPopup(string HasShownPopUp, int points)
    {
        Debug.Log("Points Earned : " + points);
        Debug.Log("Called ShowRewardsPopup");
        // Check if the pop-up has been shown before
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (!result.Data.ContainsKey(HasShownPopUp))
            {
                // Show the pop-up
                ShowPopUp(points, HasShownPopUp);
                // Set the flag to prevent the pop-up from showing again
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { HasShownPopUp, "true" } }
                }, null, null);
            }
        }, null);
    }

    private void ShowPopUp(int Points, string HasShownPopUp)
    {
        Debug.Log("Called ShowPopup Method");
        // Your code to show the pop-up goes here
        //CliamKarmaPoints(Points.ToString());
        KarmaPointsEarnedText.text = "Points : " + Points.ToString();
        KarmaPointsRewardPanel.SetActive(true);
        ClaimButton.onClick.AddListener(() => CliamKarmaPoints(Points.ToString(), HasShownPopUp));

        //CheckClaimStatus(HasShownPopUp);
    }


    private void UpdateBalance(string Balance)
    {
        UserBalanceText.text = Balance;
    }


    public void CheckClaimStatus(string HasShownPopUp)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data.ContainsKey(HasShownPopUp) && result.Data[HasShownPopUp].Value == "true")
            {
                // If HasShownPopUp is true, show the Claimed button and hide the Claim button
                ClaimmButton.gameObject.SetActive(false);
                ClaimedButton.gameObject.SetActive(true);
            }
            else
            {
                // If HasShownPopUp is false or doesn't exist, show the Claim button and hide the Claimed button
                ClaimmButton.gameObject.SetActive(true);
                ClaimedButton.gameObject.SetActive(false);
            }
        }, null);
    }


    public void closeRewardPanel()
    {
        string level = PlayerPrefs.GetString("PlayerLevel");
        Debug.Log("Player Levelsss : " + level);
        if (level.Equals("1"))
        {
            HasNotClaimedAndClosed("hasClaimedLV1");
        }
        if (level.Equals("2"))
        {
            HasNotClaimedAndClosed("hasClaimedLV2");
        }
        if (level.Equals("3"))
        {
            HasNotClaimedAndClosed("hasClaimedLV3");
        }
        if (level.Equals("4"))
        {
            HasNotClaimedAndClosed("hasClaimedLV4");
        }
        if (level.Equals("5"))
        {
            HasNotClaimedAndClosed("hasClaimedLV5");
        }
        if (level.Equals("6"))
        {
            HasNotClaimedAndClosed("hasClaimedLV6");
        }
        if (level.Equals("7"))
        {
            HasNotClaimedAndClosed("hasClaimedLV7");
        }
        if (level.Equals("8"))
        {
            HasNotClaimedAndClosed("hasClaimedLV8");
        }
        if (level.Equals("9"))
        {
            HasNotClaimedAndClosed("hasClaimedLV9");
        }
        if (level.Equals("10"))
        {
            HasNotClaimedAndClosed("hasClaimedLV10");
        }
        if (level.Equals("11"))
        {
            HasNotClaimedAndClosed("hasClaimedLV11");
        }

    }

    //if user closed the reward panel without claiming it
    public void HasNotClaimedAndClosed(string hasClaimed)
    {
        Debug.Log("function Called :" + hasClaimed);
        Debug.Log(hasClaimed);
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (!result.Data.ContainsKey(hasClaimed))
            {
                // Set the flag to prevent the  from showing again
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { hasClaimed, "false" } }
                }, null, null);
            }
            else
            {
                // Set the flag to prevent the  from showing again
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { hasClaimed, "false" } }
                }, null, null);
            }
        }, null);
    }

    public string TakenReward()
    {
        string level = PlayerPrefs.GetString("PlayerLevel");
        Debug.Log("Player Levelsss : " + level);
        if (level.Equals("1"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV1");

            return "hasClaimedLV1";
        }
        if (level.Equals("2"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV2");
            return "hasClaimedLV2";
        }
        if (level.Equals("3"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV3");
            return "hasClaimedLV3";
        }
        if (level.Equals("4"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV4");
            return "hasClaimedLV4";
        }
        if (level.Equals("5"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV5");
            return "hasClaimedLV5";
        }
        if (level.Equals("6"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV6");
            return "hasClaimedLV6";
        }
        if (level.Equals("7"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV7");
            return "hasClaimedLV7";
        }
        if (level.Equals("8"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV8");
            return "hasClaimedLV8";
        }
        if (level.Equals("9"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV9");
            return "hasClaimedLV9";
        }
        if (level.Equals("10"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV10");
            return "hasClaimedLV10";
        }
        if (level.Equals("11"))
        {
            //HasNotClaimedAndClosed("hasClaimedLV11");
            return "hasClaimedLV11";
        }

        return null;

    }

    private string UpdateLevelTextAndSlider()
    {
        string Level = PlayerPrefs.GetString("PlayerLevel");
        int currentLevel = int.Parse(Level);

        if (currentLevel == 1)
        {
            return "hasClaimedLV1";
        }
        else if (currentLevel == 2)
        {
            return "hasClaimedLV2";
        }
        else if (currentLevel == 3)
        {
            return "hasClaimedLV3";
        }
        else if (currentLevel == 4)
        {
            return "hasClaimedLV4";
        }
        else if (currentLevel == 5)
        {
            return "hasClaimedLV5";
        }
        else if (currentLevel == 6)
        {
            return "hasClaimedLV6";
        }
        else if (currentLevel == 7)
        {
            return "hasClaimedLV7";
        }
        else if (currentLevel == 8)
        {
            return "hasClaimedLV8";
        }
        else if (currentLevel == 9)
        {
            return "hasClaimedLV9";
        }
        else if (currentLevel == 10)
        {
            return "hasClaimedLV10";
        }
        else if (currentLevel == 11)
        {
            return "hasClaimedLV11"; // Assuming there's a level 12
        }
        else
        {
            return null; // Or any default value
        }
    }
}
