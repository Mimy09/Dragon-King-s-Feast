using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerIK))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    public int health;

    public int score {
        get {
            return health;
        }
    }

    public PlayerMovement pm;

    public Transform projectileSpawnPoint;

    public int damage;
    public int range;

    public float rangedAttackSpeed;

    public float attackCoolDownSpeed;
    [ReadOnly]
    private float attackTimer;


    public void TakeDamage(float amount) {
        health -= (int)(amount + 0.5f);
    }

    public void ApplyBoost(e_ItemType type) {
        switch (type) {
            case e_ItemType.Boost_Speed:
                break;

            case e_ItemType.Boost_Attack:
                break;

            case e_ItemType.Boost_Defense:
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
	}
	
	// Update is called once per frame
	void Update () {
        attackTimer += Time.deltaTime;

    #if UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {
                
                if (hit.transform.gameObject.tag == "Enemy") {
                    Attack(hit.transform);
                }
            }
        }
#endif

#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) { 

                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, ray.direction * 100, out hit)) {

                    if (hit.transform.gameObject.tag == "Enemy") {
                        Attack(hit.transform);
                    }

                }
             }
        }
#endif

    }

    public void Attack(Transform E) {
        float dist = Vector3.Distance(transform.position, E.position);

        if (attackTimer > attackCoolDownSpeed) {
            if (dist <= range) {
                GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
                go.transform.position = projectileSpawnPoint.position;
                go.GetComponent<Projectile>().SetUp(E.transform.position, damage, true, rangedAttackSpeed + pm.forwardSpeed);
                attackTimer = 0;
            }
        }
    }
}
