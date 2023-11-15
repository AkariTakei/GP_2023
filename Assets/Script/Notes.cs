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
        start = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;

        }
        if (start)
        {
            //左に移動
            transform.position -= transform.right * Time.deltaTime * NoteSpeed;
        }
    }
}
