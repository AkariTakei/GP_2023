using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    AudioClip Music;
    string songName;
    bool isPlay;

    void Start()
    {
        GameManager.instance.GetSetStart = false;
        songName = "yatai";
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        isPlay = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlay)
        {
            GameManager.instance.GetSetStart = true;
            GameManager.instance.GetSetStartTime = Time.time;
            isPlay = true;
            audio.PlayOneShot(Music);
        }
    }
}
