using System.Collections;
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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
