using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [ReadOnly]
    public Vector3 target;

    [ReadOnly]
    public float damage;

    [ReadOnly]
    public bool playerAttack;

    [ReadOnly]
    public float forwardSpeed;

    public void SetUp(Vector3 _target, float _damage, bool _playerAttack, float _forwardSpeed) {
        target = _target;
        damage = _damage;
        playerAttack = _playerAttack;
        forwardSpeed = _forwardSpeed;
    }
    
	void Update () {
        transform.LookAt(target);
        transform.position += (transform.forward * Time.deltaTime) * forwardSpeed;
	}

    public void OnTriggerEnter(Collider other) {
        if (playerAttack) {
            if (other.tag == "Enemy") {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        else {
            if (other.tag == "Player") {
                other.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
