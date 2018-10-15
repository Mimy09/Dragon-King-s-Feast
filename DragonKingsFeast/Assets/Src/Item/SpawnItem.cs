using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {
    public e_ItemType itemType;
    public int lootValue = 1;
    public Mesh gizmosMeshCoin;
    public Mesh gizmosMeshBoost;
    

    void Start () {
        Item loot = GameManager.instance.GetObjectPool().FindItemOfType(itemType).GetComponent<Item>();
        loot.spawner = true;
        loot.transform.position = transform.position;
        loot.transform.rotation = transform.rotation;
        loot.value = lootValue;

        Destroy(this);
    }
	
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
