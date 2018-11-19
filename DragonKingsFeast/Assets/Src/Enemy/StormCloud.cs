using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this inherits from the enemy class 
/// it is used to manage the storm cloud enemies.
/// this is an obsolete enemy as storm clouds are no longer used in the game
/// 
/// <para>Author: Callum Dunstone</para>
/// 
/// </summary>
public class StormCloud : Enemy {

    /// <summary> this is the script used to manage the storm clouds melee attack </summary>
    public MeleeAttack meleeAttack;
    
    /// <summary>
    /// 
    /// this overrides the enemy classes reset function
    /// so that it can also reset the rangedAttack for this enemy
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    public override void Reset() {
        base.Reset();
        meleeAttack.Reset();
    }
    
    /// <summary>
    /// 
    /// this function is called at the start of the gameobjects life
    /// it is used to get the player and set up its attack scripts
    /// as well as assign its enemy type
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    void Awake() {
        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.StormCloud;
    }
    
    /// <summary>
    /// 
    /// called at the start of the game objects life. this is used to 
    /// make sure the enemies health is correct and that it has the player
    /// 
    /// <para>Highly likely this is obsolete </para>
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }
    
    /// <summary>
    /// 
    /// if the enemy is behind the player then we deactivate the enemy
    /// sending them to the object pool
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    protected void Update() {

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            TurnOff();
        }
    }
    
    /// <summary>
    /// 
    /// if the player enters the enemies attack range we then attack them
    /// the attack range for melee attacks is dictated through using collider triggers
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                meleeAttack.AttackPlayer();
            }
    }
    
    /// <summary>
    /// 
    /// these are functions from the enemy class, we override them here as
    /// the storm cloud can not take dame and be killed
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    public override void TakeDamage2(float damage) { }
    /// <summary>
    /// 
    /// these are functions from the enemy class, we override them here as
    /// the storm cloud can not take dame and be killed
    /// 
    /// <para> Author: Callum Dunstone </para>
    /// 
    /// </summary>
    public override void TakeDamage() { }
}
