using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {

    public RangedAttack rangedAttack;

    public override void Reset() {
        base.Reset();
        rangedAttack.Reset();
    }

    void Awake() {
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Witch;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }

    public void Update() {
        //rangedAttack.Update();

        //MoveForward();
        //ShootAttack();
    }

    private void MoveForward() {
        transform.position += (Vector3.back * Time.deltaTime) * forwardSpeed;

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    private void ShootAttack() {
        rangedAttack.AttackPlayer();
    }
}
