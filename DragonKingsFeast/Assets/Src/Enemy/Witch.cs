using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {
    void Awake() {
        m_enemyType = e_EnemyType.Witch;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
        m_hasAttacked = false;
    }
    public void Update() {
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
        Vector3.Distance(transform.position, player.transform.position);
    }
}
