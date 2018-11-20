using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this is used to pull in the coins near the player so that they can pick
/// them up in a easier manner
/// 
/// <para> Author: Callum Dunstone </para>
/// 
/// </summary>
public class CoinMagnet : MonoBehaviour {

    /// <summary> list of all the coins near the player </summary> 
    private List<Transform> pickUps = new List<Transform>();
    /// <summary> how fast they will move to the player to be picked up </summary> 
    public float speed;

    /// <summary>
    /// 
    /// if we run into a coin to move to the player to pick up
    /// we added it to the list of coins we are aware off
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "PickUp") {
            pickUps.Add(other.transform);
        }
    }

    /// <summary>
    /// 
    /// if we have moved out of range of the coin with out picking it up
    /// we remove it from the list of coins we wish to pick up
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other) {
        if (other.tag == "PickUp") {
            pickUps.Remove(other.transform);
        }
    }

    /// <summary>
    /// 
    /// here we make sure that the coins we are keeping track with are still active and not
    /// picked up, as well as moving any coins towards the player
    /// 
    /// </summary>
    public void Update() {
        //if there are no coins to worry about just exit
        if (pickUps.Count == 0) {
            return;
        }

        List<Transform> killList = new List<Transform>();

        //find all the coins that have been already picked up in our list of coins
        for (int i = 0; i < pickUps.Count; i++) {
            if (pickUps[i].gameObject.activeSelf == false) {
                killList.Add(pickUps[i]);
            }
        }

        //removed the coins already picked up from our list
        if (killList.Count > 0) {
            for (int i = 0; i < killList.Count; i++) {
                pickUps.Remove(killList[i]);
            }
        }

        //moves the coins towards the player
        if (pickUps.Count > 0) {
            for (int i = 0; i < pickUps.Count; i++) {
                Vector3 dir;
                dir = transform.position - pickUps[i].transform.position;
                pickUps[i].position += (dir * Time.deltaTime) * speed;
            }
        }


    }
}
