using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntoductionText : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] phrases = new string[1];
    public int phraseCounter = 0;

    void Start(){
        textBox.SetText(phrases[0]);
    }

    public void nextPhrase(){
        phraseCounter++;
        if (phraseCounter < phrases.Length)
            textBox.SetText(phrases[phraseCounter]);  
        else 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

