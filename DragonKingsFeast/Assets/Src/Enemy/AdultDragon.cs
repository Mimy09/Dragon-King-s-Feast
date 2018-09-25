using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultDragon : Enemy {
    
    public Waypoint[] waypoints;
    private int waypoint_state;
    private int waypoint_child_state;
    private float waypoint_collider_size = 3;
    private int waypoint_timer = 0;

    private GameObject lookAtPos;

    protected override void Awake () {
        m_enemyType = e_EnemyType.AdultDragon;
    }

    private void Start() {
        Reset();
        waypoint_state          = 0;
        waypoint_child_state    = 0;
        lookAtPos               = new GameObject("LookAt");
        lookAtPos.transform.parent = transform;
    }

    public override void OnDeath() {
        transform.parent.gameObject.SetActive(false);
    }
    public override void TurnOff() { }
    public override void TurnOn() { }

    private void Update() {
        if (waypoints.Length <= 0) return;
        if (waypoint_state > waypoints.Length - 1) waypoint_state = 0;
        if (waypoint_child_state > waypoints[waypoint_state].childs.Length - 1) return;

        lookAtPos.transform.position = transform.position;
        lookAtPos.transform.LookAt(waypoints[waypoint_state].childs[waypoint_child_state].transform);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            lookAtPos.transform.rotation,
            Time.deltaTime * forwardSpeed
            );

        transform.position += transform.forward * Time.deltaTime * speed;

        // Check if is at next waypoint
        if ( Vector3.Distance(
            transform.position,
            waypoints[waypoint_state].childs[waypoint_child_state].transform.position
            ) < waypoint_collider_size) {
            // Go to next waypoint
            waypoint_child_state++;

            // If went to last waypoint, go to next set of waypoints
            if (waypoint_child_state == waypoints[waypoint_state].childs.Length) {
                waypoint_child_state = 0;
                waypoint_state++;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, waypoint_collider_size);
    }
}
