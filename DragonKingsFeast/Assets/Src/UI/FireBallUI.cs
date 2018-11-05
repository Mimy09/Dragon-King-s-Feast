using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallUI : MonoBehaviour {
    public List<Texture2D> fireballs;
    RawImage fireballImage;

    private void Start() {
        fireballImage = GetComponent<RawImage>();
    }

    void Update () {
        if (fireballs.Count != 6) return;

        fireballImage.texture = fireballs[(int)GameManager.player.bulletAmmount];
	}
}
