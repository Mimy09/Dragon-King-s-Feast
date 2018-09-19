using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Private Variables ---- //

    private int chunkID = 0;
    private int chunkID_Item = 0;
    PlayerMovement playerMovement;
    float dist = 0;

    //**************************************************************************************/
    // ---- Public Variables ---- //
    [Header("Options")]
    public float spawnDistance = 10;
    public float spawnOffset = 10;

    [Header("Item Spawn Rate")]
    [LabelOverride("Coin")] [SerializeField] public float CoinSpawnRate = 100;
    [LabelOverride("Attack")] [SerializeField] public float AttackSpawnRate = 100;
    [LabelOverride("Defense")] [SerializeField] public float DefenseSpawnRate = 100;
    [LabelOverride("Speed")] [SerializeField] public float SpeedSpawnRate = 100;


    [Header("Level 1 Spawn Rate")]
    [LabelOverride("Ghost")] [SerializeField] public float lvl_1_GhostSpawnRate = 100;
    [LabelOverride("Witch")] [SerializeField] public float lvl_1_WitchSpawnRate = 100;
    [LabelOverride("Storm")] [SerializeField] public float lvl_1_StormSpawnRate = 100;
    [LabelOverride("Phoenix")] [SerializeField] public float lvl_1_PhoenixSpawnRate = 100;

    [Header("Level 2 Spawn Rate")]
    [LabelOverride("Ghost")] [SerializeField] public float lvl_2_GhostSpawnRate = 100;
    [LabelOverride("Witch")] [SerializeField] public float lvl_2_WitchSpawnRate = 100;
    [LabelOverride("Storm")] [SerializeField] public float lvl_2_StormSpawnRate = 100;
    [LabelOverride("Phoenix")] [SerializeField] public float lvl_2_PhoenixSpawnRate = 100;

    [Header("Level 3 Spawn Rate")]
    [LabelOverride("Ghost")] [SerializeField] public float lvl_3_GhostSpawnRate = 100;
    [LabelOverride("Witch")] [SerializeField]public float lvl_3_WitchSpawnRate = 100;
    [LabelOverride("Storm")] [SerializeField]public float lvl_3_StormSpawnRate = 100;
    [LabelOverride("Phoenix")] [SerializeField] public float lvl_3_PhoenixSpawnRate = 100;

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
        //if (playerMovement != null)
        //    depth = (playerMovement.transform.position.z - playerMovement.startPos.z) / spawnDistance;
        
        for (; chunkID < spawnDistance + depth; chunkID++) {
            SpawnEnemyObject();

            if (chunkID != 0) {
                dist = (chunkID - 1) * spawnOffset - enemyList[enemyList.Count - 2].transform.position.z;
            }
            enemyList[enemyList.Count - 1].transform.position =
                new Vector3(
                    Random.Range(-width, width + 1),
                    Random.Range(-height, height + 1),
                    chunkID * spawnOffset - dist);
        }

        for (; chunkID_Item < spawnDistance + depth; chunkID_Item++) {
            SpawnItemObject();

            if (chunkID_Item != 0) {
                dist = (chunkID_Item - 1) * spawnOffset - itemList[itemList.Count - 2].transform.position.z;
            }
            itemList[itemList.Count - 1].transform.position =
                new Vector3(
                    Random.Range(-width, width + 1),
                    Random.Range(-height, height + 1),
                    chunkID_Item * spawnOffset - dist);

        }
    }

    //**************************************************************************************/
    // ---- spawn functions ---- //

    void SpawnEnemyObject() {
        float ghostspawn, witchspawn, stormspawn, phoenixspawn;
        int currlevel = 0;
        if (GameManager.instance.GetMapManager().isLoaded_level1) currlevel = 1;
        else if (GameManager.instance.GetMapManager().isLoaded_level2) currlevel = 2;
        else if (GameManager.instance.GetMapManager().isLoaded_level3) currlevel = 3;


        switch (currlevel) {
            case 1:
                ghostspawn = Random.Range(0, lvl_1_GhostSpawnRate);
                witchspawn = Random.Range(0, lvl_1_WitchSpawnRate);
                stormspawn = Random.Range(0, lvl_1_StormSpawnRate);
                phoenixspawn = Random.Range(0, lvl_1_PhoenixSpawnRate);
                break;
            case 2:
                ghostspawn = Random.Range(0, lvl_2_GhostSpawnRate);
                witchspawn = Random.Range(0, lvl_2_WitchSpawnRate);
                stormspawn = Random.Range(0, lvl_2_StormSpawnRate);
                phoenixspawn = Random.Range(0, lvl_2_PhoenixSpawnRate);
                break;
            case 3:
                ghostspawn = Random.Range(0, lvl_3_GhostSpawnRate);
                witchspawn = Random.Range(0, lvl_3_WitchSpawnRate);
                stormspawn = Random.Range(0, lvl_3_StormSpawnRate);
                phoenixspawn = Random.Range(0, lvl_3_PhoenixSpawnRate);
                break;
            default:
                ghostspawn = witchspawn = stormspawn = phoenixspawn = 0;
                break;
        }

        float max = Mathf.Max(ghostspawn, witchspawn);
        max = Mathf.Max(max, stormspawn);
        max = Mathf.Max(max, phoenixspawn);

        int i = 0;
        if (max == ghostspawn) i = 1;
        else if (max == witchspawn) i = 3;
        else if (max == stormspawn) i = 2;
        else if (max == phoenixspawn) i = 4;


        enemyList.Add(GameManager.instance.GetObjectPool().FindEnemyOfType((e_EnemyType)i));
    }

    void SpawnItemObject() {
        float coin, speed, defence, attack;
        coin = Random.Range(0, CoinSpawnRate);
        speed = Random.Range(0, SpeedSpawnRate);
        defence = Random.Range(0, DefenseSpawnRate);
        attack = Random.Range(0, AttackSpawnRate);

        float max = Mathf.Max(coin, speed);
        max = Mathf.Max(max, defence);
        max = Mathf.Max(max, attack);

        int i = 0;
        if (max == coin) i = 3;
        else if (max == speed) i = 0;
        else if (max == defence) i = 2;
        else if (max == attack) i = 1;

        itemList.Add(GameManager.instance.GetObjectPool().FindItemOfType((e_ItemType)i));
    }

    public void SpawnEnemy() {
        depth++;
    }
}
