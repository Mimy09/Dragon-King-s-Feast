using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {
    public float TimePerSlide;
    public List<Texture2D> texture_list = new List<Texture2D>();

    RawImage image;

    private void OnEnable() {
        image = GetComponent<RawImage>();
        StartCoroutine(RunCutScene());
    }

    IEnumerator RunCutScene() {
        for (int i = 0; i < texture_list.Count; i++) {
            image.texture = texture_list[i];
            yield return new WaitForSecondsRealtime(TimePerSlide);
        }

        __event<e_UI>.InvokeEvent(this, e_UI.LOADING);
    }
}
