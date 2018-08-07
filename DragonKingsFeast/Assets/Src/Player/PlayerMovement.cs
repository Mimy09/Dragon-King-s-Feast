using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    //valuse that we use to figure out when to start slowing the players directional movemeant
    public float horizontalBounds;
    public float verticalBounds;

    //this value is used to determin how far the player is from the center before applying the slow down
    public float slowDownOffSet;

    //the base speed that we are moving the player forward
    public float forwardSpeed;

    //a multiplyer that we modifie the finall speed given to the player
    public float moveSpeed;

    //how much the player will move this frame
    public Vector3 velocity;

    //these values are applyed to the axis when moving thecharacter up, down, left or right
    public Vector2 axisSpeedMultiplyer;

    public Vector3 startPos;
    
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        

	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ReadKeyBoardControls();
#endif

#if UNITY_ANDROID
        ReadPhoneControls();
#endif

        SpeedScale();

        velocity.z += forwardSpeed;

        transform.Translate((velocity * Time.deltaTime) * moveSpeed);

    }

    public void SpeedScale() {

        Vector3 pos = transform.position - startPos;
        float scale = 0;

        float horizontalValue = horizontalBounds - slowDownOffSet;
        float verticalValue = verticalBounds - slowDownOffSet;

        if (velocity.x > 0) {
            if (pos.x > 0) {
                scale = (horizontalBounds - pos.x);

                if (scale < slowDownOffSet) {
                    velocity.x *= 1 - (slowDownOffSet - scale) / slowDownOffSet;
                }
            }
        }
        else {

            if (pos.x < 0) {
                scale = (horizontalBounds + pos.x);
                if (scale < slowDownOffSet) {
                    velocity.x *= 1 - (slowDownOffSet - scale) / slowDownOffSet;
                }
            }
        }

        if (velocity.y > 0) {
            if (pos.y > 0) {
                scale = (verticalBounds - pos.y);

                if (scale < slowDownOffSet) {
                    velocity.y *= 1 - (slowDownOffSet - scale) / slowDownOffSet;
                }
            }
        }
        else {

            if (pos.y < 0) {
                scale = (verticalBounds + pos.y);

                if (scale < slowDownOffSet) {
                    velocity.y *= 1 - (slowDownOffSet - scale) / slowDownOffSet;
                }
            }
        }
    }

    public void ReadKeyBoardControls() {

        Vector3 acceleration = new Vector3(0, 0, 0);

        //Right
        if (Input.GetKey(KeyCode.D)) {
            acceleration.x += 1 * axisSpeedMultiplyer.x;
        }

        //Left
        if (Input.GetKey(KeyCode.A)) {
            acceleration.x -= 1 * axisSpeedMultiplyer.x;
        }

        //Up
        if (Input.GetKey(KeyCode.W)) {
            acceleration.y += 1 * axisSpeedMultiplyer.y;
        }

        //Down
        if (Input.GetKey(KeyCode.S)) {
            acceleration.y -= 1 * axisSpeedMultiplyer.y;
        }

        velocity = (acceleration);
    }



    private void ReadPhoneControls() {

        Vector3 acceleration = new Vector3(0, 0, 0);
        
        //////////////////Right, Left/////////////////

        float xHolder = (Input.acceleration.x * axisSpeedMultiplyer.x);

        Debug.Log("x: " + Input.acceleration);
        //Debug.Log("y: " + Input.acceleration.y);
        //Debug.Log("z: " + Input.acceleration.z);
        
        acceleration.x = xHolder;
        
        //////////////////Up, down/////////////////
        
        float tiltvalue = Input.acceleration.z * axisSpeedMultiplyer.y;
        
        acceleration.y = -tiltvalue;
        
        //////////////////////////////////////////

        velocity = (acceleration);
    }
}
