using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlay : MonoBehaviour
{
    // Start is called before the first frame update   
    public AudioSource Click;
    public AudioSource Buy;
    public AudioSource Jump;
    public AudioSource Hover;

    public void PlayClick(){
        Click.Play();
    }
    public void PlayBuy(){
        Buy.Play();
    }
    public void PlayJump(){
        Jump.Play();
    }
    public void PlayHover()
    {
        Hover.Play();
    }
}
