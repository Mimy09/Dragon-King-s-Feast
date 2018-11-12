using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {
    public float fps = 0.1f;
    public bool flip = false;
    public bool endGame = true;
    public List<Texture2D> endScreenAnim = new List<Texture2D>();
    RawImage rawImage;

    private void OnEnable() {
        rawImage = GetComponent<RawImage>();
        StartCoroutine(LoadAnim());
    }

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
