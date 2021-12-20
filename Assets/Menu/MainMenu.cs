using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour{
    public static int saveNumber;
    public TextMeshProUGUI health1;
    public TextMeshProUGUI coins1;
    public TextMeshProUGUI level1;
    public Image heart1;
    public Image moonrock1;
    public TextMeshProUGUI health2;
    public TextMeshProUGUI coins2;
    public TextMeshProUGUI level2;
    public Image heart2;
    public Image moonrock2;
    public TextMeshProUGUI health3;
    public TextMeshProUGUI coins3;
    public TextMeshProUGUI level3;
    public Image heart3;
    public Image moonrock3;
    public TextMeshProUGUI newGame1;
    public TextMeshProUGUI newGame2;
    public TextMeshProUGUI newGame3;



    public void playGame() {
        SceneManager.LoadScene("SpaceShip");
    }

    public void redPlanet(){
        SceneManager.LoadScene("RedPlanet");
    }
    public void forestPlanet(){

        SceneManager.LoadScene("ForestPlanet");
    }
    public void quitGame() {
        Application.Quit();
    }
    public void load() {
        newGame1.enabled = false;
        newGame2.enabled = false;
        newGame3.enabled = false;

        MainMenu.saveNumber = 0;
        PlayerData data1 = SaveSystem.loadPlayer();
        if(data1 == null){
            newGame1.enabled = true;
            heart1.enabled = false;
            moonrock1.enabled = false;
            level1.enabled = false;
        } else {
            health1.text = data1.health.ToString();
            coins1.text = data1.coins.ToString();
            level1.text = data1.level;
        }

        MainMenu.saveNumber = 1;
        PlayerData data2 = SaveSystem.loadPlayer();
        if(data2 == null){
            newGame2.enabled = true;
            heart2.enabled = false;
            moonrock2.enabled = false;
            level2.enabled = false;
        } else {
            health2.text = data2.health.ToString();
            coins2.text = data2.coins.ToString();
            level2.text = data2.level;
        }

        MainMenu.saveNumber = 2;
        PlayerData data3 = SaveSystem.loadPlayer();

        if(data3 == null){
            newGame3.enabled = true;
            heart3.enabled = false;
            moonrock3.enabled = false;
            level3.enabled = false;
        } else {
            health3.text = data3.health.ToString();
            coins3.text = data3.coins.ToString();
            level3.text = data3.level;
        }
    }
    public void firstSave() {
        MainMenu.saveNumber = 0;
    }
    public void secondSave() {
        MainMenu.saveNumber = 1;
    }
    public void thirdSave() {
        MainMenu.saveNumber = 2;
    }
}
