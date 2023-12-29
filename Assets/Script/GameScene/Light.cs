using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private float num;

    void Update()
    {
        if (num == 1)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Show();
            }
        }

        if (num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Show();
            }
        }
    }

    void Show()
    {
        GetComponent<Renderer>().enabled = true;
        Invoke("Hide", 0.25f);

    }

    void Hide()
    {
        GetComponent<Renderer>().enabled = false;
    }

}
