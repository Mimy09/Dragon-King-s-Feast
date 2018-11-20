using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// these are used to identify what type of pick up an item is
/// 
/// </summary>
public enum e_ItemType {
    Boost_Speed,
    Boost_Attack,
    Boost_Defense,
    Pickup,
}

/// <summary>
/// 
/// this script is used to manage coins and the different types of boosts
/// 
/// <para>Author: Mitchell Jenkins</para>
/// 
/// </summary>
public class Item : MonoBehaviour {
    /// <summary> this is the value of a coin when you pick it up, so how much score it will give you </summary> 
    public int value = 1;
    /// <summary> the type of item it is meant to be </summary> 
    public e_ItemType itemType;
    /// <summary> this is used to determine if the coin was properly spawned in or not </summary> 
    public bool spawner = false;

    /// <summary> this is a reference to its particle effect that we play when we pick it up </summary> 
    public GameObject Shine;

    /// <summary>
    /// 
    /// this is used when putting the item in the object pool
    /// 
    /// </summary>
    public virtual void TurnOff() {
        GameManager.instance.GetObjectPool().AddItemTooPool(this);
        GameManager.itemList.Remove(gameObject);
    }

    /// <summary>
    /// 
    /// this is used when getting the item out of the object pool
    /// 
    /// </summary>
    public virtual void TurnOn() {
        GameManager.itemList.Add(gameObject);
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// this is used to apply the item to the player as well as added new coins
    /// to the loot bag to show an increase of the score
    /// 
    /// </summary>
    public void OnTriggerEnter(Collider other) {
        if (spawner == false) return;

        if (other.tag == "Player") {
            if (itemType != e_ItemType.Pickup) {
                GameManager.player.ApplyBoost(itemType);
            }
            else {
                Destroy(Instantiate(Shine, transform.position, Quaternion.identity), 2.0f);

                Transform t = GameObject.FindGameObjectWithTag("CoinSP").transform;
                for (int i = 0; i < value; i++) {
                    GameObject g = Instantiate(Helper.CoinPath, new Vector3(0, 0, 0), Quaternion.identity, t) as GameObject;
                    g.GetComponent<RectTransform>().localPosition = new Vector3(
                        Random.Range(-Screen.width / 40, Screen.width / 40),
                        0,
                        0
                        );
                }
                GameManager.player.ApplyLoot(value);
            }

            TurnOff();
        }
    }

    /// <summary>
    /// 
    /// this is used to spin the items in the world to help them better stand out
    /// as well as manage if the items move behind the player to deactivate them selves
    /// 
    /// </summary>
    public void Update() {
        if (spawner == false) Debug.LogError("WRONG SPAWN!!!!!!!!!!!!");

        Vector3 r = transform.rotation.eulerAngles;
        r.y += Time.deltaTime * 90;
        transform.rotation = Quaternion.Euler(r);

        if (transform.position.z < (GameManager.player.transform.position.z - 5)) {
            TurnOff();
        }
    }

    /// <summary>
    /// 
    /// this is used to visulay show if a coin has been spawned incorectly into the scene
    /// 
    /// </summary>
    private void OnDrawGizmos() {
        if (spawner == false) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
        }
    }
}
