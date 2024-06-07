using System.Collections;
using System.Collections.Generic;
using Thirdweb;
using UnityEngine;

public class CheckHashNFT : MonoBehaviour
{
    public GameObject loadingPanel;
    private string hashContractAddress = "0xc5790ea346D9EDB4F327196774a5cA1667b80308";
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        CheckHashInWallet();
    }

    private async void CheckHashInWallet()
    {

        loadingPanel.SetActive(true);

        Contract tempContract = ThirdwebManager.Instance.SDK.GetContract(hashContractAddress);

        // Fetch owned NFTs
        //var data = await tempContract.ERC1155.GetOwned(walletAddress);
        var data = await tempContract.ERC1155.GetOwned(PlayerPrefs.GetString("WalletAddress"));
        int i = 0;
        foreach (var nft in data)
        {
            Debug.Log(nft.metadata.id);
            if (int.TryParse(nft.metadata.id, out int id))
            {
                NFTData.Instance.Hashids.Add(id);
                i++;
            }

        }
        loadingPanel.SetActive(false);

        if (NFTData.Instance.Hashids.Count == 0)
        {
            Debug.Log("No owned Hash NFTs found.");
        }
        else
        {

        }
    }
}

