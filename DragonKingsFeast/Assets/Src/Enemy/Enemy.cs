using UnityEngine;

/// <summary>
/// 
/// this is used for the inherited classes to help determine
/// what the enemy is meant to be what
/// 
/// </summary>
public enum e_EnemyType {
    AdultDragon,
    Ghost,
    StormCloud,
    Witch,
    Phoenix,
}

/// <summary>
/// 
/// this is the base class of all the enemies. it handles their on off functions 
/// used to help manage them with the object pool to reset them and turn them off.
/// it also manages variables like health and what type of enemy it is using the e_EnemyType enum
/// 
/// <para>Author: Callum Dunstone & Mitchell Jenkins</para>
/// 
/// </summary>
public class Enemy : MonoBehaviour {
    
    [Header("Type")]
    /// <summary> this is used to determine what type of enemy it is </summary> 
    protected e_EnemyType m_enemyType;
    public e_EnemyType EnemyType { get { return m_enemyType; } }

    [Header("Player")]
    /// <summary> this is a reference to the player character, used for attacks </summary>
    public Player player;
    
    [Header("Base Stats")]
    /// <summary> this is the units base health that it will be set at when reset </summary>
    public float baseHealth;
    /// <summary> this is how fast the unit moves through the scene </summary>
    public float speed;
    /// <summary> this is how fast the unit moves forward </summary>
    public float forwardSpeed;
    /// <summary> this determines how far behind the player it must be before being turned off </summary>
    public float despawnOffset;
    /// <summary> the units current health </summary>
    protected float m_health;
    /// <summary> the time of the death animation </summary>
    public float deathTime;
    
    /// <summary> this is used to determine if projectiles can hit this enemy or not </summary>
    public bool isDead;
    
    /// <summary> this is the death effect that the enemy plays when the die </summary>
    public GameObject deathEffect;
    
    /// <summary> this is the value of the loot the enemy will drop when it is killed </summary>
    public int lootValue;
    
    /// <summary> this is the animator used to animate the enemy this is needed so we can tell it when to play an attack animation </summary>
    [Header("Animation")]
    public Animator animat;
    
    /// <summary> this is the spawner that the enemy was spawned from </summary>
    [Header("Flocking")]
    public Spawner spawner;
    
    /// <summary> this is used to get the value of the enemies health </summary>
    public float GetHealth() { return m_health; }
    
    /// <summary>
    /// 
    /// this is used to reset the enemies health when they are reused by the object poll 
    /// 
    /// </summary>
    public virtual void Reset() { m_health = baseHealth; }
    
    /// <summary>
    /// 
    /// this is called when the enemy is being deactivate to be put in the object pool
    /// 
    /// </summary>
    public virtual void TurnOff() {
        Reset();
        GameManager.objectPoolManager.AddEnemyTooPool(this);
        GameManager.enemyList.Remove(gameObject);
    }
    
    /// <summary>
    /// 
    /// this is called when we turn on the enemy from the object pool
    /// 
    /// </summary>
    public virtual void TurnOn() {
        //re set its health
        Reset();
        //add it to the list of active enemies
        GameManager.enemyList.Add(gameObject);
        //turn the game object back on
        this.gameObject.SetActive(true);
        //make sure it is not playing its death animation
        animat.SetBool("Dead", false);
        //confirm it is not dead anymore so projectiles can hit it again
        isDead = false;
    }
    
    /// <summary>
    /// 
    /// this function is called when the enemy is declared dead spawning in its loot
    /// and death partical effects
    /// 
    /// </summary>
    public virtual void OnDeath() {
        //spawn in the loot at the the enemies current position and give it the proper loot value
        Item loot = GameManager.objectPoolManager.FindItemOfType(e_ItemType.Pickup).GetComponent<Item>();
        loot.transform.position = transform.position;
        loot.value = lootValue;

        //spawn the death partical effect and give it a short timer before destroying and removing it
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity, transform), 5.0f);
    }
    
    /// <summary>
    /// 
    /// this function is used when a projectile hits the enemy and does no real damage to it
    /// this function is more used to determine if the enemy should start its death sequence or not
    /// 
    /// </summary>
    public virtual void TakeDamage() {
        //check if its actually dead or not yet
        if (m_health <= 0) {
            //iniate the death sequence
            OnDeath();
            animat.SetBool("Dead", true);
            //declare it dead so other projectiles don't hit this enemy
            isDead = true;
        }
    }

    /// <summary>
    /// 
    /// this function is called by the player after they have declared an attack against the enemy 
    /// <para>so that attacks happen instantly dealing damage, this is important for the auto targeting so</para>
    /// the player script can deiced which enemy to attack however the enemy wont go into its death sequence until after the
    /// projectile has hit it.
    /// 
    /// </summary>
    public virtual void TakeDamage2(float damage) { m_health -= damage; }    
}
