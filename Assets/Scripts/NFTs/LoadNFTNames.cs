using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Thirdweb;
using TMPro;
using System.Threading.Tasks;

public class LoadNFTNames : MonoBehaviour
{
    public string contractAddress = "0x4289Fb83C538700f42a96C8B34d857d0a51c4d67";
    private Contract contract;
    public TMP_Text[] nftSupplyTexts;


    void Start()
    {
        Debug.Log("Loading");
        contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        LoadNFTNamesFromContract();
    }

    public async void LoadNFTNamesFromContract()
    {

        var data = await contract.ERC1155.GetAll();
        int i = 0;
        foreach (var nft in data)
        {
            // Display supply
            int supply = nft.supply;
            nftSupplyTexts[i].text = supply.ToString() + " Minted so far";
            i++;
        }
    }
}
