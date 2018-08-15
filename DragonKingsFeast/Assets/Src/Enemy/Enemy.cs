﻿using UnityEngine;

public enum e_EnemyType {
    AdultDragon,
    Ghost,
    StormCloud,
    Witch,
    Phoenix,
}

public class Enemy : MonoBehaviour {
    //this is used to determin what type of enemy it is
    protected e_EnemyType m_enemyType;
    public e_EnemyType EnemyType { get { return m_enemyType; } }

    //this is a refrence to the player character, used for attacks
    public Player player;

    //this is how much damage the enemy dose if it hits the player
    public float damage;
    //this is used to determin the units attack range for ranged attacks
    public float attackRange;

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
    //determins if it has made a melee attack or not
    protected bool m_hasAttacked;

    public virtual void Reset() { m_hasAttacked = false; m_health = baseHealth; }
    public virtual void TurnOff() {
        GameManager.instance.GetObjectPool().AddEnemyTooPool(this);
        GameManager.instance.GetItemSpawnManager().enemyList.RemoveAt(0);
        GameManager.instance.GetItemSpawnManager().SpawnEnemy();
    }
    public virtual void TurnOn() { Reset(); this.gameObject.SetActive(true); }
    public virtual void OnDeath() { }
    public virtual void TakeDamage(float damage) { m_health -= damage; if (m_health < 0) { OnDeath(); TurnOff(); } }
    public Enemy GetEnemy() { return this; }
}
