using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this script is used as the base class for the different attack options of the enemies
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
[System.Serializable]
public class EnemyActions{

    /// <summary> this is the amount of damage that the attack can do </summary> 
    public float damage;
    /// <summary> a reference to the player so that we can deal damage to them </summary> 
    public Player player;
    /// <summary> reference to the enemy that uses this script </summary> 
    protected Enemy owner;

    public EnemyActions() {}

    /// <summary>
    /// 
    /// this is used to set up the class getting a reference to the player and the enemy using this script
    /// 
    /// </summary>
    public void SetUp(Player p, Enemy e) { player = p; owner = e; }

    /// <summary>
    /// 
    /// this is used to start the attack of the enemy against the player beginning the attack animation
    /// 
    /// </summary>
    public virtual bool AttackPlayer() { return false; }

    /// <summary>
    /// 
    /// this is used deal the damage to the player at the appropriate time in the attack animation animation
    /// 
    /// </summary>
    public virtual void DealDamage() { }

    /// <summary>
    /// 
    /// this is used to reset any values that might need resetting
    /// 
    /// </summary>
    public virtual void Reset() { }
}

/// <summary>
/// 
/// this script is used for preforming melee attacks with the enemies
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
[System.Serializable]
public class MeleeAttack : EnemyActions {

    /// <summary> determines if it has made a melee attack or not </summary> 
    protected bool m_hasAttacked;
    /// <summary> if the enemey can attack multiple times this is set to true </summary> 
    public bool canAttackMultipleTimes = false;
    /// <summary> if this is set to true then we play the attack animation </summary> 
    public bool usesAnimation = true;

    /// <summary>
    /// 
    /// this is used to start the attack of the enemy against the player beginning the attack animation
    /// 
    /// </summary>
    public override bool AttackPlayer() {
        //normally enemies can only make one melee attack as they will then fly off behind the player
        //but some enemies like the adult dragon can attack multiple times so we check for that before beging the attack
        if (canAttackMultipleTimes) m_hasAttacked = false;
        if (m_hasAttacked != true) {
            m_hasAttacked = true;

            //the adult dragon dose not use an attack animation so check if we need to be playing on or not
            if (usesAnimation) owner.animat.SetBool("Attack", true);
            return true;
        }

        return false;   
    }

    /// <summary>
    /// 
    /// this is used deal the damage to the player at the appropriate time in the attack animation animation
    /// 
    /// </summary>
    public override void DealDamage() {
        player.TakeDamage();
    }

    /// <summary>
    /// 
    /// this is used to help manage the execution of animations, telling to only play the attack animation once
    /// 
    /// </summary>
    public void Update() {
        if (!usesAnimation) return;
        if (m_hasAttacked == true) {
            owner.animat.SetBool("Attack", false);
        }
    }

    public override void Reset() {
        m_hasAttacked = false;
    }

}

/// <summary>
/// 
/// this script is used for preforming Ranged attacks with the enemies
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
[System.Serializable]
public class RangedAttack : EnemyActions {
    
    /// <summary> this is used to determin the units attack range for ranged attacks </summary> 
    public float attackRange;
    
    /// <summary> this variable dictates how fast the attack projectile will move relative to its owner </summary> 
    public float rangedAttackSpeed;
    
    /// <summary> how quickly the enemy can attack </summary> 
    public float attackCoolDownSpeed;
    /// <summary> timer used to manage when to attack </summary> 
    private float attackTimer = 0;

    /// <summary> how long we want to projectile to live in game for </summary> 
    public float projectileLiveTime;

    /// <summary> used to manage the attack animations </summary> 
    bool flip = false;


    /// <summary>
    /// 
    /// this is used to help manage the execution of animations, and when you can attack
    /// 
    /// </summary>
    public void Update() {
        attackTimer += Time.deltaTime;

        if (flip == true) {
            owner.animat.SetBool("Attack", false);
            flip = false;
        }
    }

    /// <summary>
    /// 
    /// this is used to start the attack of the enemy against the player also beginning the attack animation
    /// 
    /// </summary>
    public override bool AttackPlayer() {
        // check if we can attack and that we are in range to attack
        if (attackTimer > attackCoolDownSpeed) {

            float dist = Vector3.Distance(owner.transform.position, player.transform.position);

            if (dist <= attackRange) {
                owner.animat.SetBool("Attack", true);
                attackTimer = 0;

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// the deal damage for the ranged attack spawns in a projectile, sets it up then lunches it toward the player
    /// 
    /// </summary>
    public override void DealDamage() {
        GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
        go.transform.position = owner.transform.position + (owner.transform.forward * 3);
        go.GetComponent<Projectile>().SetUp(player.transform, damage, rangedAttackSpeed + owner.forwardSpeed, projectileLiveTime, owner.EnemyType == e_EnemyType.Witch ? false : true);
        
        //tell it that it can stop the attack animation now that we have lunched the attack.
        flip = true;
    }
}
