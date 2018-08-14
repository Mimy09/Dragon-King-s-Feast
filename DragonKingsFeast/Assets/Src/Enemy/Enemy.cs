using UnityEngine;

public enum e_EnemyType {
    AdultDragon,
    Ghost,
    Phoenix,
    StormCloud,
    Witch,
}

public class Enemy : MonoBehaviour {
    protected e_EnemyType m_enemyType;
    public e_EnemyType EnemyType { get { return m_enemyType; } }

    public Player player;

    public float damage;
    public float baseHealth;
    public float speed;
    public float forwardSpeed;
    public float despawnOffset;

    protected float m_health;
    protected bool m_hasAttacked;

    public virtual void Reset() { m_hasAttacked = false; m_health = baseHealth; }
    public virtual void TurnOff() { GameManager.instance.GetObjectPool().AddEnemyTooPool(this); GameManager.instance.GetItemSpawnManager().enemyList.RemoveAt(0); }
    public virtual void TurnOn() { Reset(); this.gameObject.SetActive(true); }
    public virtual void OnDeath() { }
    public virtual void TakeDamage(float damage) { m_health -= damage; if (m_health < 0) { OnDeath(); TurnOff(); } }
    public Enemy GetEnemy() { return this; }
}
