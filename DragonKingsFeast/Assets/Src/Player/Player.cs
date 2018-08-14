﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerIK))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    public int health;

    public int score {
        get {
            return health;
        }
    }

    public int damage;
    public int range;

    public void TakeDamage(float amount) {
        health -= (int)(amount + 0.5f);
    }

    public void ApplyBoost(e_ItemType type) {
        switch (type) {
            case e_ItemType.Boost_Speed:
                break;

            case e_ItemType.Boost_Attack:
                break;

            case e_ItemType.Boost_Defense:
                break;

            case e_ItemType.Pickup:
                Debug.LogError("USE OTHER FUNCTION WRONG ONE");
                break;

            default:
                break;
        }
    }

    public void ApplyLoot(int amount) {
        health += amount;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
