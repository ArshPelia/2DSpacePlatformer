using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGameCollider : MonoBehaviour
{
    public Player player;

    void Update(){
        player = FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D() {
        if (player.level == "Red Planet" && SceneManager.GetActiveScene().name == "SpaceShip")
            SceneManager.LoadScene("RedPlanet");
        if (player.level == "Forest Planet" && SceneManager.GetActiveScene().name == "SpaceShip")
            SceneManager.LoadScene("ForestPlanet");
        else
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
