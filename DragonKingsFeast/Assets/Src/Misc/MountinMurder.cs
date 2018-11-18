﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountinMurder : MonoBehaviour {

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
