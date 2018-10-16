using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpout : MonoBehaviour {

    public float speed  = 1;
    public float height = 10;
    public float bias   = 50;
    Transform player;

	void Update () {
        if (player == null) {
            if (GameManager.player == null) return;
            player = GameManager.player.transform;
        }

        Vector3 t = transform.position;
        float dist = Vector3.Distance(t, player.position);

        if (dist < bias)
            if (t.y < height)
                t.y += Time.deltaTime * speed;
	}

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (transform.up * height), 1);
    }
}
