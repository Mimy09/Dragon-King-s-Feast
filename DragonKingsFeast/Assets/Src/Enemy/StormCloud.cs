using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : Enemy {

    public MeleeAttack meleeAttack;

    public override void Reset() {
        base.Reset();
        meleeAttack.Reset();
    }

    void Awake() {
        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.StormCloud;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }

    public void Update() {
        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                meleeAttack.AttackPlayer();
            }
    }

    public override void TakeDamage(float damage) { }
}
