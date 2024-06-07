using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.UI;
using System;
public class WinnersPopup : MonoBehaviour
{
    private bool hasShownWinnerPopup = false;
    private int[] prizeTable = new int[] { 40, 20, 20 };
    [SerializeField] public GameObject PopUpPanel;
    [SerializeField] public Text PopUpText;

    public void CheckWinners()
    {
        hasShownWinnerPopup = PlayerPrefs.GetInt("HasShownWinnerPopup", 0) == 1;
        GetCurrentLeaderboardVersion(currentVersion =>
        {
            int previousVersion = currentVersion - 1;
            if (!hasShownWinnerPopup)
            {
                PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest
                {
                    StatisticName = "PlayerScore",
                    MaxResultsCount = 1,
                    Version = previousVersion
                }, result =>
                {
                    var playerPosition = result.Leaderboard[0].Position;
                    if (playerPosition < 3)
                    {
                        // Show popup to winner
                        PopUpPanel.SetActive(true);
                        PopUpText.text = "You STOOD " + (playerPosition + 1) +"st" + " on the weekly leaderboard, here is your reward " + prizeTable[playerPosition] + "KarmaPoint!";
                    }
                    hasShownWinnerPopup = true;
                    // Save the value of hasShownWinnerPopup to PlayerPrefs
                    PlayerPrefs.SetInt("HasShownWinnerPopup", 1);
                    PlayerPrefs.Save();
                }, error => Debug.LogError(error.GenerateErrorReport()));

            }
        });
    }

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
