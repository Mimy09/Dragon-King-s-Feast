using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Shows the health / score value in a UI element
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class ShowHealth : MonoBehaviour {

    /// <summary>
    /// UI element to edit
    /// </summary>
    public UnityEngine.UI.Text text;

    /// <summary>
    /// Updates the UI element with the health value
    /// </summary>
    void Update () {
        text.text = GameManager.player.health.ToString();
    }
}
