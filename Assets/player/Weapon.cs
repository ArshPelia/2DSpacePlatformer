using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint; 

    public GameObject bulletPrefab;
    public Player player;
    public AudioSource Plasma;
    public AudioSource Pistol;
    public AudioSource Laser;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            if (player.gunActive)
                Shoot();
        } 
		
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (player.currentGun.name == "Pistol"){
            Pistol.Play();
        } else if (player.currentGun.name == "Plasma Rifle"){
            Plasma.Play();
        } else if (player.currentGun.name == "Laser Rifle"){
            Laser.Play();
        }
    }
}
