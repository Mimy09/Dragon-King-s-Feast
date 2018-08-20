using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //the damage the projectile will do when it hits its target
    [ReadOnly] public float damage;

    //this dictates where it was shot from the player or the enemys and who the projectile will harm
    [ReadOnly] public bool playerAttack;

    //this is how fast it will move forward relative to the shooter
    [ReadOnly] public float forwardSpeed;

    //how long it will wait before shutting its self off
    public float KillTime;
    private float killtimer;

    //the direction the projectile will move
    private Vector3 dir;

    public void TurnOn() {
        killtimer = 0;
        damage = 0;
        playerAttack = false;
        forwardSpeed = 0;
        dir = new Vector3();
        gameObject.SetActive(true);
    }

    public void TurnOff() {
        GameManager.instance.GetObjectPool().AddProjectileToPool(this);
    }

    public void SetUp(Vector3 _target, float _damage, bool _playerAttack, float _forwardSpeed) {
        damage = _damage;
        playerAttack = _playerAttack;
        forwardSpeed = _forwardSpeed;
        dir = _target - transform.position;
        transform.LookAt(_target);
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
                TurnOff();
            }
        }
        else {
            if (other.tag == "Player") {
                other.GetComponent<Player>().TakeDamage(damage);
                TurnOff();
            }
        }
    }
}
