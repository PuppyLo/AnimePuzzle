using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class DragAndDrop : MonoBehaviour
{   
    public int PlacedPieces = 0;
    public GameObject SelectedPiece;
    public GameObject EndMenu;
    public Sprite[] Levels;
    int OIL = 1;

    public Animator animator;
    private bool isPaused;
    
    public int puzzleCount;
     
     
#if UNITY_IOS
    private string gameId = "4085374";
    string mySurfacingId = "Rewarded_iOS";
#elif UNITY_ANDROID
    private string gameId = "4085375";
    string mySurfacingId = "Rewarded_Android";
#endif
    bool testMode = true;
    
    
    
    void Start()
    {
        for (int i = 0; i < puzzleCount; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt("Level")];
            
            GameObject.Find("PuzzleKey").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt("Level")];
        }
        
        PlayerPrefs.GetInt("LevelOpen", 1);
        
        Application.targetFrameRate = 120;
        
        Advertisement.Initialize (gameId, testMode);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (SelectedPiece != null)
                {
                    SelectedPiece.GetComponent<piceseScript>().Selected = false;
                    SelectedPiece = null;
                }
            }
        }

        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        }   
        
        if (PlacedPieces == puzzleCount)
        {
            EndMenu.SetActive(true);
        }
    }
    public void NextLevel()
    {
        ShowRewardedVideo();
        
        if(PlayerPrefs.GetInt("LevelOpen", 1)==PlayerPrefs.GetInt("Level")+1)
        {
            PlayerPrefs.SetInt("LevelOpen", PlayerPrefs.GetInt("LevelOpen", 1) + 1);
        }
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        PlayerPrefs.GetInt("LevelOpen", 1);
        SceneManager.LoadScene("Normal");
    }

    public void BacktoMenu()
    {
        ShowRewardedVideo();
            
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
    
    public void ShowRewardedVideo() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId)) {
            Advertisement.Show(mySurfacingId);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
}