using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Xml;

public class LeaderboardDataExport : MonoBehaviour
{
    public string leaderboardName = "Score";
    public int maxResultsCount = 50;

    public void ExportPlayerData()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            MaxResultsCount = maxResultsCount
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardError);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Players");
        xmlDoc.AppendChild(rootNode);

        foreach (var item in result.Leaderboard)
        {
            XmlNode playerNode = xmlDoc.CreateElement("Player");
            rootNode.AppendChild(playerNode);

            XmlNode customIdNode = xmlDoc.CreateElement("CustomId");
            customIdNode.InnerText = item.PlayFabId;
            playerNode.AppendChild(customIdNode);

            XmlNode displayNameNode = xmlDoc.CreateElement("DisplayName");
            displayNameNode.InnerText = item.DisplayName;
            playerNode.AppendChild(displayNameNode);

            //XmlNode titlePlayerAccountIdNode = xmlDoc.CreateElement("TitlePlayerAccountId");
           // titlePlayerAccountIdNode.InnerText = item.TitlePlayerAccountId;
            //playerNode.AppendChild(titlePlayerAccountIdNode);
        }

        xmlDoc.Save(Application.dataPath + "/Players.xml");
    }

    private void OnGetLeaderboardError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
