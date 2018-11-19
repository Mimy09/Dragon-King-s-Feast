using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Plays the background music
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class BackgroundMusic : MonoBehaviour {

    /// <summary>
    /// Time until next song plays
    /// </summary>
    float timer;

    /// <summary>
    /// Index of song playing
    /// </summary>
    int index = 0;

    /// <summary>
    /// List of the audio clips used
    /// </summary>
    List<AudioClip> audioList = new List<AudioClip>();

    /// <summary>
    /// Adds the audio clips to the audio manager
    /// </summary>
	void Start () {
        audioList.Add(Helper.Audio_Music_Level);
        audioList.Add(Helper.Audio_Music_Level2);
        audioList.Add(Helper.Audio_Music_Level3);
        audioList.Add(Helper.Audio_Music_Level4);
        GameManager.instance.GetAudioManager().volume = 0.3f;
        GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level);
        GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level2);
        GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level3);
        GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level4);
    }

    /// <summary>
    /// Changes the audio when song finishes
    /// </summary>
    void FixedUpdate () {
        if (timer > 0) {
            timer -= Time.fixedDeltaTime;
        } else {
            GameManager.instance.GetAudioManager().FadeOutMusic(audioList[index], 3);

            index++;
            if (index > audioList.Count) index = 0;

            GameManager.instance.GetAudioManager().PlayMusic(audioList[index], false);
            GameManager.instance.GetAudioManager().FadeInMusic(audioList[index], 3);
            timer = audioList[index].length;
        }
	}
}
