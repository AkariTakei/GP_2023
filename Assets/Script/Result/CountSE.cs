using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountSE : MonoBehaviour
{
    AudioSource countSE;

    private void Start()
    {
        countSE = GetComponent<AudioSource>();
    }

    public void PlayCountSE(int start, int goal, float time)
    {
        countSE.Play();
    }

    public void StopCountSE()
    {
        countSE.Stop();
    }
}
