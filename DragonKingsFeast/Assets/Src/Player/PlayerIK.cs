using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour {

    public GameObject bone_parent;
    public GameObject head;
    public GameObject[] bones;

    public float wiggleX, wiggleY;
    public float spread;
    public float spring;

    Vector3 Mult(Vector3 v1, Vector3 v2) {
        return new Vector3(v1.x * v1.x, v1.y * v1.y, v1.z * v1.z);
    }


	void Start () {
        bones = new GameObject[bone_parent.transform.childCount];

        for (int i = 0; i < bone_parent.transform.childCount; i++) {
            bones[i] = bone_parent.transform.GetChild(i).gameObject;
        }
	}
	
	void Update () {
        bones[0].transform.position = new Vector3(
                head.transform.position.x,
                head.transform.position.y,
                head.transform.position.z
                );

        for (int i = bones.Length - 1; i > 0; i--) {
            if (Vector3.Distance(bones[i].transform.position, bones[i - 1].transform.position) < spread) continue;
            bones[i].transform.position = Vector3.Lerp(
                bones[i].transform.position,
                new Vector3(
                    bones[i - 1].transform.position.x,
                    bones[i - 1].transform.position.y,
                    bones[i - 1].transform.position.z
                ),
                Time.deltaTime * spring
            );
            bones[i].transform.position += new Vector3(
                (Mathf.Sin((-Time.time * wiggleY) /*+ (i * 0.05f)*/) * 0.1f) * (/*(i - 7) **/ 0.03f),
                (Mathf.Cos((-Time.time * wiggleX) /*+ (i * 0.05f)*/) * 0.1f) * (/*(i - 7) **/ 0.03f),
                0
                );
        }

        bones[0].transform.LookAt(head.transform);

        for (int i = 1; i < bones.Length; i++) {
            bones[i].transform.LookAt(bones[i - 1].transform);
            Quaternion q = bones[i].transform.rotation;
            q.eulerAngles += new Vector3(0, -90, 0);
            bones[i].transform.rotation = q;
        }


    }
}
