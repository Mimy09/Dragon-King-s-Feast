using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 
/// Shows the on/off state of the inverse tilt
/// 
/// <para>
/// Author: Callum
/// </para>
/// 
/// </summary>
public class TiltOnOff : MonoBehaviour {

    /// <summary>
    /// Sprite to show when the inverse tilt is on
    /// </summary>
    public Sprite on;
    /// <summary>
    /// Sprite to show when the inverse tilt is off
    /// </summary>
    public Sprite off;

    /// <summary>
    /// Reference to the player movement
    /// </summary>
    public PlayerMovement playerMovement;

    /// <summary>
    /// UI elememt to edit
    /// </summary>
    private Image image;

    /// <summary>
    /// Sets up the UI element
    /// </summary>
    private void Start() {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Updates the UI element with what needs to be shown
    /// </summary>
    void Update () {
        if (playerMovement.Inverse == -1) {
            image.sprite = off;
        }
        else {
            image.sprite = on;
        }
	}
}
