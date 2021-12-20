using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string level = "";
    public int coins = 5;
    public int maxHealth = 100;
    public int currentHealth;
    public bool[] weapons = new bool[3];
    public int[] damage;
    public int currentDamage;
    public int[] potions = new int[3];
    public float highScore;
    public bool gameEnded;
    public bool hasDied;
    public bool hasKilled;

    public float upspeed; //trampoline jump

    public healthBar healthBar;

    public float speed;
    public float jumpforce;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor;
    float lastTimeGrounded;
    public int defaultAdditionalJumps;
    int additionalJumps;
	public float moveBy;

    public bool fell;
    public bool dead;
    public bool lastGroundedChecked;
    public bool hasJumped;

    public bool[] achievementsUnlocked = new bool[10];
    public float[] achievementsProgress = new float[10];
    public int kills;

    public GameObject achievementCanvas;
    public GameObject achievementDescription;
    public float achMoveSpeed = 2f;
    public bool achievementOn = false;

    

    public GameObject pauseMenu;
    public GameObject deadMenu;
    public TextMeshProUGUI textCoins;
    public Animator animator;
    private SpriteRenderer viewRender;
    public Vector3 originalPos;
    public Vector3 RespawnPos;

    public Rigidbody2D rb;

    public int currentWeaponIndex;
    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public bool gunActive = false;


    public AudioSource jumpSound;

    
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
        rb = GetComponent<Rigidbody2D>();
        viewRender = GetComponent<SpriteRenderer>();
        Time.timeScale = 1f;
        originalPos = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        loadPlayer();
        initalizeWeapons();
    }

    // Update is called once per frame
    void Update(){
        Move();
        animator.SetFloat("Speed", Mathf.Abs(moveBy));
        Jump();
        BetterJump();
        CheckIfGrounded();
        if (isGrounded)
        {
            upspeed = 500f;
        }
        CheckDirection();
        if (pauseMenu.activeSelf || deadMenu.activeSelf) {
            SavePlayer();
        }
        if (SceneManager.GetActiveScene().name == "RedPlanet-Boss_Transition") {
            SavePlayer();
        }
        if (SceneManager.GetActiveScene().name == "SpaceShip"){
            SavePlayer();
        }
        CheckIfFall();
        Heal();
        updateCoinText();
        CheckAchievementProgress();
        RespawnPos = new Vector3(rb.transform.position.x - 5, rb.transform.position.y, rb.transform.position.z);
        changeWeapon();
        CheckLevel();

        if (currentGun.name == "Pistol")
            Debug.Log(currentGun);
    }

    void CheckLevel(){
        if (SceneManager.GetActiveScene().name == "RedPlanet")
            level = "Red Planet";
        if (SceneManager.GetActiveScene().name == "ForestPlanet")
            level = "Forest Planet";
    }



    // Player Functions
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        rb.transform.position = RespawnPos;
        StartCoroutine(DamageAnimation());
    }
  
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }

    public void addCoins(int amount){
        coins += amount;
    }
    void Heal() {
        if (Input.GetKeyDown(KeyCode.E)){
            TryTakeHealthPot();
        }
    }

    void TryTakeHealthPot() {

    }

    void initalizeWeapons(){
        
        currentWeaponIndex = 0;
        currentGun = guns[currentWeaponIndex];
        guns[currentWeaponIndex].SetActive(true);
    }

    void changeWeapon(){
        if (Input.GetKeyDown(KeyCode.E) && currentWeaponIndex < guns.Length-1){
            // if (gunActive){
            //     disableGuns();
            // }

            Debug.Log("next Weapon");
            if (currentWeaponIndex < guns.Length){
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex++;
                if (currentWeaponIndex == 0){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = false;
                    currentDamage = 0;
                }
                else if (currentWeaponIndex == 1 && weapons[0]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[0];
                } else if (currentWeaponIndex == 2 && weapons[1]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[1];
                }
                else if (currentWeaponIndex == 3 && weapons[2]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[2];
                } else {
                    currentWeaponIndex--;
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                }
                // currentGun = guns[currentWeaponIndex];
                // guns[currentWeaponIndex].SetActive(true);
            }   

        } else if (Input.GetKeyDown(KeyCode.Q) && currentWeaponIndex >= 0){
            Debug.Log("prev Weapon");
            if (currentWeaponIndex < guns.Length){
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex--;
                if (currentWeaponIndex == 0){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = false;
                    currentDamage = 0;
                }
                else if (currentWeaponIndex == 1 && weapons[0]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[0];
                } else if (currentWeaponIndex == 2 && weapons[1]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[1];
                }
                else if (currentWeaponIndex == 3 && weapons[2]){
                    currentGun = guns[currentWeaponIndex];
                    guns[currentWeaponIndex].SetActive(true);
                    gunActive = true;
                    currentDamage = damage[2];
                } else {
                    currentWeaponIndex++;
                    guns[currentWeaponIndex].SetActive(true);
                }
                // currentGun = guns[currentWeaponIndex];
                // guns[currentWeaponIndex].SetActive(true);
            }   
        }
    }
    void disableGuns(){
        for (int i = 0; i < guns.Length; i++){
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
    }

/*
    void Hurt(){
        health--;
        if (health <= 0){
            dead = true;
        }else {

        }
    }*/


    // Movement
    public Vector3 GetPosition() {
        return transform.position;
    }
    void CheckDirection(){
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            viewRender.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            viewRender.flipX = true;
        }
    }
    
    void Move(){
    	float x = Input.GetAxisRaw("Horizontal");
    	moveBy = x * speed;
        if (!pauseMenu.activeSelf && !deadMenu.activeSelf) {
            achievementsProgress[6] += Math.Abs(moveBy) * Time.deltaTime;
        }
        if (moveBy != 0) {
            achievementsProgress[8] = 0;
        }
    	rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    
    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || additionalJumps > 0)){
            if (!pauseMenu.activeSelf && !deadMenu.activeSelf)
            {
                achievementsProgress[7] += 1;
            }
            achievementsProgress[8] = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            if (hasJumped) {
                additionalJumps--;
            }
            animator.SetBool("isJumping", true);
            hasJumped = true;
            jumpSound.Play();
    	}
    }
    
    void CheckIfGrounded() {
	Collider2D collider = Physics2D.OverlapCircle( isGroundedChecker.position, checkGroundRadius, groundLayer );
	if (collider != null) {
         animator.SetBool("isJumping", false);
	     isGrounded = true;
	     additionalJumps = defaultAdditionalJumps;
        if (lastGroundedChecked)
        {
            hasJumped = false;
        }
        lastGroundedChecked = false;
    } else {
	    if (isGrounded && !lastGroundedChecked) {
	        lastTimeGrounded = Time.time;
            lastGroundedChecked = true;
        }
        if (Time.time - lastTimeGrounded <= rememberGroundedFor && !hasJumped)
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
	}
    }

    void BetterJump() {
    	if (rb.velocity.y < 0) {
    	     rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
    	} else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
    	     rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
    	}  
    }

    void CheckIfFall() {
        if (rb.transform.position.y < -30) {
            //fell = true;
            TakeDamage(20);
            rb.transform.position = originalPos;
        }
    }

    public void addKill() {
        achievementsProgress[1] += 1;
        hasKilled = true;
    }

    void CheckAchievementProgress() {
        if (deadMenu.activeSelf)
        {
            achievementsProgress[1] = 0;
        }
        if (currentHealth <= 0) {
            hasDied = true;
        }
        if (!pauseMenu.activeSelf && !deadMenu.activeSelf && !gameEnded) {
            achievementsProgress[5] += Time.deltaTime;
        }
        if (!pauseMenu.activeSelf && !deadMenu.activeSelf)
        {
            achievementsProgress[8] += Time.deltaTime;
        }
        achievementsProgress[9] = highScore;
        if (!achievementOn)
        {
            if (weapons[0] && weapons[1] && weapons[2] && !achievementsUnlocked[0])
            {
                achievementsUnlocked[0] = true;
                StartCoroutine(TriggerAchievement("Purchase All Weapons"));
            }
            if (achievementsProgress[1] >= 100 && !achievementsUnlocked[1])
            {
                achievementsUnlocked[1] = true;
                StartCoroutine(TriggerAchievement("Confirm 100 Kills Without Dying"));
            }
            if (gameEnded && !hasDied && !achievementsUnlocked[2])
            {
                achievementsUnlocked[2] = true;
                StartCoroutine(TriggerAchievement("Complete the Game Without Dying"));
            }
            if (gameEnded && !hasKilled && !achievementsUnlocked[3])
            {
                achievementsUnlocked[3] = true;
                StartCoroutine(TriggerAchievement("Complete the Game Without Killing Anything"));
            }
            if (gameEnded && achievementsProgress[4] < 600 && !achievementsUnlocked[4])
            {
                achievementsUnlocked[4] = true;
                StartCoroutine(TriggerAchievement("Complete the Game In Under 10 Minutes"));
            }
            if (gameEnded && !weapons[0] && !weapons[1] && !weapons[2] && !achievementsUnlocked[5])
            {
                achievementsUnlocked[5] = true;
                StartCoroutine(TriggerAchievement("Complete the Game Without Purchasing Any Items"));
            }
            if (achievementsProgress[6] > 180836 && !achievementsUnlocked[6])
            {
                achievementsUnlocked[6] = true;
                StartCoroutine(TriggerAchievement("Travel a Marathon's Distance (~42km)"));
            }
            if (achievementsProgress[7] >= 1000 && !achievementsUnlocked[7])
            {
                achievementsUnlocked[7] = true;
                StartCoroutine(TriggerAchievement("Jump 1000 Times"));
            }
            if (achievementsProgress[8] >= 900 && !achievementsUnlocked[8])
            {
                achievementsUnlocked[8] = true;
                StartCoroutine(TriggerAchievement("Stand Still for 15 Minutes"));
            }
            if (highScore > 428 && achievementsUnlocked[9])
            {
                achievementsUnlocked[9] = true;
                StartCoroutine(TriggerAchievement("Get a Score of Over 100m in Endless Mode"));
            }
        }
    }

    IEnumerator TriggerAchievement(String s) {

        achievementOn = true;
        achievementDescription.GetComponent<TMPro.TextMeshProUGUI>().text = s;
        achievementCanvas.SetActive(true);
        Vector2 currentPos = achievementCanvas.transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 1f;
            achievementCanvas.transform.position = Vector2.Lerp(currentPos, new Vector2(currentPos.x, 135), t);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentPos = achievementCanvas.transform.position;
        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 1f;
            achievementCanvas.transform.position = Vector2.Lerp(currentPos, new Vector2(currentPos.x, -135), t);
            yield return null;
        }
        achievementCanvas.SetActive(false);
        achievementDescription.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        achievementOn = false;

    }
    // Player UI

    void updateCoinText() {
        if (coins < 10) 
            textCoins.text = "00" + coins.ToString();
        else if (coins < 100 && coins >= 10) 
            textCoins.text = "0" + coins.ToString();
        else
            textCoins.text = coins.ToString();
    }
    /*void updateHealtText() {

    }*/

    //moon rock collection
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "moonRock")
        {
            coins++;
            if (coins < 10)
                textCoins.text = "00" + coins.ToString();
            else if (coins < 100 && coins >= 10)
                textCoins.text = "0" + coins.ToString();
            else
                textCoins.text = coins.ToString();
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "Enemy")
        {
            TakeDamage(10);
           // rb.transform.position = originalPos;

        }
        else if (other.gameObject.CompareTag("Spike"))
        {
            currentHealth -= 10;
            healthBar.SetHealth(currentHealth);
            rb.transform.position = originalPos;

        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
            //rb.transform.position = originalPos;

        }
        if (other.transform.tag == "Kill" ){
            addKill();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tramp" && isGrounded == false)
        {
            upspeed += 300f;
            if( upspeed >= 1900f)
            {
                upspeed = 1900f;
            }
            rb.AddForce(new Vector2(0, upspeed));
        }
    }



    public void Restart(){
        rb.transform.position = originalPos;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        SavePlayer();
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void loadPlayer() {
        PlayerData data = SaveSystem.loadPlayer();

        level = data.level;
        currentHealth = data.health;
        coins = data.coins;
        highScore = data.highScore;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = data.weapons[i];
        }
        for (int i = 0; i < potions.Length; i++)
        {
            potions[i] = data.potions[i];
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
}
