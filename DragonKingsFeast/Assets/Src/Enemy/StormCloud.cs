using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: StormCloud
* -----------------
*
* this inhearits from the enemy class 
* it is used to manage the stomr cloud enemies.
* this is an obsalte anemy as strom clouds are no longer used in the game
*
* Author: Callum Dunstone
*/
public class StormCloud : Enemy {

    //this is the script used to manage the stomr clouds melee attack
    public MeleeAttack meleeAttack;

    /*
    * Function: Reset
    * ---------------
    *
    * this overrides the enemys classes reset function
    * so that it can also reset the rangedAttack for this enemy
    *
    * Author: Callum Dunstone
    */
    public override void Reset() {
        base.Reset();
        meleeAttack.Reset();
    }

    /*
    * Function:
    * ---------
    *
    * This is a unity monobehaviour base function
    *
    * this function is called at the start of the gameobjects life
    * it is used to get the player and set up its attack scipts
    * as well as assign its enemy type
    *
    * Author: Callum Dunstone
    */
    void Awake() {
        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.StormCloud;
    }

    /*
    * Function: Start
    * ---------------
    *
    * This is a unity monobehaviour base function
    *
    * called at the start of the game objects life. this is used to 
    * make sure the enemes health is correct and that it has the player
    * 
    * Highly likely this is obsoleate 
    *
    * Author: callum dunstone
    */
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }

    /*
    * Function: Update
    * ----------------
    *
    * This is a unity monobehaviour base function
    * 
    * if the enemy is behind the player then we deactivate the enemy
    * sending them to the object pool
    *
    * Author: Callum Dunstone
    */
    protected void Update() {

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    /*
    * Function: OnTriggerEnter
    * ------------------------
    *
    * This is a unity monobehaviour base function
    * 
    * if the player enters the enemies attack range we then attack them
    * the attack range for melee attacks is dictated through using collider triggers
    *
    * Author: Callum Dunstone
    */
    private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                meleeAttack.AttackPlayer();
            }
    }

    /*
    * Function: TakeDamage2 & TakeDamage
    * ----------------
    *
    * these are functions from the enemy class, we override them here as 
    * the strom cloud can not take dame and be killed
    *
    * Author: Callum Dunstone
    */
    public override void TakeDamage2(float damage) { }
    public override void TakeDamage() { }
}
