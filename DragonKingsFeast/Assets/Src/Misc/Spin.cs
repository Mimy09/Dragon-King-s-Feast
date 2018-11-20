using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// this script is used to slowly roatate things in game
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
public class Spin : MonoBehaviour {

    /// <summary> how fast you want them to roatate </summary>
    public float speed;
	
    /// <summary>
    /// 
    /// this is what roates the game object
    /// 
    /// </summary>
	void Update () {
        this.transform.Rotate(new Vector3(0, 0, speed * Time.fixedUnscaledDeltaTime));
    }
}
