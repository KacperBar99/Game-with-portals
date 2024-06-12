using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField,Tooltip("Quick test option")]
    private bool maxFPS;
    [SerializeField]
    private GameObject pressIcon;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private bool isMenu = false;
    private bool paused = false;
    private GameObject player;
    private bool pressIconBefore;



    // Start is called before the first frame update
    void Start()
    {
        if (maxFPS)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }
        this.player = GameObject.FindGameObjectWithTag("Player");
        if (!this.isMenu)
        {
            this.pauseMenu.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (paused)
            {
                this.resume();
            }
            else
            {
                Time.timeScale = 0;
                this.paused = true;
                this.pressIconBefore=this.pressIcon.activeSelf;
                this.pressIcon.SetActive(false);
                this.player.GetComponent<MyController>().setCanMove(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                this.pauseMenu.SetActive(true);
            }
            
        }
    }
    public void exitToMenu()
    {
        Time.timeScale=1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void resume()
    {
        Time.timeScale = 1;
        this.paused = false;
        this.player.GetComponent<MyController>().setCanMove(true);
        this.pressIcon.SetActive(this.pressIconBefore);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.pauseMenu.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Base Level",LoadSceneMode.Single);
    }
}
