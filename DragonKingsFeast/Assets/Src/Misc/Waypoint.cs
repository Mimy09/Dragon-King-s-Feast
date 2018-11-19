using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Hold the waypoint data for the big bad dragon to follow
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {

    /// <summary>
    /// the parent holding the waypoints
    /// </summary>
    public Transform parent;

    /// <summary>
    /// the childs in the parent
    /// </summary>
    [ReadOnly]
    public Transform[] childs;

	/// <summary>
    /// Updates the list of childs
    /// </summary>
	void Update () {
        childs = new Transform[parent.transform.childCount];
        for (int i = 0; i < childs.Length; i++)
            childs[i] = parent.transform.GetChild(i);
	}

    /// <summary>
    /// Draws the Gizmos used showing the position and path of the waypoints
    /// </summary>
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
