using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //static bc just want to check- don't actually want to use this script
    public static bool GameIsPaused = false;
    public static float musicvolume;
    public static float soundvolume;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject mainmenuUI;
    public string mainmenu;
    public string playbutton;
    [SerializeField] bool IsPauseMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (settingsMenuUI.activeSelf)
        {
            musicvolume = gameObject.GetComponentInChildren<Slider>(name == "Music Volume").value;
            soundvolume = gameObject.GetComponentInChildren<Slider>(name == "Sound Volume").value;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && IsPauseMenu == true)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else if (settingsMenuUI.activeSelf)
            {
                BackFromSettings();
            }
            else 
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //puts game back to normal
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        if (settingsMenuUI.activeSelf)
        {
            settingsMenuUI.SetActive(false);
        }
        pauseMenuUI.SetActive(true);
        //freezes game
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() 
    {
        Debug.Log("Loading Menu.");
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainmenu);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game.");
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(playbutton);
    }

    public void LoadSettings()
    {
        if (IsPauseMenu == true)
        {
            pauseMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
        else
        {
            mainmenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
    }

    public void BackFromSettings()
    {
        if (IsPauseMenu == true)
        {
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        else
        {
            settingsMenuUI.SetActive(false);
            mainmenuUI.SetActive(true);
        }
    }

}
