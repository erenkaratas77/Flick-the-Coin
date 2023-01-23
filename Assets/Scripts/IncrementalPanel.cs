using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IncrementalPanel : MonoBehaviour
{
    public static IncrementalPanel instance;
    // Start is called before the first frame update
    public int startLVL, incomeLVL;
    public GameObject StartCoin, Income;
    void Start()
    {
        instance = this;

        if (PlayerPrefs.HasKey("startLVL")) startLVL = PlayerPrefs.GetInt("startLVL");
        else startLVL = 1;
        if (PlayerPrefs.HasKey("incomeLVL")) incomeLVL = PlayerPrefs.GetInt("incomeLVL");
        else incomeLVL = 1;

        StartCoin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL " + startLVL;
        StartCoin.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + startLVL * 100;

        Income.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL " + incomeLVL;
        Income.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + incomeLVL * 100;

        Player.instance.transform.GetComponentInChildren<TextMeshPro>().text = startLVL + "$";
        Player.instance.myTotalMoney = startLVL;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void upgrageStartLVL()
    {
        if (LevelManager.instance.myMoneyInGame >= startLVL * 100)
        {
            HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
            PlayerPrefs.SetInt("myMoneyInGame", LevelManager.instance.myMoneyInGame - startLVL * 100);
            LevelManager.instance.myMoneyInGame = PlayerPrefs.GetInt("myMoneyInGame");
            UpperPanel.instance.myMoneyInGameText.text = LevelManager.instance.myMoneyInGame + "";

            startLVL += 1;
            StartCoin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL " + startLVL;
            StartCoin.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + startLVL * 100;

            Player.instance.transform.GetComponentInChildren<TextMeshPro>().text = startLVL + "$";
            Player.instance.myTotalMoney = startLVL;

            PlayerPrefs.SetInt("startLVL", startLVL);
            LevelManager.instance.checkCoinsState();
            


        }


    }
    public void upgradeIncome()
    {
        if (LevelManager.instance.myMoneyInGame >= incomeLVL * 100)
        {
            HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
            PlayerPrefs.SetInt("myMoneyInGame", LevelManager.instance.myMoneyInGame - incomeLVL * 100);
            LevelManager.instance.myMoneyInGame = PlayerPrefs.GetInt("myMoneyInGame");
            UpperPanel.instance.myMoneyInGameText.text = LevelManager.instance.myMoneyInGame + "";

            incomeLVL += 1;
            Income.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL " + incomeLVL;
            Income.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + incomeLVL * 100;

            PlayerPrefs.SetInt("incomeLVL", incomeLVL);

        }
    }

}
