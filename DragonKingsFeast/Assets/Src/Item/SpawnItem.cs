using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this is used to spawn the items into the game as because of how they are 
/// set up they cant just be placed into the scene and need to be instantiated 
/// 
/// <para>Author: Mitchell Jenkins </para>
/// 
/// </summary>
public class SpawnItem : MonoBehaviour {
    /// <summary> the type of item that we will spawn </summary> 
    public e_ItemType itemType;
    /// <summary> if it is a coin what is its value </summary> 
    public int lootValue = 1;
    /// <summary> reference to the coin mesh so that in editor we can see what we are planing to spawn visually </summary> 
    public Mesh gizmosMeshCoin;
    /// <summary> reference to the boost mesh so that in editor we can see what we are planing to spawn visually </summary> 
    public Mesh gizmosMeshBoost;

    /// <summary>
    /// 
    /// when the game starts spawn in the item at the position of this gameObject
    /// then we remove our self
    /// 
    /// </summary>
    void Start () {
        Item loot = GameManager.instance.GetObjectPool().FindItemOfType(itemType).GetComponent<Item>();
        loot.spawner = true;
        loot.transform.position = transform.position;
        loot.transform.rotation = transform.rotation;
        loot.value = lootValue;

        Destroy(this);
    }

    /// <summary>
    /// 
    /// draw a gizmo of what type of item we plan to spawn into the game in the editor 
    /// 
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        if (itemType == e_ItemType.Pickup)
            Gizmos.DrawMesh(gizmosMeshCoin, transform.position, transform.rotation, new Vector3(0.2f, 0.5f, 0.5f));
        else {
            if(itemType == e_ItemType.Boost_Attack) Gizmos.color = Color.red;
            else if (itemType == e_ItemType.Boost_Defense) Gizmos.color = Color.green;
            else if (itemType == e_ItemType.Boost_Speed) Gizmos.color = Color.blue;
            Gizmos.DrawMesh(gizmosMeshBoost, transform.position, Quaternion.identity, new Vector3(1, 1, 1));
        }
    }
}
