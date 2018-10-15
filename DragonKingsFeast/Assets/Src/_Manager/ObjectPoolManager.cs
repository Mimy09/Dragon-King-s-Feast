//**************************************************************************************/
// ---- Includes ---- //
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Variables ---- //

    [ReadOnly] public List<Item>        objectPool_item =               new List<Item>();
    [ReadOnly] public List<Enemy>       objectPool_enemy =              new List<Enemy>();
    [ReadOnly] public List<Projectile>  objectPool_Projectile =         new List<Projectile>();

    private GameObject ObjectPool;
    private GameObject ItemParent;
    private GameObject EnemyParent;
    private GameObject ProjectileParent;

    //**************************************************************************************/
    // ---- Public functions ---- //

    private void Start() {
        if (ObjectPool == null) Init();
    }

    private void Init() {
        ObjectPool =        new GameObject("Object Pool");
        ItemParent =        new GameObject("Item");
        EnemyParent =       new GameObject("Enemy");
        ProjectileParent =  new GameObject("Projectile");

        ItemParent.transform.parent         = ObjectPool.transform;
        EnemyParent.transform.parent        = ObjectPool.transform;
        ProjectileParent.transform.parent   = ObjectPool.transform;
    }

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

        for (int i = 0; i < objectPool_enemy.Count; i++)
            if (objectPool_enemy[i] == obj) return;

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

        if (EnemyParent == null)
            Init();

        switch (type) {
            case e_EnemyType.AdultDragon:
                go = Instantiate(Helper.EnemyPath_AdultDragon, EnemyParent.transform) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Ghost:
                go = Instantiate(Helper.EnemyPath_Ghost, EnemyParent.transform) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Phoenix:
                go = Instantiate(Helper.EnemyPath_Phoenix, EnemyParent.transform) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.StormCloud:
                go = Instantiate(Helper.EnemyPath_StormCloud, EnemyParent.transform) as GameObject;
                go.GetComponent<Enemy>().TurnOn();
                return go;
            case e_EnemyType.Witch:
                go = Instantiate(Helper.EnemyPath_Witch, EnemyParent.transform) as GameObject;
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

        for (int i = 0; i < objectPool_item.Count; i++)
            if (objectPool_item[i] == obj) return;

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

        if (ItemParent == null)
            Init();

        switch (type) {
            case e_ItemType.Boost_Attack:
                go = Instantiate(Helper.ItemPath_Boost_attack, ItemParent.transform) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Boost_Speed:
                go = Instantiate(Helper.ItemPath_Boost_speed, ItemParent.transform) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Boost_Defense:
                go = Instantiate(Helper.ItemPath_Boost_defense, ItemParent.transform) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            case e_ItemType.Pickup:
                go = Instantiate(Helper.ItemPath_pickUp, ItemParent.transform) as GameObject;
                go.GetComponent<Item>().TurnOn();
                return go;
            default:
                Debug.LogError("FindEnemyOfType - Returned null");
                return null;
        }
    }


    //**************************************************************************************/
    // ---- Projectile functions ---- //

    public void AddProjectileToPool(Projectile obj) {
        obj.gameObject.SetActive(false);
        objectPool_Projectile.Add(obj);
    }

    public GameObject FindProjectile() {
        GameObject go;

        if (objectPool_Projectile.Count > 0) {
            go = objectPool_Projectile[0].gameObject;
            objectPool_Projectile[0].TurnOn();
            objectPool_Projectile.RemoveAt(0);
            return go;
        }

        go = Instantiate(Helper.ProjectilePath, ProjectileParent.transform) as GameObject;
        go.GetComponent<Projectile>().TurnOn();
        return go;
    }

}
