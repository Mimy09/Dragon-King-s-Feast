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
    //this is a refrence to the player character, used for attacks
    public Player player;
    
    [Header("Base Stats")]
    //this is the units base health that it will be set at when reset
    public float baseHealth;
    //this is how fast the unit moves through the scene
    public float speed;
    //this is how fast the unit moves forward 
    public float forwardSpeed;
    //this determins how far behind the player it must be before being turned off
    public float despawnOffset;
    //the units current health   
    protected float m_health;

    public int lootValue;

    [Header("Animation")]
    public Animator animat;

    public virtual void Reset() { m_health = baseHealth; }
    public virtual void TurnOff() {
        GameManager.instance.GetObjectPool().AddEnemyTooPool(this);
        GameManager.instance.GetItemSpawnManager().enemyList.RemoveAt(0);
        GameManager.instance.GetItemSpawnManager().SpawnEnemy();
    }
    public virtual void TurnOn() { Reset(); this.gameObject.SetActive(true); }
    public virtual void OnDeath() {
        Item loot = GameManager.objectPoolManager.FindItemOfType(e_ItemType.Pickup).GetComponent<Item>();
        loot.transform.position = transform.position;
        loot.value = lootValue;
        loot.TurnOn();
    }
    public virtual void TakeDamage(float damage) { m_health -= damage; if (m_health <= 0) { OnDeath(); TurnOff(); } }
    public Enemy GetEnemy() { return this; }

    protected virtual void Awake() {
        animat = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    public void ColliderUpdate() {
        
    }
}
