using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

    //values that we use to figure out when to start slowing the players directional movement
    public float horizontalBounds;
    public float verticalBounds;

    //this value is used to determin how far the player is from the center before applying the slow down
    public float horizontalSlowDownOffset;
    public float VerticalSlowDownOffSet;

    //the base speed that we are moving the player forward
    public float forwardSpeed;

    //a multiplier that we modify the final speed given to the player
    public float moveSpeed;

    //how much the player will move this frame
    public Vector3 velocity;

    //these values are applied to the axis when moving the character up, down, left or right
    public Vector2 axisSpeedMultiplyer;

    public Vector3 startPos;

    public int tiltAngle;

    public GameObject neck;
    public float constraint;

    public float boostSpeed;

    public AnimationCurve aniCurve;
    public Vector2 num;

    [ReadOnly]
    private float inverse;
    public float Inverse {
        get {
            return inverse;
        }
    }

    private Player player;

    public void ResetPlayerPos() {
        transform.position = startPos;
    }

    // Use this for initialization
    void Start() {
        startPos = transform.position;
        inverse = -1;
        axisSpeedMultiplyer.y = 2.293255f;
        axisSpeedMultiplyer.x = 3.834884f;
        player = GetComponent<Player>();
    }

    public void ChangeXAxisSpeedMultiplyer(Slider slider) {
        axisSpeedMultiplyer.x = slider.value;
    }

    public void ChangeYAxisSpeedMultiplyer(Slider slider) {
        axisSpeedMultiplyer.y = slider.value;
    }

    // Update is called once per frame
    void LateUpdate() {

        //UpdateNeckBone();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ReadKeyBoardControls();
#endif

#if UNITY_ANDROID
        ReadPhoneControls();
#endif

        SpeedScale();
        

        Vector3 holder = ((velocity * Time.deltaTime) * moveSpeed);
        if (player.speedBoostTimer > player.speedBoostTime) {
            holder *= player.speedBoost;
        }

        velocity.z = forwardSpeed;

        transform.LookAt(Vector3.Normalize(velocity) + transform.position);
        transform.Rotate(new Vector3(90, 0, 0));

        // Horizontal Vertical

        GetComponent<Rigidbody>().velocity = (velocity * moveSpeed);
    }

    public void SpeedScale() {

        Vector3 pos = transform.position - startPos;
        float scale = 0;

        float horizontalValue = horizontalBounds - horizontalSlowDownOffset;
        float verticalValue = verticalBounds - VerticalSlowDownOffSet;

        if (velocity.x > 0) {
            if (pos.x > 0) {
                scale = (horizontalBounds - pos.x);

                if (scale < horizontalSlowDownOffset) {
                    velocity.x *= aniCurve.Evaluate(1 - (horizontalSlowDownOffset - scale) / horizontalSlowDownOffset);
                    /////////////////
                    num.x = aniCurve.Evaluate(1 - (horizontalSlowDownOffset - scale) / horizontalSlowDownOffset);
                }
            }
        }
        else {
            if (pos.x < 0) {
                scale = (horizontalBounds + pos.x);

                if (scale < horizontalSlowDownOffset) {
                    velocity.x *= aniCurve.Evaluate(1 - (horizontalSlowDownOffset - scale) / horizontalSlowDownOffset);
                    //////////////////
                    num.x = aniCurve.Evaluate(1 - (horizontalSlowDownOffset - scale) / horizontalSlowDownOffset);
                }
            }
        }

        if (velocity.y > 0) {
            if (pos.y > 0) {
                scale = (verticalBounds - pos.y);

                if (scale < VerticalSlowDownOffSet) {
                    velocity.y *= aniCurve.Evaluate(1 - (VerticalSlowDownOffSet - scale) / VerticalSlowDownOffSet);
                    //////////////////
                    num.y = aniCurve.Evaluate(1 - (VerticalSlowDownOffSet - scale) / VerticalSlowDownOffSet);
                }
            }
        }
        else {

            if (pos.y < 0) {
                scale = (verticalBounds + pos.y);

                if (scale < VerticalSlowDownOffSet) {
                    velocity.y *= aniCurve.Evaluate(1 - (VerticalSlowDownOffSet - scale) / VerticalSlowDownOffSet);
                    //////////////////
                    num.y = aniCurve.Evaluate(1 - (VerticalSlowDownOffSet - scale) / VerticalSlowDownOffSet);
                }
            }
        }
        
    }

    public void ReadKeyBoardControls() {

        Vector3 acceleration = new Vector3(velocity.x, velocity.y, 0);

        //Right
        if (Input.GetKey(KeyCode.D)) {
            if (acceleration.x < 2)
                acceleration.x += 1 * axisSpeedMultiplyer.x * Time.deltaTime;
        }

        //Left
        if (Input.GetKey(KeyCode.A)) {
            if (acceleration.x > -2)
                acceleration.x -= 1 * axisSpeedMultiplyer.x * Time.deltaTime;
        }

        //Up
        if (Input.GetKey(KeyCode.W)) {
            if (acceleration.y < 2)
                acceleration.y += 1 * axisSpeedMultiplyer.y * Time.deltaTime;
        }

        //Down
        if (Input.GetKey(KeyCode.S)) {
            if (acceleration.y > -2)
                acceleration.y -= 1 * axisSpeedMultiplyer.y * Time.deltaTime;
        }

        acceleration = Vector3.Lerp(acceleration, new Vector3(0,0,0), Time.deltaTime);


        velocity = (acceleration);
    }



    private void ReadPhoneControls() {
        //Get Speed based off Accelerometer
        Vector3 acelleromatorTiltValues = Quaternion.Euler(tiltAngle, 0, 0) * Input.acceleration;
        Vector3 Speed = Vector3.Scale(new Vector3(1, 1, 0), new Vector3(acelleromatorTiltValues.x, acelleromatorTiltValues.z * inverse, acelleromatorTiltValues.y));
        
        //apply speed modifiers
        Speed.x *= axisSpeedMultiplyer.x;
        Speed.y = (-Speed.y * axisSpeedMultiplyer.y);

        //apply the speed to the velocity
        velocity = Speed;
    }

    public void InverseTilt() {
        if (inverse == 1) {
            inverse = -1;
        }
        else {
            inverse = 1;
        }
    }

    public void SetTilt() {
        int holder = (int)(Input.acceleration.z * 90);
        //holder *= -1;

        tiltAngle = holder;

        Debug.Log(Input.acceleration.z);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 1, 0.4f);
        if (startPos == new Vector3())
            Gizmos.DrawCube(transform.position + (transform.forward * 600), new Vector3(horizontalBounds * 2, verticalBounds * 2, 1200));
        else
            Gizmos.DrawCube(startPos + (transform.forward * 600), new Vector3(horizontalBounds * 2, verticalBounds * 2, 1200));

        Gizmos.color = Color.red;
        Gizmos.DrawSphere( Vector3.Normalize(velocity) + transform.position, 0.1f);
    }
}
