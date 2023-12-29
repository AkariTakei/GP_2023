using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private GameObject comboText;
    private GameObject comboObj;
    private void Awake()
    {
        comboText = GameObject.Find("ComboNum");
        comboObj = GameObject.Find("Combo");
        comboText.SetActive(false);
        comboObj.SetActive(false);
    }

    public void SetText(int combo)
    {
        if (combo < 10)
        {
            comboText.SetActive(false);
            comboObj.SetActive(false);
        }
        else
        {
            comboText.SetActive(true);
            comboText.GetComponent<TextMeshProUGUI>().text = combo.ToString();
            comboObj.SetActive(true);
        }
    }
}
