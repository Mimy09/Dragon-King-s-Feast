using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Shows the loading percent in a UI element
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class Loader : MonoBehaviour {

    /// <summary>
    /// UI element to edit
    /// </summary>
    private Text percent;

    /// <summary>
    /// Sets up the UI element
    /// </summary>
    private void Start() {
        percent = GetComponent<Text>();
    }


    /// <summary>
    /// Updates the UI element with the loading percent
    /// </summary>
    void Update () {
        percent.text = "Loading: " + GameManager.instance.GetMapManager().loaded_percent.ToString() + "%";
	}
}
