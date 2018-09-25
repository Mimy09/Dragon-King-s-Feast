using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TiltOnOff : MonoBehaviour {

    public Sprite on;
    public Sprite off;

    public PlayerMovement playerMovement;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    void Update () {
        if (playerMovement.Inverse == -1) {
            image.sprite = off;
        }
        else {
            image.sprite = on;
        }
	}
}
