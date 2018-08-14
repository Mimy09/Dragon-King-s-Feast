using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Private Variables ---- //

    private int chunkID = 0;
    PlayerMovement playerMovement;

    //**************************************************************************************/
    // ---- Public Variables ---- //
    [Header("Options")]
    public float spawnDistance = 10;
    public float spawnOffset = 10;

    [Header("Size")]
    [ReadOnly] public float width;
    [ReadOnly] public float height;
    [ReadOnly] public float depth;

    [Header("Active Objects")]
    [ReadOnly] public List<GameObject> enemyList = new List<GameObject>();
    [ReadOnly] public List<GameObject> itemList = new List<GameObject>();


    //**************************************************************************************/
    // ---- Initialization ---- //

    private void Start() {
        playerMovement = GameManager.instance.GetPlayerMovement();

        if (playerMovement != null) {
            transform.position = playerMovement.transform.position;
            width = playerMovement.horizontalBounds + playerMovement.startPos.x;
            height = playerMovement.verticalBounds + playerMovement.startPos.y;
        }
    }

    //**************************************************************************************/
    // ---- Update functions ---- //

    private void Update() {
        if (playerMovement != null)
            depth = spawnDistance + (playerMovement.transform.position.z - playerMovement.startPos.z) / spawnOffset;

        for (; chunkID < depth; chunkID++) {
            SpawnEnemyObject();
            enemyList[enemyList.Count - 1].transform.position +=
                new Vector3(
                    Random.Range(-width, width+1),
                    Random.Range(-height, height + 1),
                    1 * chunkID * spawnOffset);
        }
    }

    //**************************************************************************************/
    // ---- spawn functions ---- //

    void SpawnEnemyObject() {
        enemyList.Add(GameManager.instance.GetObjectPool().FindEnemyOfType((e_EnemyType)Random.Range(1, 5)));
    }

    void SpawnItemObject() {
        itemList.Add(GameManager.instance.GetObjectPool().FindItemOfType((e_ItemType)Random.Range(0, 4)));
    }
}
