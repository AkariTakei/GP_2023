using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public int noteNum; //総ノーツ数
    private string songName; //曲名

    public List<int> LaneNum = new List<int>(); //どのブロックにノーツが来るか
    public List<int> NoteType = new List<int>(); //ノーツの種類
    public List<float> NotesTime = new List<float>(); //ノーツが判定線と重なる時間
    public List<GameObject> NotesObj = new List<GameObject>(); //ノーツのオブジェクト

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject[] noteObj = new GameObject[2];

    void OnEnable()
    {
        noteNum = 0;
        songName = "yatai";
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString); //デシリアル化

        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB); //1拍の間隔
            float beatSec = kankaku * (float)inputJson.notes[i].LPB; //1拍の秒数
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f; //ノーツが判定線と重なる時間
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float x = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj[inputJson.notes[i].block], new Vector2(x, -inputJson.notes[i].block * 2f + 1f), Quaternion.identity));

        }
    }


}
