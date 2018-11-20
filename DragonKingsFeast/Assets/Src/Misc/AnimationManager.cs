using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this is used to manage when the enemies deal damage to the play based on their attack animations
/// as well as helping manage any other important functions that need to be executed depending on the animation point
/// 
/// <para> Author: Callum Dunstone </para>
/// 
/// </summary>
public class AnimationManager : MonoBehaviour {

    /// <summary> the owner of this script that it is managing </summary> 
    public Enemy owner;
    /// <summary> the audio source for playing sounds with </summary> 
    public GameObject audioSource;
    
    /// <summary> reference to the owners melee attack option </summary> 
    private MeleeAttack meleeAttackActions = null;
    /// <summary> reference to the owners ranged attack option </summary> 
    private RangedAttack rangedAttackActions = null;
        
    /// <summary> audio of the enemies death sound </summary> 
    public AudioClip death;

    /// <summary>
    /// 
    /// here we set up the enemy attack action types based on the enemy type
    /// 
    /// </summary>
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

    /// <summary>
    /// handles any special functions for ranged attacks
    /// </summary>
    public void SetUpRangedAttack () {
        //shoots the attacks
        rangedAttackActions.DealDamage();
    }

    /// <summary>
    /// handles any special functions for melee attacks
    /// </summary>
    public void SetUpMeeleAttack() {
        //deals melee damage
        meleeAttackActions.DealDamage();
    }

    /// <summary>
    /// handles any special functions for melee attacks
    /// </summary>
    public void SetUpDeath() {
        //turns off their owner at the appropriate time so that their death animation actually plays
        owner.TurnOff();
    }

    /// <summary>
    /// plays the death sound for where we kill the enemy
    /// </summary>
    public void PlayDeathSound() {
        GameObject go = Instantiate(audioSource, transform.position, Quaternion.identity, transform);
        go.GetComponent<AudioSource>().clip = death;
        go.GetComponent<AudioSource>().Play();
        Destroy(go, 2.0f);
    }
}
