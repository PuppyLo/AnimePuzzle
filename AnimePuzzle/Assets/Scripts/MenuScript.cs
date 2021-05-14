using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button[] buttons;
    
    private void Start()
    {
        PlayerPrefs.SetInt("LevelOpen", PlayerPrefs.GetInt("LevelOpen", 1));
        
        Application.targetFrameRate = 120;

        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < PlayerPrefs.GetInt("LevelOpen", 1); i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void PlayGame(int LevelNumber)
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        SceneManager.LoadScene("Game");
    }
    
    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }


}
