using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;

public class NFTIdChecker : MonoBehaviour
{

    [SerializeField] private string contractAddress;

    public GameObject loadingPanel;

    List<int> ids = new List<int>();

    public GameObject HouseCorousalMint;
    public GameObject HouseCorousal;
    public GameObject NavigateHouseButtons;
    public TMP_Text walletConnectStatusText;
    public IfHouse ifHouse;
    public UserProfile userProfile;
    public GameObject[] MintButtons;
    public GameObject LoadingPanel;
    public GameObject CloseButton;

    public string HouseContract;

    private void Start()
    {
        walletConnectStatusText.text = "Connect Wallet and Pick Your Zura House";
        HouseCorousal.SetActive(true);
        HouseCorousalMint.SetActive(false);
        LoadingPanel.SetActive(false);
        CloseButton.SetActive(false);
    }
    public async void GetOwnedNFTMetadata()
    {
        await userProfile.fetchUserData();
        //Debug.Log("House Id is : " + HouseId);

        string houseId = PlayerPrefs.GetString("HouseId");
        //int houseId = NFTData.Instance.ids[0];
        Debug.Log("NFTDATA house id : " + houseId);

        // Check if the houseId string is not empty
        if (!string.IsNullOrEmpty(houseId))
        {
            //// Attempt to parse the houseId string into an integer
            if (int.TryParse(houseId, out int parsedHouseId))
            {
                ids.Add(parsedHouseId);
                ifHasHouse(parsedHouseId);
                //LeaderboardManager.Instance.PlayfabLogin();
                LeaderboardHouses.Instance.InvokwLeaderboard();
            }
            else
            {
                Debug.LogError("Failed to parse HouseId into an integer.");
            }
        }
        else
        {
            Debug.LogWarning("HouseId is empty.");
            HouseCorousalMint.SetActive(true);
        }
        walletConnectStatusText.text = "";
        loadingPanel.SetActive(true);

        Contract tempContract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);

        // Fetch owned NFTs
        //var data = await tempContract.ERC1155.GetOwned(walletAddress);
        var data = await tempContract.ERC1155.GetOwned(PlayerPrefs.GetString("WalletAddress"));
        int i = 0;
        foreach (var nft in data)
        {
            Debug.Log(nft.metadata.id);
            if (int.TryParse(nft.metadata.id, out int id))
            {
                //ids.Add(id);
                Debug.Log(id);
                //NFTData.Instance.ids.Add(id);
                PlayerPrefs.SetInt("NFT_ID", id);
                // Store each id under a unique key
                PlayerPrefs.SetString("NFT_ID_" + i, nft.metadata.id);
                i++;
            }

        }
        loadingPanel.SetActive(false);

        if (NavigateHouseButtons != null && HouseCorousal != null)
        {
            if (ids.Count == 0)
            {
                NavigateHouseButtons.SetActive(true);
                HouseCorousalMint.SetActive(true);
                CloseButton.SetActive(false);
                //StartCoroutine(ifHouse.ShowText());

            }
            else
            {
                HouseCorousalMint.SetActive(false);
                NavigateHouseButtons.SetActive(false);
                HouseCorousal.SetActive(true);
                CloseButton.SetActive(true);
                foreach (var id in ids)
                {
                    ifHasHouse(id);
                }
            }
        }


    }


    void ifHasHouse(int houseId)
    {
        ifHouse.HasHouseAtWalletConnect(houseId);
    }
    public void UpdateIdsList(int number)
    {
        ids.Add(number);


        foreach (var id in ids)
        {
            Debug.Log(id.ToString());
        }
    }

    public void OnButtonClicked(int tokenId)
    {
        MintHouse(tokenId);
    }


    public async void MintHouse(int tokenId)
    {
        try
        {
            var walletAddress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(HouseContract);
            MintButtons[tokenId].SetActive(false);
            LoadingPanel.SetActive(true);
            NavigateHouseButtons.SetActive(false);
            await contract.ERC1155.Claim(tokenId.ToString(), 1);
            LoadingPanel.SetActive(false);
            await userProfile.SetUserHouse(tokenId);
            GetOwnedNFTMetadata();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            LoadingPanel.SetActive(false);
            MintButtons[tokenId].SetActive(true);
            NavigateHouseButtons.SetActive(true);
        }
    }


}
