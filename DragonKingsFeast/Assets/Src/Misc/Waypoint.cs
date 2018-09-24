using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {

    public Transform parent;
    [ReadOnly]
    public Transform[] childs;

	// Use this for initialization
	void Update () {
        childs = new Transform[parent.transform.childCount];
        for (int i = 0; i < childs.Length; i++)
            childs[i] = parent.transform.GetChild(i);
	}

    private void OnDrawGizmos() {
        if (childs == null) return;
        if (childs.Length < 1) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(childs[0].position, 0.5f);

        if (parent == null) return;
        if (childs.Length < 2) return;

        for (int i = 1; i < childs.Length; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(childs[i].position, childs[i - 1].position);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(childs[i].position, 0.5f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(childs[i].position, childs[i].position + childs[i].forward);
        }
    }
}
