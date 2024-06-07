using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IfHouse : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    public GameObject panelC;
    public GameObject panelD;
    public GameObject panelE;
    //public GameObject houseCorousal;
    //public GameObject navigatebuttons;
    //public TMP_Text LoginStatusText;

    //public float delay = 0.1f;
    //public string fullText;

    //public string currentText = "";
    //public TMP_Text textComponent;

    public void HasHouseAtWalletConnect(int houseId)
    {
        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);
        panelE.SetActive(false);



        switch (houseId + 1)
        {
            case 1:
                panelA.SetActive(true);
                //textComponent.text = "Welcome to Datura house : A tranquil haven promoting calm, dependability, and thoughtfulness. Ideal for those who value a peaceful atmosphere," +
                //    " seek reliability, structure, and enjoy deep introspection�a blue house for a serene and contemplative life.";
                //StartCoroutine(ShowText());
                break;
            case 2:
                panelB.SetActive(true);
                //textComponent.text = "Welcome to Iboga house : A unique retreat celebrating individuality, fostering creativity, and an enigmatic charm. If you embrace uniqueness," +
                //    " value creativity, and enjoy an air of mystery, the purple house is your distinctive retreat.";
                //StartCoroutine(ShowText());
                break;
            case 3:
                panelC.SetActive(true);
                //textComponent.text = "Welcome to Peyote house : An optimistic retreat, a bright and sunny space for those with a cheerful disposition and a love for creativity," +
                //    " offering a warm and welcoming atmosphere to find joy in everyday life.";
                //StartCoroutine(ShowText());
                break;
            case 4:
                panelD.SetActive(true);
                //textComponent.text = "Welcome to Ayahuasca house : A nature oasis, surrounded by lush greenery, this house is ideal for nature lovers who value eco-friendly living," +
                //   " seek balance, and appreciate a nurturing environment, offering a perfect space for those with a deep connection to the outdoors.";
                //StartCoroutine(ShowText());
                break;
            case 5:
                panelE.SetActive(true);
                //textComponent.text = "Welcome to Kava house : An energetic haven for thrill-seekers who thrive on challenges and embrace a dynamic, fast-paced lifestyle in a lively space.";
                //StartCoroutine(ShowText());
                break;
        }
    }


    //public void setTrue()
    //{
    //    textComponent.text = "";
    //    LoginStatusText.text = "Connect Wallet and Pick Your Zura House";
    //    houseCorousal.SetActive(true);
    //    navigatebuttons.SetActive(true);
    //}

    //public IEnumerator ShowText()
    //{
    //    for (int i = 0; i <= fullText.Length; i++)
    //    {
    //        currentText = fullText.Substring(0, i);
    //        textComponent.text = currentText;
    //        yield return new WaitForSeconds(delay);
    //    }
    //}

    public void SetFalseHouse()
    {
        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);
        panelE.SetActive(false);
    }
}
