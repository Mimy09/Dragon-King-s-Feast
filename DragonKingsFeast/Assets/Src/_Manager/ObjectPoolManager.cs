using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    List<Item> objectPool_item = new List<Item>();
    List<Enemy> objectPool_enemy = new List<Enemy>();

    public void Reset() {
        for (int i = 0; i < objectPool_item.Count; i++) {
            objectPool_item[i].Reset();
        }
        for (int i = 0; i < objectPool_enemy.Count; i++) {
            objectPool_enemy[i].Reset();
        }
    }

    public void AddEnemyTooPool(Enemy obj) {
        obj.gameObject.SetActive(false);
        objectPool_enemy.Add(obj);
    }

    public void AddItemTooPool(Item obj) {
        obj.gameObject.SetActive(false);
        objectPool_item.Add(obj);
    }

    public GameObject FindEnemyOfType(e_EnemyType type) {
        GameObject go;

        for (int i = 0; i < objectPool_enemy.Count; i++) {
            if (objectPool_enemy[i].EnemyType == type) {
                objectPool_enemy[i].TurnOn();
                go = objectPool_enemy[i].gameObject;
                objectPool_enemy.RemoveAt(i);
                return go;
            }
        }

        switch (type) {
            case e_EnemyType.AdultDragon:
                go = Instantiate(Helper.EnemyPath_AdultDragon) as GameObject;
                return go;
            case e_EnemyType.Ghost:
                go = Instantiate(Helper.EnemyPath_Ghost) as GameObject;
                return go;
            case e_EnemyType.Phoenix:
                go = Instantiate(Helper.EnemyPath_Phoenix) as GameObject;
                return go;
            case e_EnemyType.StormCloud:
                go = Instantiate(Helper.EnemyPath_StormCloud) as GameObject;
                return go;
            case e_EnemyType.Witch:
                go = Instantiate(Helper.EnemyPath_Witch) as GameObject;
                return go;
            default:
                Debug.LogError("FindEnemyOfType - Returned null");
                return null;
        }
    }

    
}
