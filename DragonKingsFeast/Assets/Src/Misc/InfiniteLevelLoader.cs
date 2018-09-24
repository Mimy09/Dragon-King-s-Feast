using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelLoader : MonoBehaviour {

    public GameObject chunk;
    public float chunkLoadDistance = 10;
    public bool shouldLoadChunks = false;
    private List<GameObject> chunkLoads;
    private Vector3 prevPlayerPos;
    private Vector3 currPlayerPos;
    public Transform p_transform;

    private void Start() {
        chunkLoads = new List<GameObject>();
    }
    private void Update() {
        currPlayerPos = GameManager.instance.GetPlayer().transform.position;
        //currPlayerPos = p_transform.position;

        if (currPlayerPos.z < transform.position.z - chunkLoadDistance) return;
        if (currPlayerPos.z - prevPlayerPos.z > chunkLoadDistance * 2) {
            if (chunkLoads.Count != 0) {
                LoadChunk();
            }
            else {
                LoadChunk();
            }
        }


    }

    void LoadChunk() {
        shouldLoadChunks = false;
        prevPlayerPos = GameManager.instance.GetPlayer().transform.position;
        //prevPlayerPos = p_transform.position;
        GameObject c;
        c = Instantiate(chunk, transform.position + new Vector3(0, 0, (chunkLoadDistance * 2) * (chunkLoads.Count + 1)), Quaternion.identity, this.transform);
        chunkLoads.Add(c);
    }

    void UnloadChunk() {

    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position, new Vector3(10, 10, 5));
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawCube(transform.position + new Vector3(0, 0, chunkLoadDistance * 2), new Vector3(10, 10, chunkLoadDistance * 2));
    }
}