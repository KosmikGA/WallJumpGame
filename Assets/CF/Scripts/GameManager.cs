using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOver;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void GameFail()
    {

        GameOver.SetActive(true);
        Time.timeScale = 0;
    }
}

