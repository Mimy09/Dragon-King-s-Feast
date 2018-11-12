using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseMountain : MonoBehaviour {

    private Transform player;
    private float startZ;

    private void Start() {
        player = GameManager.instance.GetPlayer().transform;
        startZ = player.position.z;
    }

    void Update () {
        Vector3 t = transform.position;
        t.z = player.position.z + 100;
        t.y = (player.position.z - startZ) * 0.1f;
        t.y = Mathf.Min(t.y, 144);
        transform.position = t;

    }
}
