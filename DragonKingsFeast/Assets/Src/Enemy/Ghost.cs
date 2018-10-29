﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    public MeleeAttack meleeAttack;

    public override void Reset() {
        base.Reset();
        meleeAttack.Reset();
    }

    protected override void Awake() {
        base.Awake();

        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Ghost;
    }
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }

    protected override void Update() {
        base.Update();

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            spawner.GetListOfEnemies().Remove(this);
            TurnOff();
        }
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
            animat.SetBool("Attack", true);
            meleeAttack.AttackPlayer();
        }
    }
}
