using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class AchievementMenu : MonoBehaviour
{
    public Graphic m_Graphic;
    public Color m_MyColor;
    public Text m_Text;
    public TMPro.TextMeshProUGUI m_TMProText;
    public bool[] weapons = new bool[3];
    public bool[] achievementsUnlocked = new bool[10];
    public float[] achievementsProgress = new float[10];
    public GameObject pauseMenu;
    public GameObject deadMenu;

    // Start is called before the first frame update
    void Start()
    {
        loadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.activeSelf && !deadMenu.activeSelf) {
            loadPlayer();
            setAchievementProgress();
            CheckAchievementUnlocked();
        }
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = data.weapons[i];
        }
        for (int i = 0; i < achievementsUnlocked.Length; i++)
        {
            achievementsUnlocked[i] = data.achievementsUnlocked[i];
        }
        for (int i = 0; i < achievementsProgress.Length; i++)
        {
            achievementsProgress[i] = data.achievementsProgress[i];
        }
    }

    public void setAchievementProgress() {

        int weaponsOwned = 0;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i]) {
                weaponsOwned++;
            }
        }
        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = weaponsOwned.ToString() + " / 3";

        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = achievementsProgress[1].ToString() + " / 100";

        /*
        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = achievementsProgress[3].ToString() + " / ?";

        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = achievementsProgress[4].ToString() + " / ?";

        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = achievementsProgress[5].ToString() + " / ?";
        */

        float distance;
        distance = ((achievementsProgress[6] / 8f) * 1.4f) / 1000f;
        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = Math.Round(distance, 3).ToString() + "km / 42.195km";

        float jumps;
        jumps = achievementsProgress[7];
        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = jumps.ToString() + " / 1000";

        distance = (achievementsProgress[9] / 8f) * 1.4f;
        m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(9).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        m_Text.text = Math.Round(distance, 3).ToString() + "m / 100m";
    }

    public void CheckAchievementUnlocked () {
        for (int i = 0; i < achievementsUnlocked.Length; i++){
            if (achievementsUnlocked[i]) {
                m_MyColor = new Color(0.2762609f, 0.509804f, 0.2431373f, 1f);
                m_Graphic = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<Graphic>();
                m_Graphic.color = m_MyColor;
                m_Text = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
                m_Text.text = "Completed!";
                if (i == 8) {
                    m_TMProText = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    m_TMProText.text = "Stand Still for 15 Minutes";
                }
            }
        }
    }
}
