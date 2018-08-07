using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {

    //valuse that we use to figure out when to start slowing the players directional movemeant
    public float horizontalBounds;
    public float verticalBounds;

    //the base speed that we are moving the player forward
    public float forwardSpeed;

    //a multiplyer that we modifie the finall speed given to the player
    public float moveSpeed;

    
	// Use this for initialization
	void Start () {
		
        

	}
	
	// Update is called once per frame
	void Update () {
		


	}
}
