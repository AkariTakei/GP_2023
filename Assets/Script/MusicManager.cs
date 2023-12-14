using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    AudioClip Music;
    string songName;
    bool isPlay;

    Judge judge;

    void Start()
    {
        GameManager.instance.GetSetStart = false;
        songName = GameManager.instance.GetSetSongName;
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        isPlay = false;
        judge = GameObject.Find("Judge").GetComponent<Judge>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlay)
        {
            GameManager.instance.GetSetStart = true;
            GameManager.instance.GetSetStartTime = Time.time;
            isPlay = true;
            audio.PlayOneShot(Music);
            Debug.Log("音楽が再生されました。");
        }

        if (isPlay && !audio.isPlaying)
        {
            Debug.Log("音楽が終了しました。");
            SceneManager.sceneLoaded += KeepScore;
            SceneManager.LoadScene("ResultScene");
        }
    }

    void KeepScore(Scene next, LoadSceneMode mode)
    {
        var result = GameObject.Find("Score").GetComponent<Result>();
        result.SetResult = judge.GetResult();
        SceneManager.sceneLoaded -= KeepScore;
    }
}
