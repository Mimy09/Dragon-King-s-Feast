using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// this is the Witch sub class, it inherits from the Enemy class
/// this script is used for the witches AI'
/// 
/// <para>Author: Callum Dunstone</para>
/// 
/// </summary>
public class Witch : Enemy {

    /// <summary> this is a script used to manage using ranged attacks </summary>
    public RangedAttack rangedAttack;

    /// <summary> witches idl sound when not damaged </summary>
    public AudioClip flying;
    /// <summary> witches idl sound when damaged </summary>
    public AudioClip damagedFlying;

    /// <summary> audio source we play the sounds through </summary>
    public AudioSource audioSource;

    /// <summary>
    /// 
    /// this overrides the enemy classes reset function
    /// so that it can also reset the rangedAttack for this enemy
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    public override void Reset() {
        base.Reset();
        rangedAttack.Reset();
    }

    /// <summary>
    /// 
    /// this overrides the enemy classes Turn on function 
    /// so that we are playing the right idl sound
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    public override void TurnOn() {
        base.TurnOn();
        audioSource.spatialBlend = 1;
        audioSource.clip = flying;
        audioSource.Play();
        audioSource.loop = true;
    }

    /// <summary>
    /// 
    /// this overrides the enemy classes Damage function so that we swap
    /// to the right audio sound
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    public override void TakeDamage() {
        base.TakeDamage();
        audioSource.clip = damagedFlying;
        audioSource.Play();
        audioSource.loop = true;
    }

    /// <summary>
    /// 
    /// this function is called at the start of the gameobjects life
    /// it is used to get the player and set up its attack scripts
    /// as well as assign its enemy type
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    protected void Awake() {
        player = GameManager.player;
        rangedAttack.SetUp(player, this);
        m_enemyType = e_EnemyType.Witch;
    }
    
    /// <summary>
    /// 
    /// called at the start of the game objects life. this is used to 
    /// make sure the enemies health is correct and that it has the player
    ///  
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    private void Start() {
        player = GameManager.player;
        m_health = baseHealth;
    }
    
    /// <summary>
    /// 
    /// this is used to check to see if we can shoot the player every frame
    /// and if the enemy is behind the player then we deactivate the enemy
    /// sending them to the object pool  
    /// 
    /// <para>Author: Callum Dunstone</para>
    /// 
    /// </summary>
    protected void Update() {
        //check if we can shoot the player
        rangedAttack.AttackPlayer();
        rangedAttack.Update();

        //check if we are behind the player and if so deactivate our selves
        if (player.transform.position.z > (transform.position.z + despawnOffset)) {
            spawner.GetListOfEnemies().Remove(this);

            TurnOff();
        }
    }
}
