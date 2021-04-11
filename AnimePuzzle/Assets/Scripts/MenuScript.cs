using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MenuScript : MonoBehaviour
{
    public Button[] buttons;
    
#if UNITY_IOS
    private string gameId = "4085374";
#elif UNITY_ANDROID
    private string gameId = "4085375";
#endif
    
    private void Start()
    {
        PlayerPrefs.SetInt("LevelOpen", PlayerPrefs.GetInt("LevelOpen", 1));
        
        Advertisement.Initialize(gameId, true);
        StartCoroutine(ShowBannerWhenInitialized());
        
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
    public void Rate_Us()
    {
    
    }

    IEnumerator ShowBannerWhenInitialized () {
        while (!Advertisement.isInitialized) {
            yield return new WaitForSeconds(0.5f);
        }
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show ("Banner_Android");
        

    }
    
    
}
