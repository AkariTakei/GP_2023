using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    int[] result = new int[2];

    void Start()
    {
        Debug.Log("良 = " + result[0] + "可 = " + result[1] + "不可 = " + result[2]);
    }

    public int[] SetResult
    {
        set
        {
            result = value;
        }
    }
}
