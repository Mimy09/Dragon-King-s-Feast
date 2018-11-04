using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {

    private List<Transform> pickUps = new List<Transform>();
    public float speed;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "PickUp") {
            pickUps.Add(other.transform);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.tag == "PickUp") {
            pickUps.Remove(other.transform);
        }
    }

    public void Update() {
        List<Transform> killList = new List<Transform>();

        if (pickUps.Count == 0) {
            return;
        }

        for (int i = 0; i < pickUps.Count; i++) {
            if (pickUps[i].gameObject.activeSelf == false) {
                killList.Add(pickUps[i]);
            }
        }

        if (killList.Count > 0) {
            for (int i = 0; i < killList.Count; i++) {
                pickUps.Remove(killList[i]);
            }
        }

        if (pickUps.Count > 0) {
            for (int i = 0; i < pickUps.Count; i++) {
                Vector3 dir;
                dir = transform.position - pickUps[i].transform.position;
                pickUps[i].position += (dir * Time.deltaTime) * speed;
            }
        }


    }
}
