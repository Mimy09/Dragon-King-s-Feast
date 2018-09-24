using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHealth : MonoBehaviour {

    public UnityEngine.UI.Text text;
    
	void Update () {
        text.text = GameManager.player.health.ToString();
    }
}
