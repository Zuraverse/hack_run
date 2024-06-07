using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Thirdweb;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SupplyLoader : MonoBehaviour
{
    public TMP_Text totalSupply;

    [Header("UI ELEMENTS")]
    public Image nftImage;
    public TMP_Text nftName;
    public Button nftButton;
    public TMP_Text descriptionText; 
    public TMP_Text totalsSupply;


    public async void TotalSupply(NFT nft)
    {
        nftName.text = nft.metadata.name;
        nftImage.sprite = await ThirdwebManager.Instance.SDK.storage.DownloadImage(nft.metadata.image);
        // Get supply
        //int ownedSupply = nft.quantityOwned;
        int supply = nft.supply;

        // Display supply
        totalSupply.text = supply.ToString() + " Minted so far";


        nftButton.onClick.RemoveAllListeners();
        nftButton.onClick.AddListener(() => DoSomething(nft));
    }


    
    void DoSomething(NFT nft)
    {
        //Debugger.Instance.Log(nft.metadata.name, nft.ToString());
    }


}
