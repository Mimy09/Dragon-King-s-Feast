using UnityEngine;

/*
* Enum: e_EnemyType
* -----------------
*
* this is used for the inhearited classes to help determin
* what the enemy is ment to be
*
*/
public enum e_EnemyType {
    AdultDragon,
    Ghost,
    StormCloud,
    Witch,
    Phoenix,
}

/*
* Class: Enemy
* ------------
*
* this is the base class of all the enemies. it handles their on off functions 
* used to help manage them with the object pool to reset them and turn them off.
* it also manages variables like health and what type of enemey it is using the e_EnemyType enum
*
* Author: Callum Dunstone & Mitchell Jenkins 
*/
public class Enemy : MonoBehaviour {
    
    [Header("Type")]
    //this is used to determin what type of enemy it is
    protected e_EnemyType m_enemyType;
    public e_EnemyType EnemyType { get { return m_enemyType; } }

    [Header("Player")]
    //this is a reference to the player character, used for attacks
    public Player player;
    
    [Header("Base Stats")]
    //this is the units base health that it will be set at when reset
    public float baseHealth;
    //this is how fast the unit moves through the scene
    public float speed;
    //this is how fast the unit moves forward 
    public float forwardSpeed;
    //this determines how far behind the player it must be before being turned off
    public float despawnOffset;
    //the units current health   
    protected float m_health;
    //the time of the death animation
    public float deathTime;
    
    //private float m_timeSinceDead;

    //this is used to determin if projectiles can hit this enemy or not
    public bool isDead;
    
    //this is the death effect that the enemie plays when the die
    public GameObject deathEffect;

    //this is the value of the loot the enemie will drop when it is killed
    public int lootValue;

    //this is the animator used to animate the enemy this is needed so we can tell it when to play an attack animation
    [Header("Animation")]
    public Animator animat;

    //this is the spawner that the enemie was spawned from
    [Header("Flocking")]
    public Spawner spawner;
    
    //this is used to get the value of the enemies health 
    public float GetHealth() { return m_health; }

    /*
    * Function: Reset
    * ---------------
    *
    * this is used to reset the enemies health when they are reused by the object poll 
    *
    */
    public virtual void Reset() { m_health = baseHealth; }

    /*
    * Function: TurnOff
    * -----------------
    *
    * this is called when the enemy is being deactivate to be put in the object pool
    *
    */
    public virtual void TurnOff() {
        Reset();
        GameManager.objectPoolManager.AddEnemyTooPool(this);
        GameManager.enemyList.Remove(gameObject);
    }

    /*
    * Function: TurnOn
    * ----------------
    *
    * this is called when we turn on the enemy from the object pool
    *
    */
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

    /*
    * Function: OnDeath
    * -----------------
    *
    * this function is called when the enemy is declared dead spawning in its loot
    * and death partical effects
    *
    */
    public virtual void OnDeath() {
        //spawn in the loot at the the enmies current position and give it the proper loot value
        Item loot = GameManager.objectPoolManager.FindItemOfType(e_ItemType.Pickup).GetComponent<Item>();
        loot.transform.position = transform.position;
        loot.value = lootValue;

        //spawn the death partical effect and give it a short timer before destroying and removing it
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity, transform), 5.0f);
    }

    /*
    * Function: TakeDamage
    * --------------------
    *
    * this function is used when a projectile hits the enemy and does no real damage to it
    * this function is more used to determin if the enemy should start its death sequence or not
    *
    */
    public virtual void TakeDamage() {
        //check if its actually dead or not yet
        if (m_health <= 0) {
            //iniate the death sequnce
            OnDeath();
            animat.SetBool("Dead", true);
            //declare it dead so other projectiles dont hit this enemy
            isDead = true;
        }
    }

    /*
    * Function: TakeDamage2
    * ---------------------
    *
    * this function is called by the player after they have declared an attack against the enemy 
    * so that attacks happen instantly dealing damage, this is important for the auto targeting so
    * the player script can decied wich enemy to attack however the enemy wont go into its death sequance until after the 
    * projectile has hit it.
    *
    * Parameters @1: float damage- this is the amount of damage that gets dealt to the enemy 
    */
    public virtual void TakeDamage2(float damage) { m_health -= damage; }    
}
