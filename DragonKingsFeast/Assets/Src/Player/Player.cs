using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this is the script that is handling the player stats
/// and player attacking as well as death
/// 
/// <para>Author: Callum Dunstone & Mitchell Jenkins </para>
/// 
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    /// <summary> reference to the player movement </summary> 
    private PlayerMovement pm;

    /// <summary> stores the players health </summary> 
    [Header("Player Stats")]
    public int health;

    /// <summary> wrapper for health to be accessed out side of the player script </summary> 
    public int score {
        get {
            return health;
        }
    }
    /// <summary> how many coins are in the players loot sack </summary> 
    public int currCoinCount;
    /// <summary> how many gems are in the players loot sack </summary> 
    public int currGemCount;
    /// <summary> the max amount of coins or gems that can be in the players loot sack at a given point </summary> 
    public int maxCoins;

    /// <summary> this is where the player projectiles spawn at </summary> 
    [Header("Projectile Info")]
    public Transform projectileSpawnPoint;

    /// <summary> how much damage the player dose </summary> 
    public int damage;
    /// <summary> how far away the player can target the enemy </summary> 
    public int range;

    /// <summary> how quickly the player projectile moves relative to the player character </summary> 
    public float rangedAttackSpeed;

    /// <summary> how often the player can spam out shots </summary> 
    public float attackCoolDownSpeed;
    /// <summary> elapsed time between attacks </summary> 
    [ReadOnly]  private float attackTimer;

    /// <summary> how long the projectile will live before committing suicide </summary> 
    public float projectileLiveTime;

    /// <summary> how many shots you have stored </summary> 
    public float bulletAmmount;
    /// <summary> max amount of shots you can have stored </summary> 
    public float maxBulletAmmount;

    /// <summary> how long the attack boost lasts for </summary> 
    [Header("Boost Values")]
    public float maxAttackBoostTime;
    /// <summary> elapsed time the boost has run for </summary> 
    [ReadOnly] public float attackBoostTimer;
    
    /// <summary> is the game paused or not </summary> 
    public bool gameRunning;

    /// <summary> the sound for when the shield brakes </summary> 
    public List<AudioClip> sheildBreakSounds;
    
    /// <summary> dose the player have a protective shield or not </summary> 
    public bool sheildBoost;

    /// <summary> the particle effects for flying through watter </summary> 
    public List<GameObject> watterEffects;
    /// <summary> the particle effects for flying through clouds </summary> 
    public List<GameObject> cloudEffects;
    /// <summary> this toggles on and off the particle for flying through clouds/water </summary> 
    public bool effectsOnOff = true;


    /// <summary>
    /// 
    /// this handles the player taking damage from enemies
    /// 
    /// </summary>
    public void TakeDamage() {

        //if the player has a shield remove the shield and exit the function
        if (sheildBoost) {
            sheildBoost = false;

            if(GetComponent<AudioSource>().clip = sheildBreakSounds[Random.Range(0, sheildBreakSounds.Count)]) {
                GetComponent<AudioSource>().Play();
            }
            else {
                Debug.Log("NO AUDIO SOURCE ON " + gameObject.name);
            }

            return;
        }

        //remove all off the players score and loot in their loot bag
        if (health > 0) {
            health = 0;

            GameObject g = GameObject.FindGameObjectWithTag("CoinSP");
            for (int i = 0; i < g.transform.childCount; i++) {
                Destroy(g.transform.GetChild(i).gameObject);
            }

            currCoinCount = 0;
        }// if they already have no loot they dye
        else if (health == 0) {
            health = -1;
            Dye();
        }

    }

    /// <summary>
    /// 
    /// this resets all of the players values and clears the coin in the loot bag
    /// 
    /// </summary>
    public void Reset() {
        GameObject g = GameObject.FindGameObjectWithTag("CoinSP");
        if (g != null) {
            int h = g.transform.childCount;
            for (int i = 0; i < h; i++) {
                Destroy(g.transform.GetChild(0).gameObject);
            }
        }
        currCoinCount = 0;
        health = 0;
        attackBoostTimer = maxAttackBoostTime;
        sheildBoost = false;
    }

    /// <summary>
    /// 
    /// this is used to inform the game the player is dead
    /// 
    /// </summary>
    public void Dye() {
        GameManager.instance.GetPlayerMovement().ResetPlayerPos();
        Reset();

        __event<e_UI>.InvokeEvent(this, e_UI.ENDGAME);
    }

    /// <summary>
    /// 
    /// this function is used if the player flys into the terrain 
    /// 
    /// </summary>
    public void hitMountain() {
        health = -1000000;
    }

    /// <summary>
    /// 
    /// this is used to apply a boost to the player depending on the
    /// enum type passed
    /// 
    /// </summary>
    public void ApplyBoost(e_ItemType type) {
        switch (type) {
            case e_ItemType.Boost_Speed:
                attackBoostTimer = 0;
                break;

            case e_ItemType.Boost_Attack:
                attackBoostTimer = 0;
                break;

            case e_ItemType.Boost_Defense:
                sheildBoost = true;
                break;

            case e_ItemType.Pickup:
                Debug.LogError("USE ApplyLoot(int amount) FUNCTION, USING WRONG ONE");
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// this applies any loot we pick up to our health
    /// as well as giving us a visual representation in our loot bag
    /// 
    /// </summary>
    public void ApplyLoot(int amount) {
        health += amount;
        currCoinCount += amount;
        
        if (currCoinCount >= maxCoins) {
            
            GameObject SP = GameObject.FindGameObjectWithTag("CoinSP");

            int j = 0;
            for (int i = 0; i < SP.transform.childCount; i++) {
                if (SP.transform.GetChild(i).tag == "Coin") {
                    j++;
                    currCoinCount--;
                    Destroy(SP.transform.GetChild(i).gameObject);
                }
                if (j >= maxCoins) break;
                if (currCoinCount == 0) {
                    break;
                }
            }

            GameObject go = Instantiate(Helper.GemPath, new Vector3(0, 0, 0), Quaternion.identity, SP.transform) as GameObject;
            go.GetComponent<RectTransform>().localPosition = new Vector3(
                Random.Range(-Screen.width / 40, Screen.width / 40),
                0,
                0
                );

            currGemCount++;
        }

        if (currGemCount >= maxCoins) {

            GameObject SP = GameObject.FindGameObjectWithTag("CoinSP");

            int j = 0;
            for (int i = 0; i < SP.transform.childCount; i++) {

                if (SP.transform.GetChild(i).tag == "Gem") {
                    currGemCount--;
                    j++;
                    Destroy(SP.transform.GetChild(i).gameObject);
                }
                if (j >= maxCoins) break;
                if (currGemCount == 0) {
                    break;
                }
            }

            GameObject go = Instantiate(Helper.Gem2Path, new Vector3(0, 0, 0), Quaternion.identity, SP.transform) as GameObject;
            go.GetComponent<RectTransform>().localPosition = new Vector3(
                Random.Range(-Screen.width / 40, Screen.width / 40),
                0,
                0
                );
        }
    }


    /// <summary>
    /// 
    /// called when the player first starts the game
    /// 
    /// </summary>
    void Start () {
        attackTimer = attackCoolDownSpeed;
        pm = GetComponent<PlayerMovement>();
        __event<e_UI>.Raise(this, EventHandle);
	}

    /// <summary>
    /// 
    /// called every frame, checks the player is still alive.
    /// handles any attack requests and managing of boosts and particle effects
    /// 
    /// </summary>
    void Update () {

        if (health < 0) {
            Dye();
        }

        if (bulletAmmount < maxBulletAmmount) bulletAmmount += Time.deltaTime;

        attackTimer += Time.deltaTime;
        attackBoostTimer += Time.deltaTime;

#if UNITY_STANDALONE_WIN
        RaycastAttackByClick();
#endif

#if UNITY_ANDROID
        RaycastAttackByTouch();
#endif

        ManageParticalEffects();
    }

    public void EventHandle(object o, __eArg<e_UI> e) {
        if (e.arg == e_UI.GAME) {
            gameRunning = true;
        }
        else {
            gameRunning = false;
        }
    }

    /// <summary>
    /// 
    /// this is used to manage what particle effects are active and when
    /// 
    /// </summary>
    public void ManageParticalEffects() {

        if (transform.position.y <= 0) {
            if (GameManager.mapManager.isLoaded_level1 == true) {
                if (cloudEffects.Count > 0 && cloudEffects[0].activeSelf == false) {
                    for (int i = 0; i < cloudEffects.Count; i++) {
                        cloudEffects[i].SetActive(true);
                    }
                }

                if (watterEffects.Count > 0 && watterEffects[0].activeSelf == true) {
                    for (int i = 0; i < watterEffects.Count; i++) {
                        watterEffects[i].SetActive(false);
                    }
                }
            }
            else {
                if (cloudEffects.Count > 0 && cloudEffects[0].activeSelf == true) {
                    for (int i = 0; i < cloudEffects.Count; i++) {
                        cloudEffects[i].SetActive(false);
                    }
                }

                if (watterEffects.Count > 0 && watterEffects[0].activeSelf == false) {
                    for (int i = 0; i < watterEffects.Count; i++) {
                        watterEffects[i].SetActive(true);
                    }
                }
            }
        }
        else {
            if (cloudEffects.Count > 0 && cloudEffects[0].activeSelf == true) {
                for (int i = 0; i < cloudEffects.Count; i++) {
                    cloudEffects[i].SetActive(false);
                }
            }

            if (watterEffects.Count > 0 && watterEffects[0].activeSelf == true) {
                for (int i = 0; i < watterEffects.Count; i++) {
                    watterEffects[i].SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// this is the function used when the player clicks to see if the are targeting any enemies for attack
    /// 
    /// </summary>
    private void RaycastAttackByClick() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {

                if (hit.transform.gameObject.tag == "Enemy") {
                    if (hit.transform.gameObject.GetComponent<Enemy>().GetHealth() > 0)
                        Attack(hit.transform.gameObject.GetComponent<Enemy>());
                }
                else {
                    FindClosestEnemeyToAttack();
                }
            }
            else {
                FindClosestEnemeyToAttack();
            }
        }
    }

    /// <summary>
    /// 
    /// this is the function used when playing on a mobile devise to see
    /// if the player is trying to target a specific enemy for attack
    /// 
    /// </summary>
    private bool RaycastAttackByTouch() {
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
            //if (Input.touchCount > 0 && Input.GetTouch(i).phase == TouchPhase.Began) { 
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);


                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {

                    if (hit.transform.gameObject.tag == "Enemy") {
                        if (hit.transform.gameObject.GetComponent<Enemy>().GetHealth() > 0)
                            Attack(hit.transform.gameObject.GetComponent<Enemy>());
                        return true;
                    }
                    else {
                        FindClosestEnemeyToAttack();
                    }
                }
                else {
                    FindClosestEnemeyToAttack();
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// if the player touched or click the screen with out targeting an enemy
    /// we search for the nearest enemy and try and attack them instead
    /// 
    /// </summary>
    private void FindClosestEnemeyToAttack() {
        if (gameRunning == true) {
            float dist = range;
            Transform tran = null;
            if (GameManager.enemyList.Count == 0) return;
            List<GameObject> holder = GameManager.enemyList;

            for (int i = 0; i < holder.Count; i++) {
                float distance = Vector3.Distance(transform.position, holder[i].transform.position);
                if (distance < dist &&
                    transform.position.z < holder[i].transform.position.z &&
                    holder[i].GetComponent<Enemy>().EnemyType != e_EnemyType.StormCloud &&
                    holder[i].activeInHierarchy == true
                    ) {
                    if (holder[i].GetComponent<Enemy>().GetHealth() <= 0) continue; 
                    dist = distance;
                    tran = holder[i].transform;
                }
            }

            if (tran != null) {
                Attack(tran.gameObject.GetComponent<Enemy>());
            }
        }
    }

    /// <summary>
    /// 
    /// if we have an enemy we want to attack then we use this function to initiate the
    /// attack against them, spawning a projectile to shoot at them and dealing damage to
    /// our target
    /// 
    /// </summary>
    public void Attack(Enemy E) {
        float dist = Vector3.Distance(transform.position, E.transform.position);

        if (bulletAmmount < 1) return;
        
        if (attackTimer > attackCoolDownSpeed) {
            if (dist <= range) {
                GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
                go.transform.position = projectileSpawnPoint.position;
                go.GetComponent<Projectile>().SetUp(E.transform, 0, true, rangedAttackSpeed + pm.forwardSpeed, projectileLiveTime);

                E.TakeDamage2(attackBoostTimer < maxAttackBoostTime ? damage * 2 : damage );

                attackTimer = 0;
                bulletAmmount--;
            }
        }
    }
}
