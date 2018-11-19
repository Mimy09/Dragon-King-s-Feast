using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Shows the FPS in a UI element.
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class FPS : MonoBehaviour {
    /// <summary>
    /// UI element to edit
    /// </summary>
    Text canvas_text;

    /// <summary>
    /// Time in between frames
    /// </summary>
    float deltaTime = 0.0f;

    /// <summary>
    /// Sets up the UI element
    /// </summary>
    void Start () {
        canvas_text = this.GetComponent<Text>();
    }
	
    /// <summary>
    /// Updates the delta time
    /// </summary>
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        canvas_text.text = text;
    }
}
