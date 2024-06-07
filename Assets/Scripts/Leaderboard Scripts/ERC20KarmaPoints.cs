using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Thirdweb;

public class ERC20KarmaPoints : MonoBehaviour
{
    public UserTokenData userTokenData;

    //public GameObject ClaimPrompt;
    public GameObject hasNotClaimedState;
    public GameObject ClaimingState;
    public GameObject hasClaimedState;
    public TMPro.TextMeshProUGUI YourKarmaPoints;
    private const string currencyName = "KT";
    public GameObject ClaimPanel;
    public ParticleSystem ParticleEffect;
    public AudioSource Audio;

    //[SerializeField] private TMPro.TextMeshProUGUI tokenBalanceText;
    //[SerializeField] public TMPro.TextMeshProUGUI kpEarnedText;

    //private const string DROP_ERC20_CONTRACT = "0xdA42F63F5c454a541cfa45b739A6B2bAe9F843b1"; //mentle-testnet
    private const string DROP_ERC20_CONTRACT = "0xf79FF51Ba39f241400bF0C0338d6a54B3ef1797C"; //mumbai

    public int tokenValue; // Integer variable to store the token value

    
    // Start is called before the first frame update
    void Start()
    {
        ClaimingState.SetActive(false);
        hasClaimedState.SetActive(false);
        ParticleEffect.Stop();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void GetTokenBalance(){
        try
        {
            var address = await  ThirdwebManager.Instance.SDK.wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            var data = await contract.ERC20.BalanceOf(address);
            YourKarmaPoints.text = "$ZT: " + data.displayValue;
        }
        catch (System.Exception)
        {
            Debug.Log("Error Getting token balance");
        }
    }

    //ERC20 
    public async void MintKarmaPoints(int Value)
    {
        Debug.Log(Value);
        try
        {
            Debug.Log("Minting Karma Points Hold On..");
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            hasNotClaimedState.SetActive(false);
            ClaimingState.SetActive(true);
            var result = await contract.ERC20.Claim(Value.ToString());
            SubtractCurrencyFromPlayer(Value);
            Debug.Log("Karma Points minted");
            ClaimingState.SetActive(false);
            hasClaimedState.SetActive(true);
            Invoke("SetOffClaimPanel", 5f);

        }
        catch (System.Exception)
        {
            Debug.Log("Error minting Karma Point Server Not responding..");
        }
        
    }

    public void SubtractCurrencyFromPlayer(int Amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = currencyName,
            Amount = Amount
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractSuccess, OnSubtractFailure);
    }

    void OnSubtractSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Successfully subtracted currency!");
        Audio.Play();
        ParticleEffect.Play();
        GetTokenBalance();
        userTokenData.GetPlayerKarmaPoints();
    }

    void OnSubtractFailure(PlayFabError error)
    {
        Debug.LogError("Failed to subtract currency: " + error.GenerateErrorReport());
    }

    public void GetTokenData(int TokenData){
        tokenValue = TokenData;
    }


    public void MintTheZuraTokens(){
        MintKarmaPoints(tokenValue);
        Debug.Log("Data Sent! " + tokenValue);
    }

    public void SetOffClaimPanel(){
        ClaimPanel.SetActive(false);
    }
}
