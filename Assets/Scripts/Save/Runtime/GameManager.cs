using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Fields
    static int soundLevel;
    static int vibration;
    static int level;
    static int coin;
    static int firstTimeLoad;
    static int fieldCount;
    #endregion
    public static int FieldCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("fieldCount"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("fieldCount");
        }
        set
        {
            fieldCount = value;
            PlayerPrefs.SetInt("fieldCount", fieldCount);
        }
    }

    public static int FirstTimeLoad
    {
        get
        {
            if (!PlayerPrefs.HasKey("firstTimeLoad"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("firstTimeLoad");
        }
        set
        {
            firstTimeLoad = value;
            PlayerPrefs.SetInt("firstTimeLoad", firstTimeLoad);
        }
    }
    public static int Level
    {
        get
        {
            if (!PlayerPrefs.HasKey("level"))
            {
                return 1;
            }
            return PlayerPrefs.GetInt("level");
        }
        set
        {
            level = value;
            PlayerPrefs.SetInt("level", level);
        }
    }
    public static int Vibration
    {
        get
        {
            if (!PlayerPrefs.HasKey("vibration"))
            {
                return 1;
            }
            return PlayerPrefs.GetInt("vibration");
        }
        set
        {
            vibration = value;
            PlayerPrefs.SetInt("vibration", vibration);
        }
    }
    public static int Sound
    {
        get
        {
            return PlayerPrefs.GetInt("soundLevel");
        }
        set
        {
            soundLevel = value;
            PlayerPrefs.SetInt("soundLevel", soundLevel);
        }
    }
    public static int Coin
    {
        get
        {
            return PlayerPrefs.GetInt("coin");
        }
        set
        {
            coin = value;
            PlayerPrefs.SetInt("coin", coin);
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        if (!PlayerPrefs.HasKey("vibration"))
        {
            PlayerPrefs.SetInt("vibration", 1);
        }
        if (!PlayerPrefs.HasKey("soundLevel"))
        {
            PlayerPrefs.SetInt("soundLevel", 1);
        }
        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
        }
        if (!PlayerPrefs.HasKey("fieldCount"))
        {
            PlayerPrefs.SetInt("fieldCount", 0);
        }
        DataController.Load();
    }
}
