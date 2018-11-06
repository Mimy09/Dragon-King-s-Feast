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
    public float playerTrackPrecent;
    public float enemyTrackPrecent;

    private bool hit;
    private bool isFireBall;

    public GameObject playerFireBall;
    public GameObject playerFireBallHit;

    public GameObject sandAttack;
    public GameObject sandAttackHit;

    public List<AudioClip> fireBallSound;
    public List<AudioClip> sandBallSound;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) {
            Debug.LogError("NO AUDIO SOURCE FOR " + this.name);
        }

        if (fireBallSound.Count == 0) {
            Debug.LogError("NO FIRE BALL SOUND CLIPS " + this.name);
        }

        if (sandBallSound.Count == 0) {
            Debug.LogError("NO SAND BALL SOUND CLIPS " + this.name);
        }
    }

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

        audioSource.Stop();

        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetUp(Transform _target, float _damage, float _forwardSpeed, float _liveTime, bool _isFireBall) {
        damage = _damage;
        playerAttack = false;
        forwardSpeed = _forwardSpeed;
        m_enemy = _target;
        m_target = _target.transform.position;
        transform.LookAt(_target);

        isFireBall = _isFireBall;
        if (isFireBall) {
            Instantiate(playerFireBall, transform.position, Quaternion.identity, transform);
            audioSource.clip = fireBallSound[Random.Range(0, fireBallSound.Count)];
        }
        else {
            Instantiate(sandAttack, transform.position, Quaternion.identity, transform);
            audioSource.clip = sandBallSound[Random.Range(0, sandBallSound.Count)];
        }

        audioSource.Play();
        m_liveTime = _liveTime;
    }

    public void SetUp(Transform _target, float _damage, bool _playerAttack, float _forwardSpeed, float _liveTime) {
        damage = _damage;
        playerAttack = _playerAttack;
        forwardSpeed = _forwardSpeed;
        m_enemy = _target;
        m_target = _target.transform.position;
        transform.LookAt(_target.transform.position);

        GameObject go = Instantiate(playerFireBall, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0, 0, 0);

        audioSource.clip = fireBallSound[Random.Range(0, fireBallSound.Count)];
        audioSource.Play();

        m_liveTime = _liveTime;
    }

    void Update () {
        m_killtimer += Time.deltaTime;
        
        Move();

        transform.position += (transform.forward * Time.deltaTime) * forwardSpeed;

        if (m_killtimer > m_liveTime || (transform.position.z + 5) < GameManager.player.transform.position.z) {
            TurnOff();
        }
	}

    private void Move() {
        if (playerAttack) {
            if (transform.position.z < m_enemy.transform.position.z) {
                m_target = Vector3.Lerp(m_target, m_enemy.transform.position, playerTrackPrecent);
                transform.LookAt(m_target);
            }
        }
        else {
            if (transform.position.z > m_enemy.transform.position.z) {
                m_target = Vector3.Lerp(m_target, m_enemy.transform.position, enemyTrackPrecent);
                transform.LookAt(m_target);
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        
        if (playerAttack) {
            if (other.tag == "Enemy") {
                if (other.GetComponent<Enemy>().isDead == false) {
                    other.GetComponent<Enemy>().TakeDamage(damage);

                    GameObject go = Instantiate(playerFireBallHit);
                    go.transform.position = transform.position;
                    Destroy(go, 2.0f);

                    TurnOff();
                }
                else if (other.transform == m_enemy) {
                    GameObject go = Instantiate(playerFireBallHit);
                    go.transform.position = transform.position;
                    Destroy(go, 2.0f);
                }
            }
        }
        else {
            if (other.tag == "Player") {
                other.GetComponent<Player>().TakeDamage();

                if (isFireBall) {
                    Destroy(Instantiate(playerFireBallHit, transform.position, Quaternion.identity), 2.0f);
                }
                else {
                    Destroy(Instantiate(sandAttackHit, transform.position, Quaternion.identity), 2.0f);
                }

                TurnOff();
            }
        }
    }
}
