using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    void Awake() {
        m_enemyType = e_EnemyType.Ghost;
    }
}
