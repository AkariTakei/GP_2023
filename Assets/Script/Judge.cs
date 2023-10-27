using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] judgeObj = new GameObject[3]; //判定オブジェクト
    [SerializeField] NotesManager notesManager;

    void Update()
    {
        if (GameManager.instance.GetSetStart)
        {

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 0)
                {
                    Judgement(GetABS((Time.time - GameManager.instance.GetSetStartTime) - notesManager.NotesTime[0])); //どれぐらいずれているか
                }

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS((Time.time - GameManager.instance.GetSetStartTime) - notesManager.NotesTime[0]));

                }
            }

            if (Time.time - GameManager.instance.GetSetStartTime > notesManager.NotesTime[0] + 0.2f)
            {
                message(2);
                deleteData();
                Debug.Log("Miss");
                notesManager.NotesObj.RemoveAt(0);
                Debug.Log(GetABS((Time.time - GameManager.instance.GetSetStartTime) - notesManager.NotesTime[0]));
            }
        }
    }

    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10) //誤差が0.1秒以下
        {
            Debug.Log("Great");
            message(0);
            deleteData();
            deleteNotesObj();
        }

        else
        {
            if (timeLag <= 0.20)
            {
                Debug.Log("good");
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
    }

    void message(int judge)
    {
        Instantiate(judgeObj[judge], new Vector2(0, -notesManager.LaneNum[0] * 2f + 2f), Quaternion.identity);
    }

}
