using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour 
{
    public GameObject three;
    public GameObject fithteen;

    public bool spawnThree;

    public int spawnTimes;

    public void Start() {
        GameObject go = null;

        for (int i = 0; i < spawnTimes; i++) {
            if (spawnThree) {
                go = Instantiate(three);
            }
            else {
                go = Instantiate(fithteen);
            }

            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0, 0, 0);
        }
    }
}
