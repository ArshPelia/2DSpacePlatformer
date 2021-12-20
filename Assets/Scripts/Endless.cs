using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject StartEndless;
    public GameObject EndEndless;
    public GameObject MainCamera;
    public GameObject scoreUI;

    public void Go() {
        StartEndless.SetActive(false);
        EndEndless.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().rb.transform.position = GameObject.Find("Player").GetComponent<Player>().originalPos;
        GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().EndPlatformsX = Mathf.Infinity;
        MainCamera.GetComponent<CameraFollow>().enabled = false;
        MainCamera.GetComponent<CameraFixed>().enabled = true;
        MainCamera.GetComponent<CameraFixed>().Start();
        scoreUI.SetActive(true);
    }

    public void Stop() {
        EndEndless.SetActive(false);
        StartEndless.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().rb.transform.position = GameObject.Find("Player").GetComponent<Player>().originalPos;
        GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().EndPlatformsX = 50;
        MainCamera.GetComponent<CameraFixed>().enabled = false;
        MainCamera.GetComponent<CameraFollow>().enabled = true;
        GameObject.Find("Player").GetComponent<Player>().speed = 8f;
        scoreUI.SetActive(false);
    }
}
