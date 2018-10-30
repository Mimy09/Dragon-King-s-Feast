using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour {

    public List<AudioClip> audioHolder;

    private void Awake() {
        int i = Random.Range(0, audioHolder.Count);
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioHolder.Count > 0) {
            audioSource.clip = audioHolder[i];
            audioSource.Play();
        }
        else if(audioSource == null) {
            Debug.LogError("NO AUDIO SOURCE FOUND FOR " + this.name);
        }
        else {
            Debug.LogError("NO AUDIO CLIPS FOR " + this.name);
        }
    }
}
