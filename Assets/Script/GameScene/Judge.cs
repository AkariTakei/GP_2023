using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] judgeObj = new GameObject[3]; //判定オブジェクト
    [SerializeField] NotesManager notesManager;

    int deletedNotesNum = 0; //削除したノーツの数

    int[] judge = new int[3]; //良、可、不可の数

    private int maxCombo;
    private int combo;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            judge[i] = 0;
        }

        maxCombo = 0;
        combo = 0;
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

            if (Time.time - GameManager.instance.GetSetStartTime > notesManager.NotesTime[0] + 0.1f)
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    deleteData();
                    deleteNotesObj();
                    return;
                }
                message(2);
                deleteData();
                judge[2]++;
                deleteNotesObj();

                if (combo > maxCombo)
                {
                    maxCombo = combo;
                }
                combo = 0;
            }
        }

    }

    void Judgement(float timeLag)
    {
        if (timeLag <= 0.05) //誤差が0.05秒以下
        {
            judge[0]++;
            message(0);
            deleteData();
            deleteNotesObj();
        }

        else
        {
            if (timeLag <= 0.1)
            {
                judge[1]++;
                message(1);
                deleteData();
                deleteNotesObj();
            }
        }

        combo++;
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

    public int[] GetJudge
    {
        get { return judge; }
    }

    public int GetCombo
    {
        get
        {
            if (judge[2] == 0)
            {
                maxCombo = combo;
            }
            return maxCombo;
        }
    }

}
