using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSounds : MonoBehaviour
{

    public AudioSource sound1;
    private long cnt;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        cnt++;
        if (cnt%700 == 0)
            play();
        Debug.Log(cnt);
    }

    void play(){
        sound1.Play();
    }
}
