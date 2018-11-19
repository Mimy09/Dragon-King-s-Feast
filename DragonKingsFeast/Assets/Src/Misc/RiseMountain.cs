using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Rises the mountain up the more the player moves forward
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class RiseMountain : MonoBehaviour {

    /// <summary>
    /// reference to the players transform
    /// </summary>
    private Transform player;

    /// <summary>
    /// start z position of the player
    /// </summary>
    private float startZ;

    /// <summary>
    /// Speed of the mountain rising up
    /// </summary>
    public float speed;

    /// <summary>
    /// Gets the player transform and start Z position
    /// </summary>
    private void Start() {
        player = GameManager.instance.GetPlayer().transform;
        startZ = player.position.z;
    }

    /// <summary>
    /// Updates the position of the mountain
    /// </summary>
    void Update () {
        Vector3 t = transform.position;
        t.z = player.position.z + 100;
        t.y = (player.position.z - startZ) * speed;
        t.y = Mathf.Min(t.y, 45);
        transform.position = t;

    }
}
