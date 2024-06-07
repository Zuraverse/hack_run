using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using Thirdweb.Examples;

public class FetchWallet : MonoBehaviour
{
    //public Prefab_NFTLoader prefab_NFTLoader;
    public Prefab_NFTLoader prefab_NFTLoader;
    public TMPro.TextMeshProUGUI text;
    //[SerializeField] private ThirdwebSDK sdk;
    //public const string[] ContractAddress = {"0x812F35EbE8C50377eD8A53DF787cec41A9a1f1BF" , "0x4289Fb83C538700f42a96C8B34d857d0a51c4d67"}; //mumbai 1
    public string[] ContractAddress;
    //public const string ContractAddress = "0xA64372b2B90C64d7D943d7f8114849fd3B7A9140"; //Mumbai 2
    //public const string ContractAddress = "0xF9406f29dd14F1270FC719F06EFF9b09567cabDD"; //Mantle-testnet
    //public const string ContractAddress = "0x39691E1C32Ed33EBd775f51AA2568f1eD7eBCe7E";



    

    public GameObject Mint;
    public GameObject SelectSpaceship;
    public Color SelectSpaceship_fade;
    public Color SelectSpaceshipBright;
    public GameObject NFTPrefab;

    public GameObject FirstNFT;
    void Start()
    {
        Mint.SetActive(false);
        NFTPrefab.SetActive(false);
    }

    public async void WalletAddress()
    {
        var results = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        text.text = results;
        
    }

    public async void GetNFTBalance()
    {
        text.text = "Getting Balance";

        Contract contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress[0]);
        var result = await contract.ERC721.Balance();
        text.text = result;
    }


    public async void GetERC1155Balance()
{
    var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
    text.text = "Getting Balance";
    Contract contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress[0]);

    for (int tokenId = 0; tokenId <= 4; tokenId++)
    {
        var data = await contract.ERC1155.BalanceOf(address, tokenId.ToString());
        // Check the balance of the current token ID
        if (data == "0")
        {
            NFTPrefab.SetActive(true);
            Debug.Log("value is zero");
            Mint.SetActive(true);
            SelectSpaceship.GetComponent<Button>().enabled = false;
            SelectSpaceship.GetComponent<Image>().color = SelectSpaceship_fade;
            FirstNFT.SetActive(true);

        }
        if (data == "1")
        {
            Debug.Log("value is greater then Zero");
            Mint.SetActive(true);
            SelectSpaceship.GetComponent<Button>().enabled = true;
            SelectSpaceship.GetComponent<Image>().color = SelectSpaceshipBright;
            NFTPrefab.SetActive(true);
            NFTPrefab.GetComponent<Prefab_NFTLoader>().LoadNFTs();
            text.text = data;

            break; // Exit the loop when data is "1"
        }
    }
}


public async void claimNFT(string tokenId)
{
    try 
    {
        var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();

        text.text = "Claiming NFT";

        Contract contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress[1]);

        var data = await contract.ERC1155.ClaimTo(address, tokenId, 1);

        text.text = "NFT Claimed";


        prefab_NFTLoader.LoadNFTs();



    }
    catch(System.Exception)
    {
        Debug.Log("Error claiming NFT");
    }
}

public async void FirstNFTClaim(string token)
{
    try
    {
        var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        text.text = "Claiming NFT";
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress[0]);
        var data = await contract.ERC1155.ClaimTo(address, token, 1);
        text.text =  "Claimed";
        FirstNFT.SetActive(false);
        prefab_NFTLoader.LoadNFTs();

    }
    catch(System.Exception)
    {
        text.text = "Error Claiming";
    }
}

public void OnButtonClick(string tokenId)
{
    claimNFT(tokenId);
}


}
