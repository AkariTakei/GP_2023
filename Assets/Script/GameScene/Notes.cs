using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    float NoteSpeed;
    bool start;

    void Start()
    {
        NoteSpeed = GameManager.instance.GetNoteSpeed;
        start = true;
    }
    void Update()
    {
        // if (GameManager.instance.GetSetStart == true)
        // {
        //     start = true;

        // }
        if (start)
        {
            //左に移動
            // transform.position -= transform.right * Time.deltaTime * NoteSpeed;
            //下に移動
            transform.position -= transform.up * Time.deltaTime * NoteSpeed;
            //上に移動
            //transform.position += transform.up * Time.deltaTime * NoteSpeed;
        }
    }
}
