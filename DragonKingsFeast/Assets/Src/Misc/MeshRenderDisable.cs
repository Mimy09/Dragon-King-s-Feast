using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this simply turns off the mesh render on a game object
/// this is placed on the enemy spawner so that they do not show in game
/// but still show in the editor
/// 
/// <para> Author: Callum Dunstone </para>
/// 
/// </summary>
public class MeshRenderDisable : MonoBehaviour {

	/// <summary>
    /// 
    /// disables the mesh render at the start of the game
    /// 
    /// </summary>
	void Start () {
        GetComponent<MeshRenderer>().enabled = false;
	}
}
