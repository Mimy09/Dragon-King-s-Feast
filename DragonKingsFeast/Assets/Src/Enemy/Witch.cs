using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {
    void Awake() {
        m_enemyType = e_EnemyType.Witch;
    }
}
