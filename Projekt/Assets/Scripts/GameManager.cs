using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pressIcon;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private bool isMenu = false;
    [SerializeField]
    private GameObject settings;
    private bool paused = false;
    private GameObject player;
    private bool pressIconBefore;
    private bool isSettings;



    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.settings.GetComponent<Settings>().loadSettings();
        this.settings.SetActive(false);
        if (!this.isMenu)
        {
            this.pressIcon.SetActive(false);
            this.pauseMenu.SetActive(false);
        }
        else
        {
            this.pauseMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isMenu) return;
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (paused)
            {
                if (!this.isSettings)
                {
                    this.resume();
                }
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
        this.settings.SetActive(false);
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
    public void openSettings()
    {
        this.pauseMenu.SetActive(false);
        this.settings.SetActive(true);
        this.isSettings = true;
    }
    public void backToMenu()
    {
        this.settings.GetComponent<Settings>().save_settings();
        this.pauseMenu.SetActive(true);
        this.settings.SetActive(false);
        this.isSettings = false;
    }
}
