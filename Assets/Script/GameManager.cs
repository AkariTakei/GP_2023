using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float noteSpeed;
    bool Start;
    float startTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float GetNoteSpeed
    {
        get { return noteSpeed; }
    }

    public bool GetSetStart
    {
        get { return Start; }
        set { Start = value; }
    }

    public float GetSetStartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }
}
