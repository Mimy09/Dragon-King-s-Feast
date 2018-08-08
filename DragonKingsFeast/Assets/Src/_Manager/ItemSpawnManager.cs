using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour {

    GameObject obj;

    private void Start() {
        obj = GameManager.instance.GetObjectPool().FindEnemyOfType(e_EnemyType.Ghost);

        obj.GetComponent<Enemy>().TurnOff();

        obj = GameManager.instance.GetObjectPool().FindEnemyOfType(e_EnemyType.Ghost);
    }

}
