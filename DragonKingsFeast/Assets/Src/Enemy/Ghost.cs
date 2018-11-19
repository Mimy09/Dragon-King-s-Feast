using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this Class manages the Ghost AI
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
public class Ghost : Enemy {

    /// <summary> this manages the melee attack of the ghost </summary> 
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
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    protected void Awake() {

        player = GameManager.player;
        meleeAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Ghost;
    }
    
    /// <summary>
    /// 
    /// called at the start of the game objects life. this is used to 
    /// make sure the enemies health is correct and that it has the player
    ///  
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }
    
    /// <summary>
    /// 
    /// this just checks if we have moved behind the player and if we have we deactivate this enemy
    ///  
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    protected void Update() {

        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            spawner.GetListOfEnemies().Remove(this);
            TurnOff();
        }
    }

    /// <summary>
    /// 
    /// this is used to check if the player has moved into attack range of the ghost 
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            animat.SetBool("Attack", true);
            meleeAttack.AttackPlayer();
        }
    }
}
