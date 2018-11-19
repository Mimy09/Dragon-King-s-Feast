using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Creates an infinite level by continually creating more environment
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class InfiniteLevelLoader : MonoBehaviour {

    /// <summary>
    /// The chunk to continually create
    /// </summary>
    public GameObject chunk;

    /// <summary>
    /// Distance until next chunk is loaded
    /// </summary>
    public float chunkLoadDistance = 10;

    /// <summary>
    /// Should the next chunk load
    /// </summary>
    public bool shouldLoadChunks = false;

    /// <summary>
    /// List of all the loaded chunks
    /// </summary>
    private List<GameObject> chunkLoads;

    /// <summary>
    /// Players position last time a chunk loaded 
    /// </summary>
    private Vector3 prevPlayerPos;

    /// <summary>
    /// Current player position
    /// </summary>
    private Vector3 currPlayerPos;

    /// <summary>
    /// Has the player reached the end of the level
    /// </summary>
    private bool endScene = false;

    /// <summary>
    /// Sets up the chunks
    /// </summary>
    private void Start() {
        chunkLoads = new List<GameObject>();
    }

    /// <summary>
    /// Updates the chunks and loads them in when required
    /// </summary>
    private void Update() {
        currPlayerPos = GameManager.instance.GetPlayer().transform.position;

        if (currPlayerPos.z < transform.position.z - chunkLoadDistance) return;
        if (!endScene) {
            endScene = true;
        }

        if (currPlayerPos.z - prevPlayerPos.z > chunkLoadDistance * 2) {
            if (chunkLoads.Count != 0) {
                LoadChunk();
            }
            else {
                LoadChunk();
            }
        }
    }

    /// <summary>
    /// Loads in the next chunk
    /// </summary>
    void LoadChunk() {
        shouldLoadChunks = false;
        prevPlayerPos = GameManager.instance.GetPlayer().transform.position;
        //prevPlayerPos = p_transform.position;
        GameObject c;
        c = Instantiate(chunk, transform.position + new Vector3(0, 0, (chunkLoadDistance * 2) * (chunkLoads.Count + 1)), Quaternion.identity, this.transform);
        chunkLoads.Add(c);
    }

    /// <summary>
    /// Draws Gizmos for chunk loading
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position, new Vector3(10, 10, 5));
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawCube(transform.position + new Vector3(0, 0, chunkLoadDistance * 2), new Vector3(10, 10, chunkLoadDistance * 2));
    }
}