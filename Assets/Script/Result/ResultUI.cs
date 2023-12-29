using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    private Text comboText;
    private Text greatText;
    private Text goodText;
    private Text badText;

    private void Awake()
    {
        comboText = GameObject.Find("combo").GetComponent<Text>();
        greatText = GameObject.Find("great").GetComponent<Text>();
        goodText = GameObject.Find("good").GetComponent<Text>();
        badText = GameObject.Find("bad").GetComponent<Text>();
    }

    public void SetText(int combo, int great, int good, int bad, int score, float accuracy)
    {
        comboText.text = combo.ToString();
        greatText.text = great.ToString();
        goodText.text = good.ToString();
        badText.text = bad.ToString();

    }
}
