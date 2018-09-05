using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

    private Text percent;

    private void Start() {
        percent = GetComponent<Text>();
    }

    void Update () {
        percent.text = "Loading: " + GameManager.instance.GetMapManager().loaded_percent.ToString() + "%";
	}
}
