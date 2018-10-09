using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerIK))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    private PlayerMovement pm;

    [Header("Player Stats")]
    public int health;

    public int score {
        get {
            return health;
        }
    }
    
    [Header("Projectile Info")]
    public Transform projectileSpawnPoint;

    public int damage;
    public int range;

    public float rangedAttackSpeed;

    public float attackCoolDownSpeed;
    [ReadOnly]
    private float attackTimer;

    public float projectileLiveTime;

    public float bulletAmmount;
    public float maxBulletAmmount;

    [Header("Boost Values")]
    public float attackBoostTime;
    public float boostDamage;
    private float m_attackBoostTimer;

    public float speedBoostTime;
    private float m_speedBoostTimer;
    public float speedBoost;

    private bool gameRunning;

    public float speedBoostTimer {
        get {
            return m_speedBoostTimer;
        }
    }

    private bool m_sheild;

    public void TakeDamage() {

        if (m_sheild) {
            m_sheild = false;
            return;
        }

        if (health > 0) {
            health = 0;

            GameObject g = GameObject.FindGameObjectWithTag("CoinSP");
            for (int i = 0; i < g.transform.childCount; i++) {
                Destroy(g.transform.GetChild(i).gameObject);
            }
        }
        else if (health == 0) {
            health = -1;
        }
    }

    public void hitMountain() {
        health = -1000000;
    }

    public void ApplyBoost(e_ItemType type) {
        switch (type) {
            case e_ItemType.Boost_Speed:
                m_attackBoostTimer = 0;
                break;

            case e_ItemType.Boost_Attack:
                m_speedBoostTimer = 0;
                break;

            case e_ItemType.Boost_Defense:
                m_sheild = true;
                break;

            case e_ItemType.Pickup:
                Debug.LogError("USE OTHER FUNCTION WRONG ONE");
                break;

            default:
                break;
        }
    }

    public void ApplyLoot(int amount) {
        health += amount;
    }


	// Use this for initialization
	void Start () {
        attackTimer = attackCoolDownSpeed;
        pm = GetComponent<PlayerMovement>();
        __event<e_UI>.Raise(this, EventHandle);
	}
	
	// Update is called once per frame
	void Update () {
        if (bulletAmmount < maxBulletAmmount) bulletAmmount += Time.deltaTime;

        attackTimer += Time.deltaTime;
        m_attackBoostTimer += Time.deltaTime;
        m_speedBoostTimer += Time.deltaTime;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        RaycastAttackByClick();
#endif

#if UNITY_ANDROID
        FindClosestEnemeyToAttack();
#endif

    }

    public void EventHandle(object o, __eArg<e_UI> e) {
        if (e.arg == e_UI.GAME) {
            gameRunning = true;
        }
        else {
            gameRunning = false;
        }
    }

    private void RaycastAttackByClick() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {

                if (hit.transform.gameObject.tag == "Enemy") {
                    Attack(hit.transform);
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

    private bool RaycastAttackByTouch() {
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {

                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {

                    if (hit.transform.gameObject.tag == "Enemy") {
                        Attack(hit.transform);
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

    private void FindClosestEnemeyToAttack() {
        if (GameManager.tutorialManager.LearnToFlyComp == true && gameRunning == true) {
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
                    dist = distance;
                    tran = holder[i].transform;
                }
            }

            if (tran != null) {
                Attack(tran);
            }
        }
    }

    public void Attack(Transform E) {
        float dist = Vector3.Distance(transform.position, E.position);

        if (bulletAmmount < 1) return;
        
        if (attackTimer > attackCoolDownSpeed) {
            if (dist <= range) {
                GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
                go.transform.position = projectileSpawnPoint.position;
                go.GetComponent<Projectile>().SetUp(E.transform, m_attackBoostTimer < attackBoostTime ? damage + boostDamage : damage, true, rangedAttackSpeed + pm.forwardSpeed, projectileLiveTime);
                attackTimer = 0;
                bulletAmmount--;
            }
        }
    }
}
