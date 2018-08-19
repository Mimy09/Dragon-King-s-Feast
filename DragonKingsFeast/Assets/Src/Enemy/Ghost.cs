using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    public MeleeAttack meleeAttack;

    public override void Reset() {
        base.Reset();
        meleeAttack.Reset();
    }

    void Awake() {
        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Ghost;
    }
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
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
        if (other.tag == "Player") {
            meleeAttack.AttackPlayer();
        }
    }
}
