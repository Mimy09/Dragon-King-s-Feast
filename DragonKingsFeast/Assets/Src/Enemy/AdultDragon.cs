using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultDragon : Enemy {

    public MeleeAttack meleeAttack;

    public Waypoint[] waypoints;
    public int waypoint_state;
    private int waypoint_child_state;
    private float waypoint_collider_size = 3;

    private GameObject lookAtPos;
    private float timer;
    public float DamageTime;
    public bool canDamage = false;

    protected override void Awake () {
        m_enemyType = e_EnemyType.AdultDragon;
    }

    private void Start() {
        Reset();
        meleeAttack.SetUp(player, this);
        meleeAttack.canAttackMultipleTimes = true;
        meleeAttack.usesAnimation = false;

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

    public void OnEnable() {
        GameManager.enemyList.Add(this.gameObject);
    }

    public override void TakeDamage(float damage) {
        GameObject g = GameManager.objectPoolManager.FindItemOfType(e_ItemType.Pickup);
        g.transform.position = transform.position;
    }

    public override void TakeDamage2(float damage) {
    }

    protected override void Update() {
        if (timer < DamageTime && canDamage == false) {
            timer += Time.deltaTime;
        } else {
            canDamage = true;
            timer = 0;
        }

        if (waypoints.Length <= 0) return;
        if (waypoint_state > waypoints.Length - 1) waypoint_state = 0;
        if (waypoint_child_state > waypoints[waypoint_state].childs.Length - 1) return;

        lookAtPos.transform.position = transform.position;
        lookAtPos.transform.LookAt(waypoints[waypoint_state].childs[waypoint_child_state].transform);
        lookAtPos.transform.Rotate(new Vector3(90, 0, 0));

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            lookAtPos.transform.rotation,
            Time.deltaTime * forwardSpeed
            );


        GetComponent<Rigidbody>().velocity = (transform.up * speed);
        //transform.position += transform.forward * Time.deltaTime * speed;

        // Check if is at next waypoint
        if ( Vector3.Distance(
            transform.position,
            waypoints[waypoint_state].childs[waypoint_child_state].transform.position
            ) < waypoint_collider_size) {
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
    

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && canDamage == true) {
            // animat.SetBool("Attack", true);
            canDamage = false;
            meleeAttack.DealDamage();
        }
    }
}
