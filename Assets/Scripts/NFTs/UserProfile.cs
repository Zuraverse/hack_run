using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using Thirdweb;
using CosmicSurfer;
using System;

public class UserProfile : MonoBehaviour
{
    public static UserProfile Instance { get; private set; }
    public string urlForGetInfo = "https://zuramarket.xyz/api/user/";
    public string urlForPutInfo = "https://zuramarket.xyz/api/profile/create";
    public string urlForKP = "https://zuramarket.xyz/api/karma/update";
    public string urlForHouseUpdate = "https://zuramarket.xyz/user/profile/update";
    public string urlForSendEmail = "https://zuramarket.xyz/api/email/sendmail";
    public string urlForVerifyOTP = "https://zuramarket.xyz/api/email/verify";
    public string pasInfo;
    public int tokenId;
    public string contractAddress = "0x3AeC9067b6d0E9Cc4bD05f7842397b906E2753cf";
    public List<int> housIds = new List<int>();
    public TMP_Text username;
    public TMP_Text karmaPoints;
    //public TMP_Text carbonOffset;
    [SerializeField] private Image ProfilePictureImage;
    public LeaderboardLogin leaderboardLogin;
    [SerializeField] private GameObject ProfileUpdatePanel;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField otpInputField;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject HousMitingPanel;
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private GameObject OTPPanel;
    [SerializeField] private GameObject[] HouseNFTMain;
    [SerializeField] private GameObject PlayButton;
    // Define a class to represent your request object
    [System.Serializable]
    public class RequestObject
    {
        public string id;
        public string userName;
        public string email;
        public bool hasHouse;
        public string profileImg;
        public int karmabalance;
        public int hasHouseId;
        public string walletAddress;
        public string receiver;
        public int otp;
    }

    [System.Serializable]
    public class NewUserReuetsBody
    {
        public string zurawallet;
        public string hasHouseId;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ProfileUpdatePanel.SetActive(false);
        OTPPanel.SetActive(false);
    }



