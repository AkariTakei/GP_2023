using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject greatObj;
    [SerializeField] private GameObject goodObj;
    [SerializeField] private GameObject badObj;
    [SerializeField] private GameObject comboObj;
    [SerializeField] private GameObject scoreObj;
    [SerializeField] private GameObject accuracyObj;
    [SerializeField] private GameObject comentText;
    [SerializeField] private Sprite[] levelSprite;
    [SerializeField] private Sprite[] MusicTitleSprite;

    TextMeshProUGUI scoreText;
    TextMeshProUGUI greatText;
    TextMeshProUGUI goodText;
    TextMeshProUGUI badText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI accuracyText;
    Count scoreCount;
    Count greatCount;
    Count goodCount;
    Count badCount;
    Count comboCount;

    private GameObject hiScore;
    private GameObject resultImage;
    private GameObject restartButton;
    private GameObject nextButton;

    int score;
    int combo;
    int great;
    int good;
    int bad;
    float accuracy;

    bool scoreAnimation = false;
    bool greatAnimation = false;
    bool goodAnimation = false;
    bool badAnimation = false;
    bool comboAnimation = false;



    [SerializeField] UnityEvent reStartEvent;
    [SerializeField] UnityEvent nextEvent;
    Color color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 1f);


    private void Awake()
    {
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        scoreCount = scoreObj.GetComponent<Count>();
        greatText = greatObj.GetComponent<TextMeshProUGUI>();
        greatCount = greatObj.GetComponent<Count>();
        goodText = goodObj.GetComponent<TextMeshProUGUI>();
        goodCount = goodObj.GetComponent<Count>();
        badText = badObj.GetComponent<TextMeshProUGUI>();
        badCount = badObj.GetComponent<Count>();
        comboText = comboObj.GetComponent<TextMeshProUGUI>();
        comboCount = comboObj.GetComponent<Count>();
        accuracyText = accuracyObj.GetComponent<TextMeshProUGUI>();


        ResetResult();
        hiScore = GameObject.Find("hiscore");
        resultImage = GameObject.Find("resultImage");
        restartButton = GameObject.Find("reStart");
        nextButton = GameObject.Find("next");

    }

    private void Start()
    {
        GameObject level = GameObject.Find("level");
        GameObject musicTitle = GameObject.Find("musicTitle");
        if (GameManager.instance.GetSetMode == "easy")
        {
            level.GetComponent<Image>().sprite = levelSprite[0];
        }
        else if (GameManager.instance.GetSetMode == "normal")
        {
            level.GetComponent<Image>().sprite = levelSprite[1];
        }
        else if (GameManager.instance.GetSetMode == "hard")
        {
            level.GetComponent<Image>().sprite = levelSprite[2];
        }

        level.GetComponent<Image>().SetNativeSize();

        if (GameManager.instance.GetSetSongName == "ninnba")
        {
            musicTitle.GetComponent<Image>().sprite = MusicTitleSprite[0];
        }
        else if (GameManager.instance.GetSetSongName == "yatai")
        {
            musicTitle.GetComponent<Image>().sprite = MusicTitleSprite[1];
        }
        else if (GameManager.instance.GetSetSongName == "nakanokiri")
        {
            musicTitle.GetComponent<Image>().sprite = MusicTitleSprite[2];
        }
        else if (GameManager.instance.GetSetSongName == "ti-hyaitoro")
        {
            musicTitle.GetComponent<Image>().sprite = MusicTitleSprite[3];
        }

        musicTitle.GetComponent<Image>().SetNativeSize();

        hiScore.SetActive(false);

        score = 9000;
        great = 90;
        good = 50;
        bad = 10;
        combo = 100;
        // greatCount.CountToInt(0, great, 0.8f);
        // goodCount.CountToInt(0, good, 0.8f);
        // badCount.CountToInt(0, bad, 0.8f);
        // comboCount.CountToInt(0, combo, 0.8f);
        // accuracyCount.CountToInt(0, accuracy, 1.5f);
        //CountTrigger(scoreCount, score, 1.5f, 0.5f);
    }

    private void Update()
    {
        if (scoreAnimation == false)
        {
            //scoreCount.CountToInt(0, 9999, 1.5f);
            CountTrigger(scoreCount, score, 1.5f, 1);
            scoreAnimation = true;
        }
        CountAnimation(scoreCount, scoreText);

        if (scoreCount.IsFinish == true)
        {
            if (greatAnimation == false)
            {
                CountTrigger(greatCount, great, 0.8f, 0.8f);
                greatAnimation = true;
            }
            CountAnimation(greatCount, greatText);
        }

        if (greatCount.IsFinish == true)
        {
            if (goodAnimation == false)
            {
                CountTrigger(goodCount, good, 0.8f, 0.3f);
                goodAnimation = true;
            }
            CountAnimation(goodCount, goodText);
        }

        if (goodCount.IsFinish == true)
        {
            if (badAnimation == false)
            {
                CountTrigger(badCount, bad, 0.8f, 0.3f);
                badAnimation = true;
            }
            CountAnimation(badCount, badText);
        }

        if (badCount.IsFinish == true)
        {
            if (comboAnimation == false)
            {
                CountTrigger(comboCount, combo, 0.8f, 0.3f);
                comboAnimation = true;
            }
            CountAnimation(comboCount, comboText);
        }

    }
    public void SetText(int combo, int great, int good, int bad, int score, float accuracy)
    {
        // comboText.GetComponent<TextMeshProUGUI>().text = combo.ToString();
        // greatText.GetComponent<TextMeshProUGUI>().text = great.ToString();
        // goodText.GetComponent<TextMeshProUGUI>().text = good.ToString();
        // badText.GetComponent<TextMeshProUGUI>().text = bad.ToString();
        // scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        // accuracyText.GetComponent<TextMeshProUGUI>().text = accuracy.ToString() + "%";

        this.score = score;
        this.combo = combo;
        this.great = great;
        this.good = good;
        this.bad = bad;
        this.accuracy = accuracy;


    }

    public void PointerDownButton(GameObject obj)
    {
        Color inputColor = new Color(79 / 255f, 79 / 255f, 79 / 255f, 1f);
        obj.GetComponent<Image>().material.DOColor(inputColor, 0.1f).AsyncWaitForCompletion();
    }

    public async void PointerUpButton(GameObject obj)
    {
        Color upColor = new Color(135 / 255f, 135 / 255f, 135 / 255f, 1f);
        await obj.GetComponent<Image>().material.DOColor(upColor, 0.05f).AsyncWaitForCompletion();
        await obj.GetComponent<Image>().material.DOColor(color, 0.4f).AsyncWaitForCompletion();

        if (obj.name == "reStart")
        {
            reStartEvent.Invoke();
        }
        else if (obj.name == "next")
        {
            nextEvent.Invoke();
        }
    }

    private void ResetResult()
    {
        greatText.GetComponent<TextMeshProUGUI>().text = "0";
        goodText.GetComponent<TextMeshProUGUI>().text = "0";
        badText.GetComponent<TextMeshProUGUI>().text = "0";
        comboText.GetComponent<TextMeshProUGUI>().text = "0";
        scoreText.text = "0";
        accuracyText.GetComponent<TextMeshProUGUI>().text = "0%";

        score = 0;
        combo = 0;
        great = 0;
        good = 0;
        bad = 0;
        accuracy = 0;
    }

    private void CountAnimation(Count count, TextMeshProUGUI text)
    {

        if (count.IsWorking())
        {
            Debug.Log("アニメーション中");
            int value = (int)Mathf.Ceil(count.Value);
            text.text = value.ToString();
        }
    }

    private async void CountTrigger(Count count, int num, float time, float waitTime)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        count.CountToInt(0, num, time);
    }

    private async void Animation(GameObject obj)
    {
        await obj.transform.DOScaleY(2f, 0.5f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
        await obj.transform.DOScaleY(0.5f, 0.5f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
    }
}
