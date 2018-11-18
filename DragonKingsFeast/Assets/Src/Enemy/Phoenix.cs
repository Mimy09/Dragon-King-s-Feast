using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : Enemy {

    public RangedAttack rangedAttack;
    public MeleeAttack meleeAttack;

    public override void Reset() {
        base.Reset();
        rangedAttack.Reset();
        meleeAttack.Reset();
    }

    void Awake() {
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Phoenix;
    }

    protected void Update() {

        rangedAttack.Update();
        
        ShootAttack();

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    private void MoveToPlayer() {
        transform.LookAt(player.transform.position);
        transform.position += (transform.forward * Time.deltaTime) * speed;
        transform.position += (Vector3.back * Time.deltaTime) * forwardSpeed;

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }
    
    private void ShootAttack() {
        rangedAttack.AttackPlayer();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (meleeAttack.AttackPlayer()) {
                TurnOff();
            }
            
        }
    }

}
