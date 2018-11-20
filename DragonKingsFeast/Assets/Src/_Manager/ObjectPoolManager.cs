//**************************************************************************************/
// ---- Includes ---- //
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Stores objects in a object pool for use later on
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// </summary>
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

    /// <summary>
    /// calls the initialization for the object pool
    /// </summary>
    private void Start() {
        if (ObjectPool == null) Init();
    }

    /// <summary>
    /// Initialize the object pool
    /// </summary>
    private void Init() {
        ObjectPool =        new GameObject("Object Pool");
        ItemParent =        new GameObject("Item");
        EnemyParent =       new GameObject("Enemy");
        ProjectileParent =  new GameObject("Projectile");

        ItemParent.transform.parent         = ObjectPool.transform;
        EnemyParent.transform.parent        = ObjectPool.transform;
        ProjectileParent.transform.parent   = ObjectPool.transform;
    }

    /// <summary>
    /// Resets the object pool
    /// </summary>
    public void Reset() {
        for (int i = 0; i < objectPool_enemy.Count; i++) {
            objectPool_enemy[i].Reset();
        }
    }

    //**************************************************************************************/
    // ---- enemy functions ---- //

    /// <summary>
    /// Adds an enemy to the object pool and disables it
    /// </summary>
    /// <param name="obj">Enemy to add</param>
    public void AddEnemyTooPool(Enemy obj) {
        obj.gameObject.SetActive(false);

        for (int i = 0; i < objectPool_enemy.Count; i++)
            if (objectPool_enemy[i] == obj) return;

        objectPool_enemy.Add(obj);
    }

    /// <summary>
    /// Returns a enemy from the object pool if one is available.
    /// If not, create a new enemy
    /// </summary>
    /// <param name="type">enemy type to find</param>
    /// <returns></returns>
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

    /// <summary>
    /// Adds an item to the object pool and disables it
    /// </summary>
    /// <param name="obj">Item to add</param>
    public void AddItemTooPool(Item obj) {
        obj.gameObject.SetActive(false);

        for (int i = 0; i < objectPool_item.Count; i++)
            if (objectPool_item[i] == obj) return;

        objectPool_item.Add(obj);
    }

    /// <summary>
    /// Returns a item from the object pool if one is available.
    /// If not, create a new item
    /// </summary>
    /// <param name="type">item type to find</param>
    /// <returns></returns>
    public GameObject FindItemOfType(e_ItemType type) {
        GameObject go;

        for (int i = 0; i < objectPool_item.Count; i++) {
            if (objectPool_item[i].itemType == type) {
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

    /// <summary>
    /// Adds a projectile to the object pool and disables it
    /// </summary>
    /// <param name="obj">Projectile to add</param>
    public void AddProjectileToPool(Projectile obj) {
        obj.gameObject.SetActive(false);
        objectPool_Projectile.Add(obj);
    }

    /// <summary>
    /// Returns a projectile from the object pool if one is available.
    /// If not, create a new projectile
    /// </summary>
    /// <returns></returns>
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
