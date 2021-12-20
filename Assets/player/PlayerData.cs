using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public string level;
    public int health;
    public int coins;
    public bool[] achievementsUnlocked = new bool[10];
    public float[] achievementsProgress = new float[10];
    public float highScore;
    public bool[] weapons = new bool[3];
    public int[] potions = new int[3];

    public PlayerData (Player Player) {
        level = Player.level;
        health = Player.currentHealth;
        coins = Player.coins;
        highScore = Player.highScore;
        for (int i = 0; i < weapons.Length; i ++){
            weapons[i] = Player.weapons[i];
        }
        for (int i = 0; i < potions.Length; i ++){
            potions[i] = Player.potions[i];
        }

        for (int i = 0; i < achievementsUnlocked.Length; i++) {
            achievementsUnlocked[i] = Player.achievementsUnlocked[i];
        }
        for (int i = 0; i < achievementsProgress.Length; i++)
        {
            achievementsProgress[i] = Player.achievementsProgress[i];
        }
    }
}
