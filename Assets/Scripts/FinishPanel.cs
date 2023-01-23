using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class FinishPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public static FinishPanel instance;
    public TextMeshProUGUI rewardText;
    bool isLevelEnd;
    public float rewardValue;
    float value;
    int myMoney;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelEnd)
        {

            value = Mathf.Lerp(value, rewardValue+1, 5*Time.deltaTime);
            if ((int) value  < rewardValue)
            {
                HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);

            }
            rewardText.text = (int)value + "";
            UpperPanel.instance.myMoneyInGameText.text = ""+(myMoney + (int)value-1) ;
        }
    }
    public void getLevelEndMoney(float reward)
    {
        float incomeLVL = PlayerPrefs.GetInt("incomeLVL");
        transform.DOScale(1, .2f);
        rewardValue = reward * (1 + (incomeLVL / 10));
        myMoney = PlayerPrefs.GetInt("myMoneyInGame");
        PlayerPrefs.SetInt("myMoneyInGame", (int)(LevelManager.instance.myMoneyInGame + rewardValue));
        LevelManager.instance.myMoneyInGame = PlayerPrefs.GetInt("myMoneyInGame");
        isLevelEnd = true;

    }



}
