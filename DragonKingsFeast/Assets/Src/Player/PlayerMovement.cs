using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        ReadPhoneControls();
        SpeedScale();

        transform.Translate((velocity * Time.deltaTime) * moveSpeed);

    }

    public void SpeedScale() {

        Vector3 pos = transform.position - startPos;
        float scale = 0;

        if (velocity.z > 0) {
            if (pos.z > 0) {
                scale = (horizontalBounds - pos.z) / horizontalBounds;
                velocity *= scale;
            }
        }
        else {

            if (pos.z < 0) {
                scale = (horizontalBounds + pos.z) / horizontalBounds;
                velocity *= scale;
            }
        }

        if (velocity.y > 0) {
            if (pos.y > 0) {
                scale = (verticalBounds - pos.y) / verticalBounds;
                velocity *= scale;
            }
        }
        else {

            if (pos.y < 0) {
                scale = (verticalBounds + pos.y) / verticalBounds;
                velocity *= scale;
            }
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
