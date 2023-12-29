using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

public class SelectMusicUI : MonoBehaviour
{
    [SerializeField] private GameObject selectInstrument;
    [SerializeField] private GameObject selectMode;
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject PlayNumText;
    [SerializeField] private GameObject hiScoreText;

    SelectMusicManager selectMusicManager;
    Color color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 1f);

    SaveDataManager.SaveData data;

    void Start()
    {
        data = SaveDataManager.Load();
        selectMusicManager = GameObject.Find("SelectMusic").GetComponent<SelectMusicManager>();
        selectInstrument = GameObject.Find("tuke");
        selectMode = GameObject.Find("easy");
        selectInstrument.GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色
        selectInstrument.transform.Find("image").GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色

        selectMode.GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色

        selectMusicManager.GetSetInstrument = selectInstrument.name;
        selectMusicManager.GetSetMode = selectMode.name;
        startButton.GetComponent<Image>().material.SetColor("_Color", color);
    }
    public void OnClick(GameObject obj)
    {
        //ここでデーターを元に選択できる楽器・難易度を調整する

        if (obj.name == "tuke" || obj.name == "ookan" || obj.name == "kane")
        {
            Change(obj, "instrument");
        }

        else if (obj.name == "easy" || obj.name == "normal" || obj.name == "hard")
        {
            Change(obj, "mode");
        }

        if (obj == startButton)
        {
            PutButtonAnimaion();
        }
    }

    public void PointerUp(GameObject obj)
    {
        UpButtonAnimation();
    }

    public void ChangeText()
    {
        if (data == null)
        {
            return;
        }

        for (int i = 0; i < data.songData.Length; i++)
        {
            if (data.songData[i].songName == selectMusicManager.GetSetMusicTitle)
            {
                for (int j = 0; j < data.songData[i].instrumentData.Length; j++)
                {
                    if (data.songData[i].instrumentData[j].instrumentName == selectMusicManager.GetSetInstrument)
                    {
                        for (int k = 0; k < data.songData[i].instrumentData[j].levelData.Length; k++)
                        {
                            if (data.songData[i].instrumentData[j].levelData[k].levelName == selectMusicManager.GetSetMode)
                            {
                                if (data.songData[i].instrumentData[j].levelData[k].levelName == "easy")
                                {
                                    levelText.GetComponent<TextMeshProUGUI>().text = "かんたん";
                                }
                                else if (data.songData[i].instrumentData[j].levelData[k].levelName == "normal")
                                {
                                    levelText.GetComponent<TextMeshProUGUI>().text = "ふつう";
                                }
                                else if (data.songData[i].instrumentData[j].levelData[k].levelName == "hard")
                                {
                                    levelText.GetComponent<TextMeshProUGUI>().text = "むずかしい";
                                }

                                if (data.songData[i].instrumentData[j].levelData[k].playNum == 0)
                                {
                                    PlayNumText.GetComponent<TextMeshProUGUI>().text = "ーーー";
                                    hiScoreText.GetComponent<TextMeshProUGUI>().text = "ーーー";
                                }
                                else
                                {
                                    PlayNumText.GetComponent<TextMeshProUGUI>().text = data.songData[i].instrumentData[j].levelData[k].playNum.ToString();
                                    hiScoreText.GetComponent<TextMeshProUGUI>().text = data.songData[i].instrumentData[j].levelData[k].score.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    private void Change(GameObject selectObj, string button)
    {
        if (selectInstrument != selectObj && button == "instrument")
        {
            //灰色にする
            selectInstrument.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1); //灰色
            selectInstrument.transform.Find("image").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1); //白色
            selectInstrument = selectObj;
            selectInstrument.GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色
            selectInstrument.transform.Find("image").GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色

            selectMusicManager.GetSetInstrument = selectInstrument.name;
        }

        else if (selectMode != selectObj && button == "mode")
        {
            //灰色にする
            selectMode.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1); //灰色
            selectMode = selectObj;
            selectMode.GetComponent<Image>().color = new Color(1, 1, 1, 1); //白色

            selectMusicManager.GetSetMode = selectMode.name;

        }
    }

    private void PutButtonAnimaion()
    {
        Color inputColor = new Color(79 / 255f, 79 / 255f, 79 / 255f, 1f);
        //0.5秒かけてマテリアルのシェーダーのtintを変える
        startButton.GetComponent<Image>().material.DOColor(inputColor, 0.1f).AsyncWaitForCompletion();
        //startButton.GetComponent<Image>().material.SetColor("_Color", inputColor);
        //0.5秒かけてボタンを縮小しながらマテリアルの色を変える
        startButton.transform.DOScale(0.48f, 0.1f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
    }

    private async void UpButtonAnimation()
    {
        Color upColor = new Color(157 / 255f, 157 / 255f, 157 / 255f, 1f);
        startButton.transform.DOScale(0.5f, 0.1f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
        await startButton.GetComponent<Image>().material.DOColor(upColor, 0.05f).AsyncWaitForCompletion();
        await startButton.GetComponent<Image>().material.DOColor(color, 0.4f).AsyncWaitForCompletion();
    }
}
