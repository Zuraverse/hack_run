using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NFTData : MonoBehaviour
{
    // Singleton instance
    public static NFTData Instance { get; private set; }

    // List to store NFT ids
    public List<int> ids = new List<int>();
    public List<int> Hashids = new List<int>();

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If not, set this instance as the singleton instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }


    public void ClearAllData()
    {
        ids = new List<int>();
        Hashids = new List<int>();
    }

    public void ClearPlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
    }


}
