using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerAxisSpeedMultiplyer : MonoBehaviour {

    public PlayerMovement playerMovemeant;

    public bool isX;

    public Slider slider;
    
    public void OnEnable() {
        if (isX) {
            slider.value = playerMovemeant.axisSpeedMultiplyer.x;
        }
        else {
            slider.value = playerMovemeant.axisSpeedMultiplyer.y;
        }
    }
}
