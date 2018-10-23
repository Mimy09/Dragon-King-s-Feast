using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpout : MonoBehaviour {

    public float speed  = 1;
    public float height = 10;
    public float bias   = 50;
    Transform player;

    public GameObject bubbles;
    public GameObject spout;
    public Transform p;

    private GameObject hodler;

    private void Start() {
        hodler = Instantiate(bubbles, p.position, Quaternion.identity);
    }

    void Update () {
        if (player == null) {
            if (GameManager.player == null) return;
            player = GameManager.player.transform;
        }

        Vector3 t = transform.position;
        float dist = t.z - player.position.z;

        if (dist < bias)
            if (t.y < height) {
                t.y += Time.deltaTime * speed;

                if (hodler != null) {
                    Destroy(hodler, 1.0f);
                    hodler = null;
                    Destroy(Instantiate(spout, p.position, Quaternion.identity), 3.0f);
                }
            }

        transform.position = t;
	}

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (transform.up * height), 1);
    }
}