    public async Task fetchUserData()
    {
        string email = PlayerPrefs.GetString("WalletAddress");

        UnityWebRequest www = UnityWebRequest.Get(urlForGetInfo);
        www.SetRequestHeader("zurawallet", email);

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            // If user not found, send a PUT request with the wallet address

        }
        else
        {
            string responseBody = www.downloadHandler.text;


            Debug.Log("Response Body : " + responseBody);
            Debug.Log(responseBody);

            // If the response body is null or empty, call SendWalletAddress
            if (string.IsNullOrEmpty(responseBody))
            {
                Debug.Log("Response body is null or empty. Sending wallet address...");

                await SendWalletAddress(PlayerPrefs.GetString("WalletAddress"));
                return;
            }

            UserInfo userInfo = JsonUtility.FromJson<UserInfo>(responseBody);
            string username = userInfo.userName;
            string hasHouseid = userInfo.hasHouseId;
            string hasEmail = userInfo.email;
            Debug.Log(hasEmail);
            //string carbonOffset = userInfo.carbonOffset;
            string karmaPoints = userInfo.karmabalance;
            string profilePicture = userInfo.profileImg;

            if (!string.IsNullOrEmpty(hasHouseid))
            {
                if (!string.IsNullOrEmpty(username))
                {
                    if (!string.IsNullOrEmpty(hasEmail))
                    {
                        NFTData.Instance.ClearAllData();
                        PlayerPrefs.DeleteKey("NFT_ID");
                        NFTData.Instance.ids.Add(int.Parse(hasHouseid));
                        PlayerPrefs.SetInt("NFT_ID", int.Parse(hasHouseid));
                        PlayerPrefs.SetString("HouseId", hasHouseid);
                        PlayerPrefs.SetString("UserName", username);
                        //PlayerPrefs.SetString("CarbonOffset", carbonOffset);
                        PlayerPrefs.SetString("karmaPoints", karmaPoints);
                        ActivateObjectsByIDsList();

                        if (PlayButton != null)
                        {
                            PlayButton.SetActive(true);
                        }
                        //StartCoroutine(LoadImage(profilePicture));
                        Debug.Log("PlayerPrefs House Id : " + PlayerPrefs.GetString("HouseId"));
                        leaderboardLogin.Loginn();
                    }
                    else
                    {
                        HousMitingPanel.SetActive(true);
                        ProfileUpdatePanel.SetActive(true);
                    }
                    
                }
                else
                {
                    HousMitingPanel.SetActive(true);
                    ProfileUpdatePanel.SetActive(true);
                }

            }
            else
            {
                HousMitingPanel.SetActive(true);
                ProfileUpdatePanel.SetActive(false);
                Debug.Log("hasHouseId is null");
                PlayerPrefs.DeleteKey("HouseId");
            }
            Debug.Log("Username: " + username);
            Debug.Log("House Id : " + hasHouseid);
        }
        setProfile();
    }


    private async Task CheckingHouseIfAny()
    {
        Debug.Log("Checking House if Any");
        Contract tempContract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        var data = await tempContract.ERC1155.GetOwned(PlayerPrefs.GetString("WalletAddress"));
        int i = 0;
        foreach (var nft in data)
        {
            Debug.Log(nft.metadata.id);
            if (int.TryParse(nft.metadata.id, out int id))
            {
                housIds.Add(id);
                Debug.Log(id);
                i++;
            }
        }
    }
    public async Task SendWalletAddress(string walletAddress)
    {

        //checking House if any before sending walletaddress
        //await CheckingHouseIfAny();

        Debug.Log("walletAddress : " + walletAddress);

        // Create a new RequestObject with the wallet address
        NewUserReuetsBody requestObject = new NewUserReuetsBody();
        requestObject.zurawallet = walletAddress;

        // Convert the RequestObject to a JSON string
        string jsonBody = JsonUtility.ToJson(requestObject);

        // Convert the JSON string to a byte array
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        // Create a new UnityWebRequest and attach the byte array as the uploadHandler
        UnityWebRequest www = new UnityWebRequest(urlForPutInfo, "POST");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Set the Content-Type header to application/json
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            //foreach(var nft in HouseNFTMain)
            //{
            //    nft.SetActive(false);
            //}
            Debug.LogError(www.error);
        }
        else
        {
            //string responseBody = www.downloadHandler.text;
            //Debug.Log(responseBody);
            //Debug.Log("Retrieved Info");
            await fetchUserData();
        }
        //if (housIds.Count > 0) //sending WalletAddress with HouseIds
        //{
        //    // Create a new RequestObject with the wallet address
        //    NewUserReuetsBody requestObject = new NewUserReuetsBody();
        //    requestObject.zurawallet = walletAddress;
        //    requestObject.hasHouseId = housIds[0].ToString();

        //    // Convert the RequestObject to a JSON string
        //    string jsonBody = JsonUtility.ToJson(requestObject);

        //    // Convert the JSON string to a byte array
        //    byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        //    // Create a new UnityWebRequest and attach the byte array as the uploadHandler
        //    UnityWebRequest www = new UnityWebRequest(urlForPuInfo, "POST");
        //    www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        //    // Set the Content-Type header to application/json
        //    www.SetRequestHeader("Content-Type", "application/json");

        //    // Send the request
        //    var asyncOperation = www.SendWebRequest();

        //    while (!asyncOperation.isDone)
        //    {
        //        await Task.Yield();
        //    }

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.LogError(www.error);
        //    }
        //    else
        //    {
        //        //string responseBody = www.downloadHandler.text;
        //        //Debug.Log(responseBody);
        //        //Debug.Log("Retrieved Info");
        //        //await SetUserHouse(housIds[0]);
        //        await fetchUserData();
        //    }
        //}
        //else //sending WalletAddress without HouseIds
        //{
        //    // Create a new RequestObject with the wallet address
        //    NewUserReuetsBody requestObject = new NewUserReuetsBody();
        //    requestObject.zurawallet = walletAddress;

        //    // Convert the RequestObject to a JSON string
        //    string jsonBody = JsonUtility.ToJson(requestObject);

        //    // Convert the JSON string to a byte array
        //    byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        //    // Create a new UnityWebRequest and attach the byte array as the uploadHandler
        //    UnityWebRequest www = new UnityWebRequest(urlForPuInfo, "POST");
        //    www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        //    // Set the Content-Type header to application/json
        //    www.SetRequestHeader("Content-Type", "application/json");

        //    // Send the request
        //    var asyncOperation = www.SendWebRequest();

        //    while (!asyncOperation.isDone)
        //    {
        //        await Task.Yield();
        //    }

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.LogError(www.error);
        //    }
        //    else
        //    {
        //        //string responseBody = www.downloadHandler.text;
        //        //Debug.Log(responseBody);
        //        //Debug.Log("Retrieved Info");
        //        await fetchUserData();
        //    }
        //}

    }




    IEnumerator LoadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //renderer.material.mainTexture = texture;
            ProfilePictureImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); ;
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    [System.Serializable]
    public class UserInfo
    {
        public string userName;
        public string hasHouseId;
        public string email;
        //public string carbonOffset;
        public string karmabalance;
        public string profileImg;

    }

    public async Task SetUserHouse(int tokenId)
    {
        //Debug.Log("Updating HasHouse On Database with TokenID : " + tokenId);

        RequestObject requestObject = new RequestObject();
        requestObject.hasHouseId = tokenId;

        string jsonBody = JsonUtility.ToJson(requestObject);

        UnityWebRequest www = UnityWebRequest.Put(urlForHouseUpdate, jsonBody);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            //Debug.Log(responseBody);
            //Debug.Log("Retrieved Info");
        }
        setProfile();
    }

    public void setProfile()
    {
        username.text = PlayerPrefs.GetString("UserName");
        karmaPoints.text = PlayerPrefs.GetString("karmaPoints");
        //carbonOffset.text = PlayerPrefs.GetString("CarbonOffset");
    }



    //this block of code Giving Error in webgl 
    public async Task SendUserData(int Value)
    {
        Debug.Log("Updating Data On Database with Key : " + Value);
        RequestObject requestObject = new RequestObject();
        requestObject.karmabalance = Value;

        string jsonBody = JsonUtility.ToJson(requestObject);

        UnityWebRequest www = UnityWebRequest.Put(urlForKP, jsonBody);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            //Debug.Log(responseBody);
            //Debug.Log("Retrieved Info");

            // Fetch user data immediately after updating data
            await fetchUserData();
        }
    }

    // This block of code will not compile in WebGL
    //#if !UNITY_WEBGL
    //    public async Task SendUserData(int value)
    //    {
    //        Debug.Log("Updating Data On Database with Key: " + value);

    //        RequestObject requestObject = new RequestObject();
    //        requestObject.karmabalance = value;

    //        string jsonBody = JsonUtility.ToJson(requestObject);

    //        UnityWebRequest www = UnityWebRequest.Put(urlForKP, jsonBody);
    //        www.SetRequestHeader("Content-Type", "application/json");
    //        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

    //        var asyncOperation = www.SendWebRequest();

    //        while (!asyncOperation.isDone)
    //        {
    //            await Task.Yield();
    //        }

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.LogError(www.error);
    //        }
    //        else
    //        {
    //            string responseBody = www.downloadHandler.text;
    //            Debug.Log("Response: " + responseBody);
    //            Debug.Log("Retrieved Info");

    //            // Fetch user data immediately after updating data
    //            await fetchUserData();
    //        }
    //    }
    //#endif
    public async void SendUserInformation()
    {
        //HousMitingPanel.SetActive(false);
        //usernameInputField.text = PlayerPrefs.GetString("UserName");
        string username = usernameInputField.text;
        string email = emailInputField.text;
        //await SetUserName(username, email);
        //await EmailSend(username, email);
        await CheckAndSendOTP();
    }

    public async Task CheckAndSendOTP()
    {
        string wallet = PlayerPrefs.GetString("WalletAddress");
        string email = emailInputField.text;
        string username = usernameInputField.text;

        if (string.IsNullOrEmpty(username.Trim()))
        {
            statusText.text = "Username cannot be empty!";
            Debug.Log("Username input field is empty.");
            return;
        }

        UnityWebRequest www = UnityWebRequest.Get(urlForGetInfo);
        www.SetRequestHeader("zurawallet", wallet);

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
            statusText.text = "Error fetching user data!";
            return;
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            Debug.Log("Response Body: " + responseBody);

            // If the response body is null or empty, proceed to check and send OTP
            if (!string.IsNullOrEmpty(responseBody))
            {
                Debug.Log("Response body is null or empty. Proceeding to check username and send OTP...");

                // Try to set the username before sending the email
                bool isUsernameSet = await TrySetUserName(username);
                if (isUsernameSet)
                {
                    statusText.text = "";
                    await EmailSend(username, email);
                }
                else
                {
                    statusText.text = "Username already exists!";
                }
                return;
            }

            //UserInfo userInfo = JsonUtility.FromJson<UserInfo>(responseBody);
            //string existingUsername = userInfo.userName;

            //if (!string.IsNullOrEmpty(existingUsername))
            //{
            //    statusText.text = "Username already exists!";
            //    await EmailSend(username, email);
            //    Debug.Log("Username already exists: " + existingUsername);
            //}
            //else
            //{
            //    statusText.text = "";
            //    // Try to set the username before sending the email
            //    bool isUsernameSet = await TrySetUserName(username);
            //    if (isUsernameSet)
            //    {
            //        await EmailSend(username, email);
            //    }
            //    else
            //    {
            //        statusText.text = "Username already exists!";
            //    }
            //}
        }
    }

    public async Task<bool> TrySetUserName(string username)
    {
        RequestObject requestObject = new RequestObject();
        requestObject.userName = username.Trim().ToLower();

        string jsonBody = JsonUtility.ToJson(requestObject);

        UnityWebRequest www = UnityWebRequest.Put(urlForHouseUpdate, jsonBody);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            //statusText.text = "username already exist!";
            Debug.LogError(www.error);
            return false;
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            Debug.Log("SetUserName response: " + responseBody);
            // If we reach here, setting the username was successful
            return true;
        }
    }

    public async void VerifyOneTimePassword()
    {
        string email = emailInputField.text;
        // Assuming otpInputField is a UI element that contains the OTP as text
        string otp = otpInputField.text;

        int parsedOtp;
        bool isParsed = int.TryParse(otp, out parsedOtp);
        if (isParsed)
        {
            // Successfully parsed the OTP
            Debug.Log("Parsed OTP: " + parsedOtp);
        }
        else
        {
            // Failed to parse the OTP
            Debug.LogError("Failed to parse OTP");
        }
        //int otp = int.Parse(otpInputField.text);
        await VerifyEmail(parsedOtp, email);
    }

    public async Task EmailSend(string username, string email = "")
    {
        RequestObject requestObject = new RequestObject();
        requestObject.receiver = email;

        // Only include email in the request if it's provided
        if (!string.IsNullOrEmpty(email))
        {
            requestObject.receiver = email;
        }

        string jsonBody = JsonUtility.ToJson(requestObject);

        UnityWebRequest www = UnityWebRequest.Put(urlForSendEmail, jsonBody);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (www.isNetworkError || www.isHttpError)
        {
            statusText.text = "Something Went Wrong!";
            Debug.LogError(www.error);
            return;
        }
        else
        {
            statusText.text = "Please enter your OTP";
            string responseBody = www.downloadHandler.text;
            //Debug.Log(responseBody);
            //Debug.Log("Retrieved Info");
            emailPanel.SetActive(false);
            OTPPanel.SetActive(true);
        }
    }

    public async Task VerifyEmail(int otp, string email)
    {
        // Create the request object and populate it
        RequestObject requestObject = new RequestObject
        {
            otp = otp,
            email = email
        };

        // Convert the request object to a JSON string
        string jsonBody = JsonUtility.ToJson(requestObject);

        // Create a new UnityWebRequest for a POST request
        using (UnityWebRequest www = new UnityWebRequest(urlForVerifyOTP, "POST"))
        {
            // Attach the JSON data to the request body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Set the content type and other headers
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

            // Send the request and wait for the response
            var asyncOperation = www.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            // Check for network or HTTP errors
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                statusText.text = "Please Enter Correct OTP!";
                Debug.LogError(www.error);
                return;
            }
            else
            {
                statusText.text = "";
                string responseBody = www.downloadHandler.text;
                //Debug.Log(responseBody);
                //Debug.Log("Retrieved Info");
                PlayerPrefs.SetString("email", emailInputField.text);
                // Perform additional actions after a successful response
                await SetUserName(usernameInputField.text, emailInputField.text);
            }
        }
    }



    public async Task SetUserName(string username, string email = "")
    {
        //RequestObject requestObject = new RequestObject();
        //requestObject.userName = username.Trim().ToLower();

        //// Only include email in the request if it's provided
        //if (!string.IsNullOrEmpty(email))
        //{
        //    requestObject.email = email;
        //}

        //string jsonBody = JsonUtility.ToJson(requestObject);

        //UnityWebRequest www = UnityWebRequest.Put(urlForHouseUpdate, jsonBody);
        //www.SetRequestHeader("Content-Type", "application/json");
        //www.SetRequestHeader("zurawallet", PlayerPrefs.GetString("WalletAddress"));

        //var asyncOperation = www.SendWebRequest();

        //while (!asyncOperation.isDone)
        //{
        //    await Task.Yield();
        //}

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    statusText.text = "username already exist!";
        //    Debug.LogError(www.error);
        //    return;
        //}
        //else
        //{
        //    statusText.text = "";
        //    string responseBody = www.downloadHandler.text;
        //    //Debug.Log(responseBody);
        //    //Debug.Log("Retrieved Info");
        //    OTPPanel.SetActive(false);
        //    HousMitingPanel.SetActive(false);
        //    ProfileUpdatePanel.SetActive(false);
        //    // Fetch user data immediately after updating data
        //    await fetchUserData();
        //}

        statusText.text = "";
        OTPPanel.SetActive(false);
        HousMitingPanel.SetActive(false);
        ProfileUpdatePanel.SetActive(false);
        // Fetch user data immediately after updating data
        await fetchUserData();
    }


    public void ActivateObjectByIndex(int index)
    {
        if (index >= 0 && index < HouseNFTMain.Length)
        {
            HouseNFTMain[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Index out of range.");
        }
    }


    public void ActivateObjectsByIDsList()
    {
        foreach (int id in NFTData.Instance.ids)
        {
            ActivateObjectByIndex(id);
        }
    }

}
