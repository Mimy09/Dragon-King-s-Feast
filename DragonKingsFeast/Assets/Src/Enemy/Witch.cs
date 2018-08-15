using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {

    //this variable dictates how fast the witchs attack projectile will move relative to it
    public float rangedAttackSpeed;

    //how quickly the witch can attack
    public float attackCoolDownSpeed;
    private float attackTimer = 0;

    void Awake() {
        m_enemyType = e_EnemyType.Witch;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
        m_hasAttacked = false;
    }
    public void Update() {
        attackTimer += Time.deltaTime;

        MoveToPlayer();
        ShootAttack();
    }

    private void MoveToPlayer() {
        transform.position += (Vector3.back * Time.deltaTime) * forwardSpeed;

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    private void ShootAttack() {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (attackTimer > attackCoolDownSpeed) {
            if (dist <= attackRange) {
                GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
                go.transform.position = transform.position + (transform.forward * 3);
                go.GetComponent<Projectile>().SetUp(player.transform.position, damage, false, rangedAttackSpeed + forwardSpeed);
                attackTimer = 0;
            }
        }
    }
}
