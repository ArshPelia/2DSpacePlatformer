using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFixed : MonoBehaviour
{
    public Transform target;
    float step;
    float originalStep;
    float originalSpeed;
    Vector2 screenBounds;
    Vector2 screenOrigo;
    Vector3 originalPosition;
    Vector3 originalPos;
    bool resetting;
    int playerHealth;
    float score;
    float distance;
    bool higher;
    public GameObject scoreUI;
    // Start is called before the first frame update
    public void Start()
    {
        transform.parent = null;
        score = 0f;
        step = 3f;
        originalStep = step;
        GameObject.Find("Player").GetComponent<Player>().speed = 8f;
        originalSpeed = GameObject.Find("Player").GetComponent<Player>().speed;
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        originalPos = new Vector3(GameObject.Find("Player").GetComponent<Player>().rb.transform.position.x, GameObject.Find("Player").GetComponent<Player>().rb.transform.position.y, GameObject.Find("Player").GetComponent<Player>().rb.transform.position.z);
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
        distance = (GameObject.Find("Player").GetComponent<Player>().highScore / 8f) * 1.4f;
        scoreUI.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Math.Round(distance, 3).ToString() + "m";
        higher = false;
        resetting = false;
    }

    // Update is called once per frame
    private void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        if (GameObject.Find("Player").GetComponent<Player>().currentHealth < playerHealth)
        {
            resetting = true;
            GameObject.Find("Player").GetComponent<Player>().rb.transform.position = originalPos;
            transform.position = originalPosition;
            GameObject.Find("Player").GetComponent<Player>().speed = originalSpeed;
            step = originalStep;
            if (higher)
            {
                GameObject.Find("Player").GetComponent<Player>().highScore = score;
                distance = (score / 8f) * 1.4f;
                scoreUI.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Math.Round(distance, 3).ToString() + "m";
            }
            score = 0f;
            scoreUI.transform.GetChild(2).gameObject.SetActive(false);
            scoreUI.transform.GetChild(3).gameObject.SetActive(true);
            scoreUI.transform.GetChild(4).gameObject.SetActive(true);
            higher = false;
            resetting = false;
        } else if ((target.position.x > screenBounds.x || target.position.x < screenOrigo.x) && !resetting)
        {
            resetting = true;
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(1);
            GameObject.Find("Player").GetComponent<Player>().rb.transform.position = originalPos;
            transform.position = originalPosition;
            GameObject.Find("Player").GetComponent<Player>().speed = originalSpeed;
            step = originalStep;
            if (higher)
            {
                GameObject.Find("Player").GetComponent<Player>().highScore = score;
                distance = (score / 8f) * 1.4f;
                scoreUI.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Math.Round(distance, 3).ToString() + "m";
            }
            score = 0f;
            scoreUI.transform.GetChild(2).gameObject.SetActive(false);
            scoreUI.transform.GetChild(3).gameObject.SetActive(true);
            scoreUI.transform.GetChild(4).gameObject.SetActive(true);
            higher = false;
            resetting = false;
        }
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
        var cameraPosition = transform.position;
        cameraPosition.x += step * Time.deltaTime;
        transform.position = cameraPosition;
        score = score + (step * Time.deltaTime);
        distance = (score / 8f) * 1.4f;
        scoreUI.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Math.Round(distance, 3).ToString() + "m";
        if (score > GameObject.Find("Player").GetComponent<Player>().highScore && !higher) {
            scoreUI.transform.GetChild(2).gameObject.SetActive(true);
            scoreUI.transform.GetChild(3).gameObject.SetActive(false);
            scoreUI.transform.GetChild(4).gameObject.SetActive(false);
            higher = true;
        }
        GameObject.Find("Player").GetComponent<Player>().speed = GameObject.Find("Player").GetComponent<Player>().speed + (1 * Time.deltaTime);
        step = step + (1 * Time.deltaTime);
    }
}
