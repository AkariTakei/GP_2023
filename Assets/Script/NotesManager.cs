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

    private float NotesSpeed;
    [SerializeField] GameObject[] noteObj = new GameObject[2];

    [SerializeField] GameObject rightJudge;
    [SerializeField] GameObject leftJudge;
    Vector2 localRightPos;
    Vector2 localLeftPos;

    void Start()
    {
        if (GameManager.instance != null)
        {
            NotesSpeed = GameManager.instance.GetNoteSpeed;
            noteNum = 0;
            songName = GameManager.instance.GetSetSongName;
            localRightPos = rightJudge.transform.position;
            localLeftPos = leftJudge.transform.position;
            Load(songName);
        }
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
            if (inputJson.notes[i].block == 0)
            {
                NotesObj.Add(Instantiate(noteObj[inputJson.notes[i].block], new Vector2(x + localRightPos.x, localRightPos.y), Quaternion.identity));
                NotesObj[i].transform.parent = rightJudge.transform;
                NotesObj[i].transform.localScale = new Vector2(100, 100);
            }
            else
            {
                NotesObj.Add(Instantiate(noteObj[inputJson.notes[i].block], new Vector2(x + localLeftPos.x, localLeftPos.y), Quaternion.identity));
                NotesObj[i].transform.parent = leftJudge.transform;
                NotesObj[i].transform.localScale = new Vector2(100, 100);
            }
        }
        Debug.Log(songName);
    }


}
