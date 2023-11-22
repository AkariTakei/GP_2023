using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] judgeObj = new GameObject[3]; //判定オブジェクト
    [SerializeField] NotesManager notesManager;

    int deletedNotesNum = 0; //削除したノーツの数

    int greatNum = 0;
    int goodNum = 0;
    int missNum = 0;

    void Start()
    {
        greatNum = 0;
        goodNum = 0;
        missNum = 0;
    }

    void Update()
    {
        if (GameManager.instance.GetSetStart && notesManager.noteNum > deletedNotesNum)
        {

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 0)
                {
                    Judgement(GetABS((Time.time - GameManager.instance.GetSetStartTime) - notesManager.NotesTime[0])); //どれぐらいずれているか
                    return;
                }

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS((Time.time - GameManager.instance.GetSetStartTime) - notesManager.NotesTime[0]));
                    return;

                }
            }

            if (Time.time - GameManager.instance.GetSetStartTime > notesManager.NotesTime[0] + 0.2f)
            {
                message(2);
                deleteData();
                missNum++;
                deleteNotesObj();
            }
        }

    }

    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10) //誤差が0.1秒以下
        {
            greatNum++;
            message(0);
            deleteData();
            deleteNotesObj();
        }

        else
        {
            if (timeLag <= 0.20)
            {
                goodNum++;
                message(1);
                deleteData();
                deleteNotesObj();
            }
        }
    }

    float GetABS(float num) //引数の絶対値を返す
    {
        if (num >= 0)
        {
            return num;
        }

        else
        {
            return -num;
        }
    }

    void deleteData()
    {
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);
    }

    void deleteNotesObj()
    {
        Destroy(notesManager.NotesObj[0]);
        notesManager.NotesObj.RemoveAt(0);
        deletedNotesNum++;
    }

    void message(int judge)
    {
        Instantiate(judgeObj[judge], new Vector2(0, -notesManager.LaneNum[0] * 2f + 2f), Quaternion.identity);
    }

}
