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
        Debug.Log("UNITY EDITOR CODE RUNNING");
#endif

#if UNITY_ANDROID
        ReadPhoneControls();
        Debug.Log("ANDROID CODE RUNNING");
#endif

        SpeedScale();

        transform.Translate((velocity * Time.deltaTime) * moveSpeed);

    }

    public void SpeedScale() {

        Vector3 pos = transform.position - startPos;
        float scale = 0;

        float horizontalValue = horizontalBounds - slowDownOffSet;
        float verticalValue = verticalBounds - slowDownOffSet;

        if (velocity.x > 0) {
            if (pos.x > 0) {
                scale = (horizontalBounds - pos.x) / horizontalBounds;
                velocity *= scale;
            }
        }
        else {

            if (pos.x < 0) {
                scale = (horizontalBounds + pos.x) / horizontalBounds;
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

        acceleration.x = xHolder;
        
        //////////////////Up, down/////////////////
        
        float tiltvalue = Input.acceleration.z * axisSpeedMultiplyer.y;
        
        acceleration.y = -tiltvalue;
        
        //////////////////////////////////////////

        velocity = (acceleration);
    }
}
