using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this script is used on the projectiles in the game to 
/// make them move to their target. it is used by both the enemy and player
/// projectiles
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
public class Projectile : MonoBehaviour {

    /// <summary> the damage the projectile will do when it hits its target </summary> 
    [ReadOnly] public float damage;
    
    /// <summary> this dictates where it was shot from the player or the enemys and who the projectile will harm </summary> 
    [ReadOnly] public bool playerAttack;
    
    /// <summary> this is how fast it will move forward relative to the shooter </summary> 
    [ReadOnly] public float forwardSpeed;
    
    /// <summary> how long it will wait before shutting its self off </summary> 
    private float m_liveTime;
    /// <summary> when kill time is greater then live time we turn off the projectile </summary> 
    private float m_killtimer;
    
    /// <summary> this is target we are tying to hit </summary> 
    private Vector3 m_target;
    /// <summary> this is the gameobject we are trying to hit </summary> 
    private Transform m_enemy;
    /// <summary> this is how much the players projectiles will correct to hit their target </summary> 
    public float playerTrackPrecent;
    /// <summary> this is how much the enemy projectiles will correct to hit their target </summary> 
    public float enemyTrackPrecent;
    
    /// <summary> if this is false it is a whitch attack </summary> 
    private bool isFireBall;

    /// <summary> this is the partical effects for the fireball traveling </summary> 
    public GameObject playerFireBall;
    /// <summary> this is the partical effects for the fireball hitting </summary> 
    public GameObject playerFireBallHit;

    /// <summary> this is the partical effects for the sand attack traveling </summary> 
    public GameObject sandAttack;
    /// <summary> this is the partical effects for the sand attack hitting </summary> 
    public GameObject sandAttackHit;

    /// <summary> audio clips for the fireball traveling </summary> 
    public List<AudioClip> fireBallSound;
    /// <summary> audio clips for the sand attack traveling </summary> 
    public List<AudioClip> sandBallSound;

    /// <summary> the audio source we will be playing sounds from </summary> 
    private AudioSource audioSource;

    /// <summary>
    /// 
    /// here we set up the audio stuff for use in the game
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// here we turn on the projectile and set its values to default
    /// used after we get it from the object pool
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// here we turn off the projectile as we send it to the object pool
    /// 
    /// </summary>
    public void TurnOff() {
        GameManager.instance.GetObjectPool().AddProjectileToPool(this);

        audioSource.Stop();

        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 
    /// this is the set up used by the enemies when they are shooting their projectiles
    /// 
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_damage"></param>
    /// <param name="_forwardSpeed"></param>
    /// <param name="_liveTime"></param>
    /// <param name="_isFireBall"></param>
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

    /// <summary>
    /// 
    /// this is the set up used by the player when they are shooting their projectiles
    /// 
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_damage"></param>
    /// <param name="_playerAttack"></param>
    /// <param name="_forwardSpeed"></param>
    /// <param name="_liveTime"></param>
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

    /// <summary>
    /// 
    /// here we move the projectile towards its target, while updating its time to see if it has lived to long
    /// if the projectile also moves behind the player we turn it off
    /// 
    /// </summary>
    void Update () {
        m_killtimer += Time.deltaTime;
        
        Move();

        transform.position += (transform.forward * Time.deltaTime) * forwardSpeed;

        if (m_killtimer > m_liveTime || (transform.position.z + 5) < GameManager.player.transform.position.z) {
            TurnOff();
        }
	}

    /// <summary>
    /// 
    /// here we move the projectile forwards and have it track its target
    /// depending on it its a player projectile or not depends on what diffrent tracking we use
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// here we check if we have collided with our target
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        
        //if it is a player attack and it has hit something we first check if it is a viable target
        if (playerAttack) {
            if (other.tag == "Enemy" || other.tag == "BadDragon") {
                //check that the target is not already dead and if not we hit them
                if (other.GetComponent<Enemy>().isDead == false) {
                    other.GetComponent<Enemy>().TakeDamage();

                    GameObject go = Instantiate(playerFireBallHit);
                    go.transform.position = transform.position;
                    Destroy(go, 2.0f);

                    TurnOff();
                }//else if the transform is our target but they are dieing still hit them
                else if (other.transform == m_enemy) {
                    GameObject go = Instantiate(playerFireBallHit);
                    go.transform.position = transform.position;
                    Destroy(go, 2.0f);
                }
            }
        }
        else { // if we have hit the player deal damage then play the right partical effects dependning on if it was a fireball attack or not
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
