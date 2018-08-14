using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : Enemy {

    void Awake() {
        m_enemyType = e_EnemyType.StormCloud;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
        m_hasAttacked = false;
    }

    public void Update() {
        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
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

    public override void TakeDamage(float damage) { }
}
