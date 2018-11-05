using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBigDragon : MonoBehaviour {

    public GameObject dragon;

	// Use this for initialization
	void Start () {
        //dragon = GameObject.FindGameObjectWithTag("BadDragon");
        //dragon.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if (GameManager.instance.GetPlayer().transform.position.z > transform.position.z) {
            dragon.transform.position = GameManager.instance.GetPlayer().transform.position - (Vector3.up * 5) + (Vector3.forward * 10);
            dragon.SetActive(true);
        }
    }
}
