using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseMountain : MonoBehaviour {

    private Transform player;
    private float startZ;
    public float speed;

    private void Start() {
        player = GameManager.instance.GetPlayer().transform;
        startZ = player.position.z;
    }

    void Update () {
        Vector3 t = transform.position;
        t.z = player.position.z + 100;
        t.y = (player.position.z - startZ) * speed;
        t.y = Mathf.Min(t.y, 45);
        transform.position = t;

    }
}
