using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
   
    [SerializeField]
    private WaveSpawner waveSpawner;

    private void Start()
    {
        if(gm == null)
        {
            gm = this;
           // gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        }
    }

    /*public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2; 

    public IEnumerator RespawnPlayer()
    {
        Debug.Log("TODO: ADD SPAWN PARTICLES");
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("TODO: ADD SPAWN PARTICLES");
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine (gm.RespawnPlayer());
    }*/
}
