using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UpperPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public static UpperPanel instance;
    public int combo;
    public TextMeshProUGUI comboText;

    bool timerStarted;
    float timer, waitSecond;
    Image img;
    private float targetAlpha = 0;
    private float fadeDuration = 1;
    private float lerpParam;
    private float startAlpha = 0;

    public TextMeshProUGUI myMoneyInGameText;
    void Start()
    {
        instance = this;
        img = comboText.GetComponentInParent<Image>();
        SetMaterialAlpha(1);
        myMoneyInGameText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        myMoneyInGameText.text = LevelManager.instance.myMoneyInGame +"";
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text="Level "+ (PlayerPrefs.GetInt("Level")+1);

    }

    void Update()
    {
        lerpParam += Time.deltaTime;

        float alpha = Mathf.Lerp(startAlpha, targetAlpha, lerpParam / fadeDuration);
        SetMaterialAlpha(alpha);
        if (alpha == 0)
        {
            Player.instance.particles[4].Stop();
        }
        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer >= waitSecond)
            {
                timer = 0;
                timerStarted = false;
                FadeTo(0, 1);
            }
        }
        
    }
    public void makeCombo()
    {
        float punchScale = Mathf.Clamp(.1f * combo / 4, 0.1f, 1.6f);
        comboText.transform.parent.transform.DOPunchScale(new Vector3(punchScale,punchScale,punchScale), .5f);
        timer = 0;
        FadeTo(1, .2f);
        combo += 1;
        comboText.text = "x" + combo;
        timerStarted = true;
        waitSecond = 3;
        Player.instance.particles[4].Play();

    }
   
    public void endCombo()
    {
        timer = 0;
        FadeTo(0, .2f);
        combo = 0;
        comboText.text = "";
        timerStarted = false;

    }
    public void FadeTo(float alpha, float duration = 1)
    {
        startAlpha = img.color.a;
        targetAlpha = alpha;
        fadeDuration = duration;
        lerpParam = 0;
    }

    private void SetMaterialAlpha(float alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
        comboText.color = color;
        comboText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
    }
}
