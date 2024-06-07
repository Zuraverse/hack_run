using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;
using TMPro;
using System;

public class LevelWiseScore : MonoBehaviour
{
    public TMP_Text Level1Text, Level2Text, Level3Text, Level4Text, Level5Text, Level6Text, Level7Text, Level8Text, Level9Text, Level10Text, Level11Text;
    public Slider lv1Slider, lv2Slider, lv3Slider, lv4Slider, lv5Slider, lv6Slider, lv7Slider, lv8Slider, lv9Slider, lv10Slider, lv11Slider;
    public Button claimButton1, claimButton2, claimButton3, claimButton4, claimButton5, claimButton6, claimButton7, claimButton8, claimButton9, claimButton10, claimButton11;
    public Button claimedButton1, claimedButton2, claimedButton3, claimedButton4, claimedButton5, claimedButton6, claimedButton7, claimedButton8, claimedButton9, claimedButton10, claimedButton11;
    string DROP_ERC20_CONTRACT = "0x67424D755a966A06501816B1f652e76f0965117F";
    public GameObject[] loadingbar;

    private void Start()
    {
        //ShowScoreOnTheLevelPanel();
    }
    //public void ShowScoreOnTheLevelPanel()
    //{
    //    string level = PlayerPrefs.GetString("PlayerLevel");
    //    string playerScore = PlayerPrefs.GetString("PlayerScore");
    //    int currentLevel = int.Parse(level) + 1;
    //    int score = int.Parse(playerScore);

    //    // Update the current level's text and slider
    //    UpdateLevelTextAndSlider(currentLevel, score);

    //    // Fill sliders for levels less than the current level
    //    for (int i = 1; i < currentLevel; i++)
    //    {
    //        FillSlider(i);
    //        CheckHasClaimedOrNot("hasClaimedLV" + i);
    //    }

    //    // Disable buttons for levels beyond the current level
    //    for (int i = currentLevel; i <= 11; i++)
    //    {
    //        DisableButtons(i);
    //    }
    //}

    public void ShowScoreOnTheLevelPanel()
    {
        // Validate the input strings before parsing
        string levelString = PlayerPrefs.GetString("PlayerLevel");
        string playerScoreString = PlayerPrefs.GetString("PlayerScore");

        if (string.IsNullOrEmpty(levelString) || string.IsNullOrEmpty(playerScoreString))
        {
            Debug.LogError("Player level or score string is null or empty");
            return;
        }

        int currentLevel;
        int score;

        if (!int.TryParse(levelString, out currentLevel))
        {
            Debug.LogError("Failed to parse PlayerLevel: " + levelString);
            return;
        }

        if (!int.TryParse(playerScoreString, out score))
        {
            Debug.LogError("Failed to parse PlayerScore: " + playerScoreString);
            return;
        }

        currentLevel += 1; // If you are incrementing to the next level

        // Update the current level's text and slider
        UpdateLevelTextAndSlider(currentLevel, score);

        // Fill sliders for levels less than the current level
        for (int i = 1; i < currentLevel; i++)
        {
            FillSlider(i);
            CheckHasClaimedOrNot("hasClaimedLV" + i);
        }

        // Disable buttons for levels beyond the current level
        for (int i = currentLevel; i <= 11; i++)
        {
            DisableButtons(i);
        }
    }



