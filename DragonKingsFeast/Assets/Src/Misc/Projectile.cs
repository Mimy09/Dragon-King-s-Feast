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
    private float m_liveTime;
    private float m_killtimer;

    //the direction the projectile will move
    private Vector3 m_target;
    private Transform m_enemy;

    public void TurnOn() {
        m_killtimer = 0;
        damage = 0;
        playerAttack = false;
        forwardSpeed = 0;
        m_target = new Vector3();
        gameObject.SetActive(true);
        m_enemy = null;
        m_liveTime = 0;
    }

    public void TurnOff() {
        GameManager.instance.GetObjectPool().AddProjectileToPool(this);
    }

    public void SetUp(Transform _target, float _damage, float _forwardSpeed, float _liveTime) {
        damage = _damage;
        playerAttack = false;
        forwardSpeed = _forwardSpeed;
        m_enemy = _target;
        m_target = _target.transform.position;
        transform.LookAt(_target);

        m_liveTime = _liveTime;
    }

    public void SetUp(Transform _target, float _damage, bool _playerAttack, float _forwardSpeed, float _liveTime) {
        damage = _damage;
        playerAttack = _playerAttack;
        forwardSpeed = _forwardSpeed;
        m_enemy = _target;
        m_target = _target.transform.position;
        transform.LookAt(_target.transform.position);

        m_liveTime = _liveTime;
    }

    void Update () {
        m_killtimer += Time.deltaTime;
        
        Move();

        transform.position += (transform.forward * Time.deltaTime) * forwardSpeed;

        if (m_killtimer > m_liveTime) {
            TurnOff();
        }
	}

    private void Move() {
        if (playerAttack) {
            if (transform.position.z < m_enemy.transform.position.z) {
                m_target = Vector3.Lerp(m_target, m_enemy.transform.position, 0.1f);
                transform.LookAt(m_target);
            }
        }
        else {
            if (transform.position.z > m_enemy.transform.position.z) {
                m_target = Vector3.Lerp(m_target, m_enemy.transform.position, 0.1f);
                transform.LookAt(m_target);
            }
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
