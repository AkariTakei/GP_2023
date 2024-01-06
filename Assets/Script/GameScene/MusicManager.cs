using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;


public class MusicManager : MonoBehaviour
{
    AudioSource songAudio;
    AudioClip Music;
    string songName;

    Judge judge;

    async void Start()
    {
        songName = GameManager.instance.GetSetSongName;
        songAudio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        judge = GameObject.Find("Judge").GetComponent<Judge>();

        await UniTask.Delay(500);

        if (!GameManager.instance.GetSetStart)
        {
            GameManager.instance.GetSetStart = true;
            GameManager.instance.GetSetStartTime = Time.time;
            Debug.Log(Time.time);
            Debug.Log("音楽が再生を始める時間 = " + GameManager.instance.GetSetStartTime);
            songAudio.PlayOneShot(Music);
            Debug.Log("音楽が再生されました。");

        }


    }

    void Update()
    {
        if (GameManager.instance.GetSetStart && !songAudio.isPlaying && !GameManager.instance.GetSetPause)
        {
            Debug.Log("音楽が終了しました。");
            GameManager.instance.GetSetStart = false;
            SceneManager.sceneLoaded += KeepScore;
            SceneManager.LoadScene("ResultScene");
        }
    }

    void KeepScore(Scene next, LoadSceneMode mode)
    {
        var resultManager = GameObject.Find("ResultManager").GetComponent<ResultManager>();
        resultManager.SetResult(judge.GetCombo, judge.GetJudge[0], judge.GetJudge[1], judge.GetJudge[2]);
        SceneManager.sceneLoaded -= KeepScore;
    }

    public void Pause()
    {
        songAudio.Pause();
    }

    public void UnPause()
    {
        songAudio.UnPause();
    }


}
