using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    void Awake() {
        m_enemyType = e_EnemyType.Ghost;
    }
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
        m_hasAttacked = false;
    }

    public void Update() {
        MoveToPlayer();
    }

    private void MoveToPlayer() {
        transform.LookAt(player.transform.position);
        transform.position += (transform.forward * Time.deltaTime) * speed;
        transform.position += (Vector3.back * Time.deltaTime) * forwardSpeed;

        if(player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (m_hasAttacked == false) {
            if (other.tag == "Player") {
                player.TakeDamage(damage);
                m_hasAttacked = true;
            }
        }
    }
}
