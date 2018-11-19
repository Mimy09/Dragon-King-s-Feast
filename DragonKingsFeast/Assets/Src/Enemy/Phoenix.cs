using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this class controls the AI for the Phoenix enemy
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
public class Phoenix : Enemy {

    /// <summary> this handles the ranged attacks of the phoenix </summary> 
    public RangedAttack rangedAttack;
    /// <summary> this handles the melee attacks of the phoenix </summary> 
    public MeleeAttack meleeAttack;

    /// <summary>
    /// 
    /// this overrides the enemy classes reset function
    /// so that it can also reset the rangedAttack and meleeAttack for this enemy
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    public override void Reset() {
        base.Reset();
        rangedAttack.Reset();
        meleeAttack.Reset();
    }

    /// <summary>
    /// 
    /// this function is called at the start of the gameobjects life
    /// it is used to get the player and set up its attack scripts
    /// as well as assign its enemy type
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    void Awake() {
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Phoenix;
    }


    /// <summary>
    /// 
    /// this is used to check to see if we can shoot the player every frame
    /// and if the enemy is behind the player then we deactivate the enemy
    /// sending them to the object pool  
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    protected void Update() {

        rangedAttack.Update();
        rangedAttack.AttackPlayer();

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }

    /// <summary>
    /// 
    /// this is used to check if the player runs in to the phoenix that then blows up to attack the player 
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            meleeAttack.DealDamage();
            m_health = 0;
            TakeDamage();
        }
    }

}
