using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float noteSpeed;
    [SerializeField] bool Start;
    float startTime;
    [SerializeField] string songName;
    string instrument;
    string mode;

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

    public string GetSetSongName
    {
        get { return songName; }
        set { songName = value; }
    }

    public string GetSetInstrument
    {
        get { return instrument; }
        set { instrument = value; }
    }

    public string GetSetMode
    {
        get { return mode; }
        set { mode = value; }
    }
}