    private void DisableButtons(int level)
    {
        switch (level + 1)
        {
            case 1:
                claimButton1.gameObject.SetActive(false);
                claimedButton1.gameObject.SetActive(false);
                break;
            case 2:
                claimButton2.gameObject.SetActive(false);
                claimedButton2.gameObject.SetActive(false);
                break;
            case 3:
                claimButton3.gameObject.SetActive(false);
                claimedButton3.gameObject.SetActive(false);
                break;
            case 4:
                claimButton4.gameObject.SetActive(false);
                claimedButton4.gameObject.SetActive(false);
                break;
            case 5:
                claimButton5.gameObject.SetActive(false);
                claimedButton5.gameObject.SetActive(false);
                break;
            case 6:
                claimButton6.gameObject.SetActive(false);
                claimedButton6.gameObject.SetActive(false);
                break;
            case 7:
                claimButton7.gameObject.SetActive(false);
                claimedButton7.gameObject.SetActive(false);
                break;
            case 8:
                claimButton8.gameObject.SetActive(false);
                claimedButton8.gameObject.SetActive(false);
                break;
            case 9:
                claimButton9.gameObject.SetActive(false);
                claimedButton9.gameObject.SetActive(false);
                break;
            case 10:
                claimButton10.gameObject.SetActive(false);
                claimedButton10.gameObject.SetActive(false);
                break;
            case 11:
                claimButton11.gameObject.SetActive(false);
                claimedButton11.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }


    private void UpdateLevelTextAndSlider(int level, int score)
    {
        switch (level)
        {
            case 0:
                Level1Text.text = score + "/1,000";
                lv1Slider.value = score;

                if (lv1Slider.value >= lv1Slider.maxValue)
                {
                    claimButton1.gameObject.SetActive(false);
                    claimedButton1.gameObject.SetActive(false);
                    //CheckHasClaimedOrNot("hasClaimedLV1");
                }
                break;
            case 1:
                Level1Text.text = score + "/1,000";
                lv1Slider.value = score;

                if (lv1Slider.value >= lv1Slider.maxValue)
                {
                    claimButton1.gameObject.SetActive(false);
                    claimedButton1.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV1");
                }
                break;
            case 2:
                Level2Text.text = score + "/10,000";
                lv2Slider.value = score;

                if (lv2Slider.value >= lv2Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV2");
                }
                break;
            case 3:

                Level3Text.text = score + "/100,000";
                lv3Slider.value = score;

                if (lv3Slider.value >= lv3Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV3");
                }
                break;
            case 4:

                Level4Text.text = score + "/500,000";
                lv4Slider.value = score;

                if (lv4Slider.value >= lv4Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV4");
                }
                break;
            case 5:

                Level5Text.text = score + "/1,000,000";
                lv5Slider.value = score;

                if (lv5Slider.value >= lv5Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV5");
                }
                break;
            case 6:

                Level6Text.text = score + "/2,000,000";
                lv6Slider.value = score;

                if (lv6Slider.value >= lv6Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV6");
                }
                break;
            case 7:

                Level7Text.text = score + "/3,000,000";
                lv7Slider.value = score;

                if (lv7Slider.value >= lv7Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV7");
                }
                break;
            case 8:

                Level8Text.text = score + "/5,000,000";
                lv8Slider.value = score;

                if (lv8Slider.value >= lv8Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV8");
                }
                break;
            case 9:

                Level9Text.text = score + "/7,000,000";
                lv9Slider.value = score;

                if (lv9Slider.value >= lv9Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV9");
                }
                break;
            case 10:

                Level10Text.text = score + "/10,000,000";
                lv10Slider.value = score;

                if (lv10Slider.value >= lv10Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV10");
                }
                break;
            case 11:

                Level11Text.text = score + "/100,000,000";
                lv11Slider.value = score;

                if (lv11Slider.value >= lv11Slider.maxValue)
                {
                    claimButton2.gameObject.SetActive(false);
                    claimedButton2.gameObject.SetActive(false);
                    CheckHasClaimedOrNot("hasClaimedLV11");
                }
                break;
            default:
                break;
        }
    }

    private void FillSlider(int level)
    {
        switch (level)
        {
            case 1:
                lv1Slider.value = lv1Slider.maxValue;
                Level1Text.text = "1,000/1,000";
                // Disable claim and claimed buttons if slider value reaches max  
                break;
            case 2:
                lv2Slider.value = lv2Slider.maxValue;
                Level2Text.text = "10,000/10,000";
                break;
            case 3:
                lv3Slider.value = lv3Slider.maxValue;
                Level3Text.text = "100,000/100,000";
                
                break;
            case 4:
                lv4Slider.value = lv4Slider.maxValue;
                Level4Text.text = "500,000/500,000";

                break;
            case 5:
                lv5Slider.value = lv5Slider.maxValue;
                Level5Text.text = "1,000,000/1,000,000";

                break;
            case 6:
                lv6Slider.value = lv6Slider.maxValue;
                Level6Text.text = "2,000,000/2,000,000";

                break;
            case 7:
                lv7Slider.value = lv7Slider.maxValue;
                Level7Text.text = "3,000,000/3,000,000";

                break;
            case 8:
                lv8Slider.value = lv8Slider.maxValue;
                Level8Text.text = "5,000,000/5,000,000";

                break;
            case 9:
                lv9Slider.value = lv9Slider.maxValue;
                Level9Text.text = "7,000,000/7,000,000";

                break;
            case 10:
                lv10Slider.value = lv10Slider.maxValue;
                Level10Text.text = "10,000,000/10,000,000";

                break;
            case 11:
                lv11Slider.value = lv11Slider.maxValue;
                Level11Text.text = "100,000,000/100,000,000";

                break;
            default:
                break;
        }
    }


    public void CheckHasClaimedOrNot(string hasClaimed)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data.ContainsKey(hasClaimed) && result.Data[hasClaimed].Value == "true")
            {
                // If hasClaimed is true, enable the claimed button and disable the claim button
                EnableClaimedButton(hasClaimed);
            }
            else
            {
                // If hasClaimed is false or doesn't exist, enable the claim button and disable the claimed button
                EnableClaimButton(hasClaimed);
            }
        }, null);
    }

    private void EnableClaimButton(string hasClaimedKey)
    {
        switch (hasClaimedKey)
        {
            case "hasClaimedLV1":
                claimButton1.gameObject.SetActive(true);
                claimedButton1.gameObject.SetActive(false);
                //yaha se claim karwane ka function banana hai
                claimButton1.onClick.AddListener(() => CliamKarmaPoints(25, "hasClaimedLV1", 0, claimButton1, claimedButton1));
                break;
            case "hasClaimedLV2":
                claimButton2.gameObject.SetActive(true);
                claimedButton2.gameObject.SetActive(false);
                claimButton2.onClick.AddListener(() => CliamKarmaPoints(50, "hasClaimedLV2", 1, claimButton2, claimedButton2));
                break;
            case "hasClaimedLV3":
                claimButton3.gameObject.SetActive(true);
                claimedButton3.gameObject.SetActive(false);
                claimButton3.onClick.AddListener(() => CliamKarmaPoints(100, "hasClaimedLV3", 2, claimButton3, claimedButton3));
                break;
            case "hasClaimedLV4":
                claimButton4.gameObject.SetActive(true);
                claimedButton4.gameObject.SetActive(false);
                claimButton4.onClick.AddListener(() => CliamKarmaPoints(200, "hasClaimedLV4", 3, claimButton4, claimedButton4));
                break;
            case "hasClaimedLV5":
                claimButton5.gameObject.SetActive(true);
                claimedButton5.gameObject.SetActive(false);
                claimButton5.onClick.AddListener(() => CliamKarmaPoints(500, "hasClaimedLV5", 4, claimButton5, claimedButton5));
                break;
            case "hasClaimedLV6":
                claimButton6.gameObject.SetActive(true);
                claimedButton6.gameObject.SetActive(false);
                claimButton6.onClick.AddListener(() => CliamKarmaPoints(1200, "hasClaimedLV6", 5, claimButton6, claimedButton6));
                break;
            case "hasClaimedLV7":
                claimButton7.gameObject.SetActive(true);
                claimedButton7.gameObject.SetActive(false);
                claimButton7.onClick.AddListener(() => CliamKarmaPoints(3000, "hasClaimedLV7", 6, claimButton7, claimedButton7));
                break;
            case "hasClaimedLV8":
                claimButton8.gameObject.SetActive(true);
                claimedButton8.gameObject.SetActive(false);
                claimButton8.onClick.AddListener(() => CliamKarmaPoints(5000, "hasClaimedLV8", 7, claimButton8, claimedButton8));
                break;
            case "hasClaimedLV9":
                claimButton9.gameObject.SetActive(true);
                claimedButton9.gameObject.SetActive(false);
                claimButton9.onClick.AddListener(() => CliamKarmaPoints(7000, "hasClaimedLV9", 8, claimButton9, claimedButton9));
                break;
            case "hasClaimedLV10":
                claimButton10.gameObject.SetActive(true);
                claimedButton10.gameObject.SetActive(false);
                claimButton10.onClick.AddListener(() => CliamKarmaPoints(10000, "hasClaimedLV10", 9, claimButton10, claimedButton10));
                break;
            case "hasClaimedLV11":
                claimButton11.gameObject.SetActive(true);
                claimedButton11.gameObject.SetActive(false);
                claimButton11.onClick.AddListener(() => CliamKarmaPoints(20000, "hasClaimedLV11", 10, claimButton11, claimedButton11));
                break;
            default:
                break;
        }
    }


    private void EnableClaimedButton(string hasClaimedKey)
    {
        switch (hasClaimedKey)
        {
            case "hasClaimedLV1":
                claimButton1.gameObject.SetActive(false);
                claimedButton1.gameObject.SetActive(true);
                break;
            case "hasClaimedLV2":
                claimButton2.gameObject.SetActive(false);
                claimedButton2.gameObject.SetActive(true);
                break;
            case "hasClaimedLV3":
                claimButton3.gameObject.SetActive(false);
                claimedButton3.gameObject.SetActive(true);
                break;
            case "hasClaimedLV4":
                claimButton4.gameObject.SetActive(false);
                claimedButton4.gameObject.SetActive(true);
                break;
            case "hasClaimedLV5":
                claimButton5.gameObject.SetActive(false);
                claimedButton5.gameObject.SetActive(true);
                break;
            case "hasClaimedLV6":
                claimButton6.gameObject.SetActive(false);
                claimedButton6.gameObject.SetActive(true);
                break;
            case "hasClaimedLV7":
                claimButton7.gameObject.SetActive(false);
                claimedButton7.gameObject.SetActive(true);
                break;
            case "hasClaimedLV8":
                claimButton8.gameObject.SetActive(false);
                claimedButton8.gameObject.SetActive(true);
                break;
            case "hasClaimedLV9":
                claimButton9.gameObject.SetActive(false);
                claimedButton9.gameObject.SetActive(true);
                break;
            case "hasClaimedLV10":
                claimButton10.gameObject.SetActive(false);
                claimedButton10.gameObject.SetActive(true);
                break;
            case "hasClaimedLV11":
                claimButton11.gameObject.SetActive(false);
                claimedButton11.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public async void CliamKarmaPoints(int karmaRewardPoints, string key, int i, Button claimbutton, Button ClaimedButton)
    {
        try
        {
            var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            Debug.Log("Claiming..");
            claimbutton.gameObject.SetActive(false);
            loadingbar[i].SetActive(true);
            await contract.ERC20.ClaimTo(address, karmaRewardPoints.ToString());
            // Create the request
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { key, "true" } }
            };

            // Send the request to update the user data
            PlayFabClientAPI.UpdateUserData(request, result =>
            {
                Debug.Log("User data updated successfully");
            }, error =>
            {
                Debug.Log("Error updating user data: " + error.GenerateErrorReport());
            });
            loadingbar[i].SetActive(false);
            ClaimedButton.gameObject.SetActive(true);
            Debug.Log("claimed");
            KarmaClaim.Instance.GetKarmaPoints();

            //// Set the flag to prevent the pop-up from showing again
            //PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            //{
            //    Data = new Dictionary<string, string> { { HasShownPopUp, "true" } }
            //}, null, null);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            claimbutton.gameObject.SetActive(true);
            ClaimedButton.gameObject.SetActive(false);
            loadingbar[i].SetActive(false);
            // If there's an error, you might want to set the user data to "false"
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { key, "false" } }
            };

            // Send the request to update the user data
            PlayFabClientAPI.UpdateUserData(request, result =>
            {
                Debug.Log("User data updated successfully");
            }, error =>
            {
                Debug.Log("Error updating user data: " + error.GenerateErrorReport());
            });
        }

    }
}

