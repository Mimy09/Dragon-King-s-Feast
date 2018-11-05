using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Enemy owner;
    public GameObject audioSource;
    
    private MeleeAttack meleeAttackActions = null;
    private RangedAttack rangedAttackActions = null;



    public AudioClip rangedAttack;
    public AudioClip meeleAttack;
    public AudioClip idl;
    public AudioClip death;

    public void Start() {
        switch (owner.EnemyType) {
            case e_EnemyType.Ghost:
                meleeAttackActions = (owner as Ghost).meleeAttack;
                break;
            case e_EnemyType.Witch:
                rangedAttackActions = (owner as Witch).rangedAttack;
                break;
            case e_EnemyType.Phoenix:
                meleeAttackActions = (owner as Phoenix).meleeAttack;
                rangedAttackActions = (owner as Phoenix).rangedAttack;
                break;
            default:
                break;
        }
    }

    public void SetUpRangedAttack () {
        rangedAttackActions.DealDamage();
    }

    public void SetUpMeeleAttack() {
        meleeAttackActions.DealDamage();
    }

    public void SetUpIdl() {
    }

    public void SetUpDeath() {
        owner.TurnOff();
    }

    public void PlayDeathSound() {
        GameObject go = Instantiate(audioSource, transform.position, Quaternion.identity, transform);
        go.GetComponent<AudioSource>().clip = death;
        go.GetComponent<AudioSource>().Play();
        Destroy(go, 2.0f);
    }
}
