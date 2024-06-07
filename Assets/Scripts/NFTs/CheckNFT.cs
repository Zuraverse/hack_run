using System.Collections.Generic;
using Thirdweb;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace CosmicSurfer
{
    public class CheckNFT : MonoBehaviour
    {
        public static CheckNFT Instance { get; private set; }
        [SerializeField] private string contractAddress;

        public GameObject loadingPanel;
        public GameObject HouseMintingPaneal;
        public UserProfile userProfile;
        public IfHouse ifHouse;
        //public LeaderboardLogin leaderboardLogin;

        List<int> ids = new List<int>();

        private void Awake()
        {
            // Check if an instance already exists
            if (Instance == null)
            {
                // If not, set this instance as the singleton instance
                Instance = this;
            }
            ids.Clear();
        }
        private void Start()
        {
            HouseMintingPaneal.SetActive(false);
            //GetOwnedNFTMetadata();
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
                    //leaderboardLogin.Loginn();
                }
                else
                {
                    Debug.LogError("Failed to parse HouseId into an integer.");
                }
            }
            else
            {
                Debug.LogWarning("HouseId is empty.");
                HouseMintingPaneal.SetActive(true);
            }
            //walletConnectStatusText.text = "";
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
                    //NFTData.Instance.ids.Add(id);
                    PlayerPrefs.SetInt("NFT_ID", id);
                    // Store each id under a unique key
                    PlayerPrefs.SetString("NFT_ID_" + i, nft.metadata.id);
                    i++;
                }

            }
            loadingPanel.SetActive(false);
            if (ids.Count == 0)
            {
                HouseMintingPaneal.SetActive(true);

                //testNftmint();
                Debug.Log("No owned NFTs found.");
            }
            else
            {
                //HouseMintingPaneal.SetActive(false);
                //for (int j = 0; j < ids.Count; j++)
                //{
                //    Debug.Log(ids[j]);
                //}
                //ActivateObjectsByIDsList();
                //PlayButton.SetActive(true);
                //AddIds();
            }
        }
        public void AddIds()
        {
            //NFTData.Instance.ids.Add(1);
            //NFTData.Instance.ids.Add(5);
        }


        void ifHasHouse(int houseId)
        {
            ifHouse.HasHouseAtWalletConnect(houseId);
        }

        public void testNftmint()
        {
            NFTData.Instance.ids.Add(4);
            if (NFTData.Instance.ids.Count == 1)
            {
                HouseMintingPaneal.SetActive(false);
                Debug.Log("No owned NFTs found.");
                //ActivateObjectsByIDsList();
                //PlayButton.SetActive(true);
                PlayerPrefs.SetInt("NFT_ID", 1);
            }
        }




        //public void ActivateObjectByIndex(int index)
        //{
        //    if (index >= 0 && index < HouseNFTMain.Length)
        //    {
        //        HouseNFTMain[index].SetActive(true);
        //    }
        //    else
        //    {
        //        Debug.LogError("Index out of range.");
        //    }
        //}


        //public void ActivateObjectsByIDsList()
        //{
        //    foreach (int id in NFTData.Instance.ids)
        //    {
        //        ActivateObjectByIndex(id);
        //    }
        //}

    }


}
