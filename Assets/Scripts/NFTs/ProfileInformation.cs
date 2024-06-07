using UnityEngine;
using TMPro;
using Thirdweb;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;
using System;
using MetaMask.SocketIOClient;
using System.Globalization;

public class ProfileInformation : MonoBehaviour
{
    public TMP_Text[] KarmaPoints;
    public TMP_Text[] WalletAddress;
    public TMP_Text[] WalletBalance;
    public TMP_Text userName;
    private string _address;
    private Coroutine checkBalanceCoroutine;

    // This method is called when the script is enabled
    private void OnEnable()
    {
        string _address = PlayerPrefs.GetString("WalletAddress");
        // Start the coroutine that checks the balance every 10 seconds
        checkBalanceCoroutine = StartCoroutine(CheckBalancePeriodically());
    }

    // This method is called when the script is disabled
    private void OnDisable()
    {
        // Stop the coroutine to prevent memory leaks or unexpected behavior
        if (checkBalanceCoroutine != null)
        {
            StopCoroutine(checkBalanceCoroutine);
            checkBalanceCoroutine = null;
        }
    }

    private IEnumerator CheckBalancePeriodically()
    {
        while (true)
        {
            // Call the CheckBalance method
            CheckBalance();

            // Wait for 10 seconds before the next iteration
            yield return new WaitForSeconds(45f);
        }
    }

    public void ShowKarmaPointsOnMainScene()
    {
        // Update KarmaPoints text
        string karmaPoints = PlayerPrefs.GetString("karmaPoints");
        userName.text = PlayerPrefs.GetString("UserName");
        foreach (var textComponent in KarmaPoints)
        {
            textComponent.text = karmaPoints;
        }
        // Update all wallet address text elements with shortened address
        string walletAddress = PlayerPrefs.GetString("WalletAddress");
        string shortenedAddress = ShortenAddress(walletAddress);

        foreach (var textComponent in WalletAddress)
        {
            textComponent.text = shortenedAddress;
        }

        // Update all wallet balance text elements with stored balance
        string walletBalance = PlayerPrefs.GetString("WalletBalance");

        foreach (var textComponent in WalletBalance)
        {
            textComponent.text = walletBalance;
        }
        CheckBalance();
    }

    public string ShortenAddress(string address)
    {
        if (address.Length != 42)
            throw new ArgumentException("Invalid Address Length.");
        return $"{address[..6]}...{address[38..]}";
    }

    public async void CheckBalance()
    {
        var bal = await ThirdwebManager.Instance.SDK.wallet.GetBalance();
        var balStr = $"{bal.value.ToEth()} {bal.symbol}";

        // Update all wallet balance text components
        foreach (var textComponent in WalletBalance)
        {
            textComponent.text = balStr;
        }
    }


    public void CopyAddress()
    {

        GUIUtility.systemCopyBuffer = _address;
        ThirdwebDebug.Log($"Copied address to clipboard: {_address}");
    }



    public async void Disconnect()
    {

        ThirdwebDebug.Log("Disconnecting...");
        try
        {
            await ThirdwebManager.Instance.SDK.wallet.Disconnect();
            NFTData.Instance.ClearAllData();
            SceneManager.LoadScene("ConnectWallet");
            GameObject MusicSource = GameObject.Find("MusicSource");
            Destroy(MusicSource);

        }
        catch (System.Exception e)
        {
            ThirdwebDebug.LogError($"Failed to disconnect: {e}");
        }
    }
}
