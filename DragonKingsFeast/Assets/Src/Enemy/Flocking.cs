using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flocking : MonoBehaviour {
    Rigidbody rb = null;
    List<Enemy> formation = new List<Enemy>();
    List<Enemy> neighbour = new List<Enemy>();
    Vector3 trackPosition = Vector3.zero;
    Vector3 offset = Vector3.zero;
    bool isLeader = false;

    [Header("Flocking")]
    public float seekSpeed = 1;

    [Header("Radius")]
    public float separationRadius = 1;
    public float cohesionRadius = 1;
    public float alignRadius = 1;

    [Header("Weights")]
    [Range(0, 1)] public float separationWeight = 1;
    [Range(0, 1)] public float cohesionWeight = 1;
    [Range(0, 1)] public float alignWeight = 1;

    private void Start() {
        // set the rigidbody
        rb = GetComponent<Rigidbody>();

        // set formation
        formation = GetComponent<Enemy>().spawner.GetListOfEnemies();
        if (formation.Count == 0) return;
        if (formation[0].gameObject == gameObject) isLeader = true;
        if (!isLeader) offset = formation[0].transform.position - transform.position;
    }

    private void Update() {
        if (formation.Count == 0) return;
        if (formation[0].gameObject == gameObject) isLeader = true;

        if (isLeader) {
            trackPosition = GameManager.player.transform.position;
        }
        else trackPosition = formation[0].transform.position - offset;

        Vector3 vel = Vector3.zero;

        vel = (Seek());
        vel += (Separation());
        vel += (Alignment());
        vel += (Cohesion());

        rb.velocity = vel;
    }

    Vector3 GetVelocity() {
        return rb.velocity;
    }

    void GetNeighbourhood(float radius) {
        neighbour.Clear();
        for (int i = 0; i < formation.Count; i++) {
            if (formation[i].gameObject == gameObject) continue;
            if (Vector3.Distance(transform.position, formation[i].transform.position) < radius) {
                neighbour.Add(formation[i]);
            }
        }
    }

    Vector3 Seek() {
        Vector3 direction = trackPosition - transform.position;
        direction = direction.normalized;
        return direction * seekSpeed;
    }

    Vector3 Separation() {
        GetNeighbourhood(separationRadius);
        if (neighbour.Count == 0) return Vector3.zero;
        Vector3 separateForce = Vector3.zero;
        
        for (int i = 0; i < neighbour.Count; i++)
            separateForce += transform.position - neighbour[i].transform.position;
        separateForce /= neighbour.Count;

        return (separateForce - rb.velocity) * separationWeight;
    }

    Vector3 Alignment() {
        GetNeighbourhood(alignRadius);
        if (neighbour.Count == 0) return Vector3.zero;
        Vector3 alignForce = Vector3.zero;

        for (int i = 0; i < neighbour.Count; i++)
            alignForce += neighbour[i].GetComponent<Flocking>().GetVelocity();
        alignForce /= neighbour.Count;

        return (alignForce - rb.velocity) * alignWeight;
    }

    Vector3 Cohesion() {
        GetNeighbourhood(cohesionRadius);
        if (neighbour.Count == 0) return Vector3.zero;
        Vector3 cohesionForce = Vector3.zero;

        for (int i = 0; i < neighbour.Count; i++)
            cohesionForce += neighbour[i].transform.position - transform.position;
        cohesionForce /= neighbour.Count;

        return (cohesionForce - rb.velocity) * cohesionWeight;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red * new Vector4(1, 1, 1, 0.5f);
        Gizmos.DrawWireSphere(transform.position, separationRadius);
        Gizmos.color = Color.green * new Vector4(1, 1, 1, 0.5f);
        Gizmos.DrawWireSphere(transform.position, cohesionRadius);
        Gizmos.color = Color.yellow * new Vector4(1, 1, 1, 0.5f);
        Gizmos.DrawWireSphere(transform.position, alignRadius);

        if (rb != null) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
        }

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(trackPosition, 0.1f);
    }
}