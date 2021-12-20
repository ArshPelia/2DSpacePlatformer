using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCollider : MonoBehaviour
{

    public GameObject shop;
    public GameObject shopError;
    public AudioSource MenuOpen;

    void OnTriggerEnter2D() {
        shop.SetActive(true);
        MenuOpen.Play();
    }
    void OnTriggerExit2D() {
        shop.SetActive(false);
        shopError.SetActive(false);
    }
}
