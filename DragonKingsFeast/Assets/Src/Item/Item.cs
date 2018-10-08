﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_ItemType {
    Boost_Speed,
    Boost_Attack,
    Boost_Defense,
    Pickup,
}

public class Item : MonoBehaviour {
    public int value = 1;
    public e_ItemType m_itemType;
    public e_ItemType ItemType { get { return m_itemType; } }

    public virtual void TurnOff() { GameManager.instance.GetObjectPool().AddItemTooPool(this); }
    public virtual void TurnOn() { Reset(); this.gameObject.SetActive(true); }
    public virtual void Reset() { }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (m_itemType != e_ItemType.Pickup) {
                GameManager.player.ApplyBoost(m_itemType);
            }
            else {
                GameManager.player.ApplyLoot(value);
                Transform t = GameObject.FindGameObjectWithTag("CoinSP").transform;
                Instantiate(Helper.CoinPath, t.position, Quaternion.identity, t);
            }

            TurnOff();
        }
    }

    public void Update() {
        if (transform.position.z < (GameManager.player.transform.position.z - 5)) {
            TurnOff();
        }
    }
}
