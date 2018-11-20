using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this is put onto the terrain so that if the player runs into it they die!
/// 
/// <para> Author: Callum Dunstone </para>
/// 
/// </summary>
public class MountinMurder : MonoBehaviour {

    /// <summary>
    /// 
    /// this is used to detect if a player or enemy has flyed into the terrain
    /// and then murders them
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.player.hitMountain();
        }
        if (other.tag == "Enemy") {
            other.GetComponent<Enemy>().TakeDamage2(1000000);
            other.GetComponent<Enemy>().TakeDamage();
        }
    }
}
