using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Enemy owner;
    public AudioSource audioSource;
    
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
        audioSource.clip = rangedAttack;
        audioSource.loop = false;

    }

    public void SetUpMeeleAttack() {
        meleeAttackActions.DealDamage();
        audioSource.clip = meeleAttack;
        audioSource.loop = false;
    }

    public void SetUpIdl() {
        audioSource.clip = idl;
        audioSource.loop = true;
    }

    public void SetUpDeath() {
        audioSource.clip = death;
        audioSource.loop = false;
        owner.TurnOff();
    }
}
