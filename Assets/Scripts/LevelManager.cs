using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int Level;
    public int myMoneyInGame;
    // Start is called before the first frame update
    public Coin[] Coins;
    private void Awake()
    {

        Level = PlayerPrefs.GetInt("Level");
        Level = Level % transform.childCount;
        transform.GetChild(Level).gameObject.SetActive(true);

        myMoneyInGame = PlayerPrefs.GetInt("myMoneyInGame");
    }
    void Start()
    {
        instance = this;
        Coins = (Coin[])GameObject.FindObjectsOfType(typeof(Coin));

        checkCoinsState();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(InputPanel.valX) > 0.01f && Player.instance.playerState == PlayerStates.idle)
        {
            Play();
            IncrementalPanel.instance.transform.DOScale(0, .5f);
        }

    }
    public void Play()
    {
        Player.instance.playerState = PlayerStates.moving;
        Player.instance.transform.DOMoveY(1.4f, .5f);
        Player.instance.GetComponent<Collider>().material = null;
        Player.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        DOTween.Clear(true);
        SceneManager.LoadScene(0);
    }
    public void restartLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level"));
        DOTween.Clear(true);
        SceneManager.LoadScene(0);
    }
    public void checkCoinsState()
    {
        for (int i = 0; i < Coins.Length; i++)
        {
            
            if (Coins[i].GetComponent<Coin>().operationValue > Player.instance.myTotalMoney)
            {
                Coins[i].transform.GetComponentInChildren<TextMeshPro>().color = Color.red;
            }
            else
            {
                Coins[i].transform.GetComponentInChildren<TextMeshPro>().color = Color.green;
            }
        }
    }
    
}
