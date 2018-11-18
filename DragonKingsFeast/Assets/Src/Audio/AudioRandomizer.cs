using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: AudioRandomizer
* ----------------------
*
* this class is used to play a randomized sound on awake, this is normally used on 
* partical effects and the such that are meant to have a short life
*
* Author: Callum Dunstone
*/
public class AudioRandomizer : MonoBehaviour {

    //this holds all of the diffrent audio clips that can be played on awake
    public List<AudioClip> audioHolder;

    /*
    * Function: Awake
    * ---------------
    *
    * This is a unity monobehaviour base function
    *
    *  this function gets called when the object gets called, it then finds an audio
    *  source on the game object it is attached to and assigns it a random audio clip to play
    * 
    * Author: Callum Dunstone
    */
    private void Awake() {
        //gets the audio source and a random audio clip to play
        int i = Random.Range(0, audioHolder.Count);
        AudioSource audioSource = GetComponent<AudioSource>();

        //check that we have both the audio source a possible audio clip then assign and play the clip else through an error
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
