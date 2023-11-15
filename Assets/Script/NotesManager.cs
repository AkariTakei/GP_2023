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
    [SerializeField] RectTransform[] lane = new RectTransform[2];

    Vector2 rightJudgePos;
    Vector2 leftJudgePos;

    void Start()
    {
        if (GameManager.instance != null)
        {
            NotesSpeed = GameManager.instance.GetNoteSpeed;
            noteNum = 0;
            songName = "yatai";

            Vector2 localRightPos = lane[0].anchoredPosition;
            Vector2 localLeftPos = lane[1].anchoredPosition;
            Vector2 worldRightPos = lane[0].transform.TransformPoint(localRightPos);
            Vector2 worldLeftPos = lane[1].transform.TransformPoint(localLeftPos);

            rightJudgePos = worldRightPos * 0.5f;
            leftJudgePos = worldLeftPos * 0.5f;

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
                NotesObj.Add(Instantiate(noteObj[inputJson.notes[i].block], new Vector2(x + rightJudgePos.x, rightJudgePos.y), Quaternion.identity));
            }
            else
            {
                NotesObj.Add(Instantiate(noteObj[inputJson.notes[i].block], new Vector2(x + leftJudgePos.x, leftJudgePos.y), Quaternion.identity));
            }
        }
        Debug.Log("生成完了");
    }


}
