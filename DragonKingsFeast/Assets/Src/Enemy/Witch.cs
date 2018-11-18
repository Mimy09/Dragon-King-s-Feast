using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: Witch
* ------------
*
* this is the wich sub class, it inhearits from the Enemy class
* this script is used for the witchs AI
*
* Author: Callum Dunstone
*/
public class Witch : Enemy {

    //this is a script used to manage using ranged attacks
    public RangedAttack rangedAttack;

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
        rangedAttack.Reset();
    }

    /*
    * Function: Awake
    * ---------------
    *
    * This is a unity monobehaviour base function
    *
    * this function is called at the start of the gameobjects life
    * it is used to get the player and set up its attack scipts
    * as well as assign its enemy type
    *
    * Author: Callum Dunstone
    */
    protected void Awake() {
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Witch;
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
    * this is used to check to see if we can shoot the player every frame
    * and if the enemy is behind the player then we deactivate the enemy
    * sending them to the object pool
    *
    * Author: Callum Dunstone
    */
    protected void Update() {
        //check if we can shoot the player
        rangedAttack.AttackPlayer();
        rangedAttack.Update();

        //check if we are behind the player and if so deactivate our selves
        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            spawner.GetListOfEnemies().Remove(this);

            TurnOff();
        }
    }
}
