using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button[] buttons;
    
    private int LevelNumber;
    
    public Toggle[] toggles;
    
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

    private void PlayGame3x3()
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        SceneManager.LoadScene("3x3");
    }
    
    private void PlayGame6x6()
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        SceneManager.LoadScene("6x6");
    }
    
    private void PlayGame13x13()
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        SceneManager.LoadScene("13x13");
    }
    
    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }
    
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ToggleGroup()
    {
        if (toggles[0].isOn)
        {
            PlayerPrefs.SetInt("LevelDiffuse", 0);
            PlayGame3x3();
        }
        else if (toggles[1].isOn)
        {
            PlayerPrefs.SetInt("LevelDiffuse", 1);
            PlayGame6x6();
        }
        else if (toggles[2].isOn)
        {
            PlayerPrefs.SetInt("LevelDiffuse", 2);
            PlayGame13x13();
        }
    }


}
