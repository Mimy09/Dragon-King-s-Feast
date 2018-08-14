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

    public float KillTime;
    private float killtimer;

    private Vector3 dir;

    public void TurnOn() {
        killtimer = 0;
        target = new Vector3();
        damage = 0;
        playerAttack = false;
        forwardSpeed = 0;
        dir = new Vector3();
    }

    public void TurnOff() {
        GameManager.instance.GetObjectPool().AddProjectileToPool(this);
    }

    public void SetUp(Vector3 _target, float _damage, bool _playerAttack, float _forwardSpeed) {
        target = _target;
        damage = _damage;
        playerAttack = _playerAttack;
        forwardSpeed = _forwardSpeed;
        dir = target - transform.position;
        transform.LookAt(target);
    }
    
	void Update () {
        killtimer += Time.deltaTime;
        transform.position += (dir * Time.deltaTime) * forwardSpeed;

        if (killtimer > KillTime) {
            TurnOff();
        }
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
