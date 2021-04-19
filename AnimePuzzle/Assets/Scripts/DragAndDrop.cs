using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class DragAndDrop : MonoBehaviour
{   
    public int PlacedPieces = 0;
    public GameObject SelectedPiece;
    public GameObject EndMenu;
    public Sprite[] Levels;
    int OIL = 1;

    public Animator animator;
    private bool isPaused;
    
    void Start()
    {
        for (int i = 0;i < 36; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt("Level")];
        }
        
        PlayerPrefs.GetInt("LevelOpen", 1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (!EventSystem.current.IsPointerOverGameObject())
            
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<piceseScript>().Selected = true;
                        SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                        OIL++;
                    }
                } 
                
        }


        if (Input.GetMouseButtonUp(0))
        {
           // if (!EventSystem.current.IsPointerOverGameObject())
            
                if (SelectedPiece != null)
                {
                    SelectedPiece.GetComponent<piceseScript>().Selected = false;
                    SelectedPiece = null;
                }
            
        }

        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        }             
        if (PlacedPieces == 36)
        {
            EndMenu.SetActive(true);
        }
    }
    public void NextLevel()
    {
        if(PlayerPrefs.GetInt("LevelOpen", 1)==PlayerPrefs.GetInt("Level")+1)
        {
            PlayerPrefs.SetInt("LevelOpen", PlayerPrefs.GetInt("LevelOpen", 1) + 1);
        }
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        PlayerPrefs.GetInt("LevelOpen", 1);
        SceneManager.LoadScene("Game");
    }

    public void BacktoMenu()
    {
        if(PlayerPrefs.GetInt("LevelOpen", 1)==PlayerPrefs.GetInt("Level")+1)
        {
            PlayerPrefs.SetInt("LevelOpen", PlayerPrefs.GetInt("LevelOpen", 1) + 1);
        }
        SceneManager.LoadScene("Menu");
    }

    public void MainMenu()
    {  
        SceneManager.LoadScene("Menu");
    }

    public void MenuButton()
    {
        isPaused =! isPaused;
        if (isPaused)
        {
            animator.SetBool("Menu_Show", true);
        }
        else {animator.SetBool("Menu_Show", false);}
    }
}