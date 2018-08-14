//**************************************************************************************/
// ---- Includes ---- //
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Variables ---- //

    List<Item> objectPool_item = new List<Item>();
    List<Enemy> objectPool_enemy = new List<Enemy>();

    //**************************************************************************************/
    // ---- Public functions ---- //

    public void Reset() {
        for (int i = 0; i < objectPool_item.Count; i++) {
            objectPool_item[i].Reset();
        }
        for (int i = 0; i < objectPool_enemy.Count; i++) {
            objectPool_enemy[i].Reset();
        }
    }

    //**************************************************************************************/
    // ---- enemy functions ---- //

    public void AddEnemyTooPool(Enemy obj) {
        obj.gameObject.SetActive(false);
        objectPool_enemy.Add(obj);
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
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Ghost:
                go = Instantiate(Helper.EnemyPath_Ghost) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Phoenix:
                go = Instantiate(Helper.EnemyPath_Phoenix) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.StormCloud:
                go = Instantiate(Helper.EnemyPath_StormCloud) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Witch:
                go = Instantiate(Helper.EnemyPath_Witch) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            default:
                Debug.LogError("FindEnemyOfType - Returned null");
                return null;
        }
    }

    //**************************************************************************************/
    // ---- item functions ---- //

    public void AddItemTooPool(Item obj) {
        obj.gameObject.SetActive(false);
        objectPool_item.Add(obj);
    }

    public GameObject FindItemOfType(e_ItemType type) {
        GameObject go;

        for (int i = 0; i < objectPool_item.Count; i++) {
            if (objectPool_item[i].ItemType == type) {
                objectPool_item[i].TurnOn();
                go = objectPool_item[i].gameObject;
                objectPool_item.RemoveAt(i);
                return go;
            }
        }

        switch (type) {
            case e_ItemType.Boost_Attack:
                go = Instantiate(Helper.ItemPath_Boost_attack) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Boost_Speed:
                go = Instantiate(Helper.ItemPath_Boost_speed) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Boost_Defense:
                go = Instantiate(Helper.ItemPath_Boost_defense) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Pickup:
                go = Instantiate(Helper.ItemPath_pickUp) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            default:
                Debug.LogError("FindEnemyOfType - Returned null");
                return null;
        }
    }

}
