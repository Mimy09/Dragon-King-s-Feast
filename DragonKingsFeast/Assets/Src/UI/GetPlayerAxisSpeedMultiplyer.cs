using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Changes the X and Y movement using a UI slider
/// 
/// <para>
/// Author: Callum
/// </para>
/// 
/// </summary>
public class GetPlayerAxisSpeedMultiplyer : MonoBehaviour {

    /// <summary>
    /// Reference to the player movement
    /// </summary>
    public PlayerMovement playerMovemeant;

    /// <summary>
    /// Is this editing the x or y axis
    /// </summary>
    public bool isX;

    /// <summary>
    /// UI element to edit
    /// </summary>
    public Slider slider;
    
    /// <summary>
    /// Change the clider to the inital value of the player movement
    /// </summary>
    public void OnEnable() {
        if (isX) {
            slider.value = playerMovemeant.axisSpeedMultiplyer.x;
        }
        else {
            slider.value = playerMovemeant.axisSpeedMultiplyer.y;
        }
    }
}
