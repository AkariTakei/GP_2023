using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    float NoteSpeed;
    bool isPlay = false;

    void Start()
    {
        NoteSpeed = GameManager.instance.GetNoteSpeed;
    }
    void Update()
    {
        // if (GameManager.instance.GetSetStart == true)
        // {
        //     start = true;

        // }
        if (GameManager.instance.GetSetStart == true)
        {
            if (isPlay == false)
            {
                isPlay = true;
                Debug.Log("ノーツが動き始める時間 = " + Time.time);
            }
            //左に移動
            // transform.position -= transform.right * Time.deltaTime * NoteSpeed;
            //下に移動
            transform.position -= transform.up * Time.deltaTime * NoteSpeed;
            //上に移動
            //transform.position += transform.up * Time.deltaTime * NoteSpeed;
        }
    }
}
