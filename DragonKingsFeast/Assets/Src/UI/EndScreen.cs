using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class EndScreen : MonoBehaviour {

    /// <summary>
    /// time in between each frame
    /// </summary>
    public float fps = 0.1f;
    
    /// <summary>
    /// Makes the frames play backwards
    /// </summary>
    public bool flip = false;

    /// <summary>
    /// Sets weather this plays the death end screen or the win screen
    /// </summary>
    public bool endGame = true;

    /// <summary>
    /// List of all the images to make up the animation
    /// </summary>
    public List<Texture2D> endScreenAnim = new List<Texture2D>();

    /// <summary>
    /// UI element to edit to show textures on screen
    /// </summary>
    RawImage rawImage;

    /// <summary>
    /// Sets the image and starts the end scene when enabled
    /// </summary>
    private void OnEnable() {
        rawImage = GetComponent<RawImage>();
        StartCoroutine(LoadAnim());
    }

    /// <summary>
    /// Loops through the end scene using a coroutine. Then unloads the levels when done.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAnim() {
        if (flip) {
            for (int i = 0; i < endScreenAnim.Count; i++) {
                rawImage.texture = endScreenAnim[i];
                yield return new WaitForSecondsRealtime(fps);
            }
        } else {
            for (int i = endScreenAnim.Count - 1; i >= 0; i--) {
                rawImage.texture = endScreenAnim[i];
                yield return new WaitForSecondsRealtime(fps);
            }
        }
        if (endGame) {
            GameManager.instance.GetMapManager().isLoaded_level1 = true;
            GameManager.instance.GetMapManager().isLoaded_level2 = true;
            GameManager.instance.GetMapManager().isLoaded_level3 = true;
            GameManager.instance.GetMapManager().UnloadLevel(1);
            GameManager.instance.GetMapManager().UnloadLevel(2);
            GameManager.instance.GetMapManager().UnloadLevel(3);
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
