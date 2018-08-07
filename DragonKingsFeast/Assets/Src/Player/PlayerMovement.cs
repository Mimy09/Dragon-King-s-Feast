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

    //how much the player will move this frame
    public Vector3 velocity;

    public Vector3 startPos;
    
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        

	}
	
	// Update is called once per frame
	void Update () {
		


	}

    public void SpeedScale() {
        
        if (velocity.z > 0) {
            //pos
        }
        else {
            
        }

        if (velocity.y > 0) {

        }
        else {

        }
    }

    private void ReadPhoneControls() {

        Vector3 acceleration = new Vector3(0, 0, 0);
        
        //////////////////Right, Left/////////////////

        float xHolder = -Input.acceleration.x;

        acceleration.z = xHolder;

        Debug.Log("running, X = " + acceleration);

        //////////////////Up, down/////////////////
        
            float tiltvalue = Input.acceleration.z;

            Debug.Log("running, X = " + tiltvalue);
        
            acceleration.y = -tiltvalue;
        

        velocity = (acceleration);

    }
}
