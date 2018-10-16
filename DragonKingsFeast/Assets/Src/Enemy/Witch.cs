using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {

    public RangedAttack rangedAttack;

    public override void Reset() {
        base.Reset();
        rangedAttack.Reset();
    }

    protected override void Awake() {
        base.Awake();
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Witch;
    }

    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }

    public void Update() {

        //MoveForward();

        ShootAttack();
        rangedAttack.Update();

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            spawner.GetListOfEnemies().Remove(this);

            TurnOff();
        }
    }

    private void MoveForward() {
        transform.position += (Vector3.back * Time.deltaTime) * forwardSpeed;
    }

    private void ShootAttack() {
        rangedAttack.AttackPlayer();
    }
}
