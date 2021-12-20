using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour{

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 60f;

    [SerializeField] private Transform LevelPart_Start;

    [SerializeField] private List<Transform> LevelPartList;
    [SerializeField] private Transform BossLevel;
    [SerializeField] private Player player;
    [SerializeField] public float EndPlatformsX;

    private bool Spawned;

    int playerHealth;

    private Vector3 lastEndPosition;

    private int [] array = new int[]{0,1,2,3,4,5};
    private List<int> list = new List<int>{0,1,2,3,4,5};

    void Start() {
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }

    private void Awake() {
        lastEndPosition = LevelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++) {
            SpawnLevelPart();
        }
    }

    private void Update() {
        if (GameObject.Find("Player").GetComponent<Player>().currentHealth < playerHealth)
        {
            Awake();
        }
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;

        if (Vector3.Distance(player.GetPosition(), lastEndPosition)  < PLAYER_DISTANCE_SPAWN_LEVEL_PART && EndPlatformsX != player.transform.position.x && !Spawned){
            SpawnLevelPart();
        } else if (EndPlatformsX <= player.transform.position.x - PLAYER_DISTANCE_SPAWN_LEVEL_PART && !Spawned) {
            SpawnBossLevel();
        } else 
            return;
    }
  
    private void SpawnLevelPart() {
        // add up level
        Transform chosenLevelPart = LevelPartList[Random.Range(0, LevelPartList.Count)];
        Transform lastLevelPartTransform  = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Transform LevelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return LevelPartTransform;
    }

    private void SpawnBossLevel() {
        Transform chosenLevelPart = BossLevel;
        Transform lastLevelPartTransform  = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        Spawned = true;
    }
}
