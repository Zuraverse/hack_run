using Thirdweb;
using UnityEngine;
using TMPro;
using CosmicSurfer;
using System.Threading.Tasks;
using UnityEngine.UI;

public class MintNFT : MonoBehaviour
{

    public TMP_Text text;
    public const string ContractAddress = "0x4213E33A24BaD8dFb7F23c3cD6A6a2e164b0E034";
    public GameObject LoadingBar;
    public UserProfile userProfile;
    public GameObject HousMitingPanel;
    public GameObject[] MintButtons;


    private void Start()
    {
        text.text = "Please pick a house.";
        LoadingBar.SetActive(false);
        HousMitingPanel.SetActive(false);
    }
    public async void claimNFT(string tokenId)
    {
        foreach (var button in MintButtons)
        {
            button.GetComponent<Button>().enabled = false;
        }
        try
        {
            var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();

            //text.text = "Claiming NFT";
            //gameObject.SetActive(false);
            LoadingBar.SetActive(true);

            Contract contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);

            var data = await contract.ERC1155.ClaimTo(address, tokenId, 1);

            text.text = "NFT Claimed";
            LoadingBar.SetActive(false);
            HousMitingPanel.SetActive(false);
            int tokenIdInt = int.Parse(tokenId);
            await userProfile.SetUserHouse(tokenIdInt);
            CheckNFT.Instance.GetOwnedNFTMetadata();

        }
        catch (System.Exception)
        {
            Debug.Log("Error claiming NFT, Please retry");
            LoadingBar.SetActive(false);
            gameObject.SetActive(true);
            foreach (var button in MintButtons)
            {
                button.GetComponent<Button>().enabled = true;
            }
        }
    }

    public void OnButtonClick(string tokenId)
    {
        Debug.Log(tokenId);
        claimNFT(tokenId);
    }
    
    public async void testMint()
    {
        NFTData.Instance.ClearAllData();
        NFTData.Instance.ids.Add(4);
        //CheckNFT.Instance.testNftmint();
        CheckNFT.Instance.GetOwnedNFTMetadata();
        await userProfile.SetUserHouse(4);
    }
}
