using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevels : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    public GameObject panelC;
    public GameObject panelD;
    public GameObject panelE;
    public GameObject panelF;
    public GameObject panelG;
    public GameObject panelH;
    public GameObject panelI;
    public GameObject panelJ;
    public GameObject panelK;


    public void StartGlowStart()
    {
        string level = PlayerPrefs.GetString("PlayerLevel");

        LevelStars(int.Parse(level) + 1);
    }

    // Method to handle dropdown value change
    void LevelStars(int change)
    {
        // Disable all panels first
        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);
        panelE.SetActive(false);
        panelF.SetActive(false);
        panelG.SetActive(false);
        panelH.SetActive(false);
        panelI.SetActive(false);
        panelJ.SetActive(false);
        panelK.SetActive(false);
        // Enable the corresponding panel based on the selected value
        switch (change)
        {
            case 1:
                panelA.SetActive(true);
                break;
            case 2:
                panelB.SetActive(true);
                break;
            case 3:
                panelC.SetActive(true);
                break;
            case 4:
                panelD.SetActive(true);
                break;
            case 5:
                panelE.SetActive(true);
                break;
            case 6:
                panelA.SetActive(true);
                break;
            case 7:
                panelB.SetActive(true);
                break;
            case 8:
                panelC.SetActive(true);
                break;
            case 9:
                panelD.SetActive(true);
                break;
            case 10:
                panelE.SetActive(true);
                break;
            case 11:
                panelE.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid Levels value!");
                break;
        }
    }
}
