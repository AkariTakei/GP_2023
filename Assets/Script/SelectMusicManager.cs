using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMusicManager : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.instance.GetSetSongName = gameObject.name;
        Invoke("ChangeScene", 0.5f);

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
