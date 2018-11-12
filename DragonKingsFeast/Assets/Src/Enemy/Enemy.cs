using UnityEngine;

public enum e_EnemyType {
    AdultDragon,
    Ghost,
    StormCloud,
    Witch,
    Phoenix,
}

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
    private float m_timeSinceDead;
    public bool isDead;
    

    public GameObject deathEffect;

    public int lootValue;

    [Header("Animation")]
    public Animator animat;

    [Header("Flocking")]
    public Spawner spawner;
    
    public float GetHealth() { return m_health; }

    public virtual void Reset() { m_health = baseHealth; }
    public virtual void TurnOff() {
        Reset();
        GameManager.objectPoolManager.AddEnemyTooPool(this);
        GameManager.enemyList.Remove(gameObject);
    }
    public virtual void TurnOn() {
        Reset();
        GameManager.enemyList.Add(gameObject);
        this.gameObject.SetActive(true);
        animat.SetBool("Dead", false);
        isDead = false;
        m_timeSinceDead = 0;
    }
    public virtual void OnDeath() {
        Item loot = GameManager.objectPoolManager.FindItemOfType(e_ItemType.Pickup).GetComponent<Item>();
        loot.transform.position = transform.position;
        loot.value = lootValue;

        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity, transform), 2.0f);
    }

    public virtual void TakeDamage(float damage) { if (m_health <= 0) { OnDeath(); animat.SetBool("Dead", true); isDead = true; } }
    public virtual void TakeDamage2(float damage) { m_health -= damage; }

    public Enemy GetEnemy() { return this; }

    protected virtual void Update() {

    }


    protected virtual void Awake() {
        
    }

    public void ColliderUpdate() {
        
    }
}
