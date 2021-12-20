using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; 
    public GameObject pauseMenuUI;
    public GameObject achievementMenu;
    public GameObject deadUI;
    public GameObject ControlsUI;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("Player").GetComponent<Player>().fell) {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
        if (GameObject.Find("Player").GetComponent<Player>().currentHealth <= 0 || GameObject.Find("Player").GetComponent<Player>().dead){
            Dead();
        }
            
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        achievementMenu.SetActive(false);
        ControlsUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        GameObject.Find("Player").GetComponent<Player>().Restart();
        SceneManager.LoadScene(0);
        GameIsPaused = false;
    }

    public void LevelSelect() {
        GameObject.Find("Player").GetComponent<Player>().Restart();
        SceneManager.LoadScene(1);
        GameIsPaused = false;
    }

    public void Dead() {
        deadUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart() {
        deadUI.SetActive(false);
        GameObject.Find("Player").GetComponent<Player>().fell = false;
        GameObject.Find("Player").GetComponent<Player>().Restart();
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
