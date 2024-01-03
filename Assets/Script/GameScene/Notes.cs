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
        if (GameManager.instance.GetSetStart == true)
        {
            if (isPlay == false)
            {
                isPlay = true;
                Debug.Log("ノーツが動き始める時間 = " + Time.time);
            }
            transform.position -= transform.up * Time.deltaTime * NoteSpeed;
        }
    }
}
