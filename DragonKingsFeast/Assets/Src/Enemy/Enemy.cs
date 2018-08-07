using UnityEngine;

public enum e_EnemyType {
    AdultDragon,
    Ghost,
    Phoenix,
    StormCloud,
    Witch,
}

public class Enemy : MonoBehaviour {
    private e_EnemyType m_enemyType;
    public e_EnemyType EnemyType { get { return m_enemyType; } }

    public virtual void Reset() { }
    public virtual void TurnOff() { this.gameObject.SetActive(false); }
    public virtual void TurnOn() { this.gameObject.SetActive(true); }
    public Enemy GetEnemy() { return this; }
}
