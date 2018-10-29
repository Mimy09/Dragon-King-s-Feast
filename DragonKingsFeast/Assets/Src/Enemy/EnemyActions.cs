using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyActions{

    //this is how much damage the enemy dose if it hits the player
    public float damage;
    public Player player;
    protected Enemy owner;

    public EnemyActions() {}
    public void SetUp(Player p, Enemy e) { player = p; owner = e; }

    public virtual bool AttackPlayer() { return false; }
    public virtual void DealDamage() { }
    public virtual void Reset() { }
}

[System.Serializable]
public class MeleeAttack : EnemyActions {
    //determins if it has made a melee attack or not
    protected bool m_hasAttacked;
    
    public override bool AttackPlayer() {
        if (m_hasAttacked != true) {
            m_hasAttacked = true;
            owner.animat.SetBool("Attack", true);
            return true;
        }

        return false;   
    }

    public override void DealDamage() {
        player.TakeDamage();
    }

    public void Update() {
        if (m_hasAttacked == true) {
            owner.animat.SetBool("Attack", false);
        }
    }

}

[System.Serializable]
public class RangedAttack : EnemyActions {

    //this is used to determin the units attack range for ranged attacks
    public float attackRange;

    //this variable dictates how fast the witchs attack projectile will move relative to it
    public float rangedAttackSpeed;

    //how quickly the witch can attack
    public float attackCoolDownSpeed;
    private float attackTimer = 0;

    public float projectileLiveTime;

    bool flip = false;
    
    public void Update() {
        attackTimer += Time.deltaTime;

        if (flip == true) {
            owner.animat.SetBool("Attack", false);
            flip = false;
        }
    }

    public override bool AttackPlayer() {
        float dist = Vector3.Distance(owner.transform.position, player.transform.position);

        if (attackTimer > attackCoolDownSpeed) {
            if (dist <= attackRange) {
                owner.animat.SetBool("Attack", true);
                attackTimer = 0;

                return true;
            }
        }

        return false;
    }

    public override void DealDamage() {
        GameObject go = GameManager.instance.GetObjectPool().FindProjectile();
        go.transform.position = owner.transform.position + (owner.transform.forward * 3);
        go.GetComponent<Projectile>().SetUp(player.transform, damage, rangedAttackSpeed + owner.forwardSpeed, projectileLiveTime, owner.EnemyType == e_EnemyType.Witch ? false : true);
        
        flip = true;
    }
}
