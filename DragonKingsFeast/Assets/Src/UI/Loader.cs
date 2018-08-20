using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

    public Text percent;

    private void Awake() {
        Time.timeScale = 0;
    }

    public void StratGame() {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    void Update () {
        percent.text = "Loading: " + GameManager.instance.GetMapManager().loaded_percent.ToString() + "%";
	}
}
