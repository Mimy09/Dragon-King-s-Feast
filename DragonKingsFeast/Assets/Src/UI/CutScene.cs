using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Controls the cut scene that plays at the start of level.
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class CutScene : MonoBehaviour {
    /// <summary>
    /// The time in between slides
    /// </summary>
    public float TimePerSlide;

    /// <summary>
    /// List of the slides for the cut scene
    /// </summary>
    public List<Texture2D> texture_list = new List<Texture2D>();

    /// <summary>
    /// Image used to display the cut scene
    /// </summary>
    RawImage image;

    /// <summary>
    /// Sets the image and starts the cut scene when enabled
    /// </summary>
    private void OnEnable() {
        image = GetComponent<RawImage>();
        StartCoroutine(RunCutScene());
    }

    /// <summary>
    /// Loops through the cut scene using a coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator RunCutScene() {
        for (int i = 0; i < texture_list.Count; i++) {
            image.texture = texture_list[i];
            yield return new WaitForSecondsRealtime(TimePerSlide);
        }

        __event<e_UI>.InvokeEvent(this, e_UI.LOADING);
    }
}
