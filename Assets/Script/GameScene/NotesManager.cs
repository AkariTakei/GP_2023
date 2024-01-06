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

    private int preLaneNum; //1つ前のブロックの番号
    public List<int> NoteType = new List<int>(); //ノーツの種類
    public List<float> NotesTime = new List<float>(); //ノーツが判定線と重なる時間

    public List<GameObject> NotesObj = new List<GameObject>(); //ノーツのオブジェクト

    private float NotesSpeed;
    [SerializeField] GameObject[] noteObj = new GameObject[2];

    [SerializeField] GameObject rightJudge;
    [SerializeField] GameObject leftJudge;
    [SerializeField] GameObject[] yataiNotes;
    [SerializeField] GameObject[] nakanokiriNotes;
    [SerializeField] GameObject[] tihyaitoroNotes;
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
            if (songName == "yatai")
            {
                Load(songName, yataiNotes);
            }
            else if (songName == "nakanokiri")
            {
                Load(songName, nakanokiriNotes);
            }

            else if (songName == "ti-hyaitoro")
            {
                Load(songName, tihyaitoroNotes);
            }


        }
    }


    private void Load(string SongName, GameObject[] songNotes)
    {
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString); //デシリアル化

        noteNum = inputJson.notes.Length;


        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            Debug.Log(inputJson.offset);
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB); //1拍の間隔
            float beatSec = kankaku * (float)inputJson.notes[i].LPB; //1拍の秒数
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.00001f; //ノーツが判定線と重なる時間
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);
            float y = NotesTime[i] * NotesSpeed;
            if (i + 1 < inputJson.notes.Length && inputJson.notes[i + 1].block == 2)
            {
                preLaneNum = inputJson.notes[i].block;
            }
            if (inputJson.notes[i].block == 0)
            {
                NotesObj.Add(Instantiate(songNotes[i], new Vector2(localRightPos.x, y + localRightPos.y), Quaternion.identity));
                NotesObj[i].transform.SetParent(rightJudge.transform, true);
                NotesObj[i].transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else if (inputJson.notes[i].block == 1)
            {
                NotesObj.Add(Instantiate(songNotes[i], new Vector2(localLeftPos.x, y + localLeftPos.y), Quaternion.identity));
                NotesObj[i].transform.SetParent(leftJudge.transform, true);
                NotesObj[i].transform.localScale = new Vector2(0.5f, 0.5f);
            }

            else
            {
                if (preLaneNum == 0)
                {
                    NotesObj.Add(Instantiate(songNotes[i], new Vector2(localRightPos.x, y + localRightPos.y), Quaternion.identity));
                }
                else
                {
                    NotesObj.Add(Instantiate(songNotes[i], new Vector2(localLeftPos.x, y + localLeftPos.y), Quaternion.identity));
                }
                // NotesObj.Add(Instantiate(songNotes[i], new Vector2((localRightPos.x + localLeftPos.x) / 2, y + localRightPos.y), Quaternion.identity));
                NotesObj[i].transform.SetParent(leftJudge.transform, true);
                NotesObj[i].transform.localScale = new Vector2(0.6f, 0.6f);
                //非表示にする
                //NotesObj[i].SetActive(false);
            }


        }
        Debug.Log(songName);
    }


}
