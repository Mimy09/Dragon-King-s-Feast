using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

    /// <summary>
    /// values that we use to figure out when to start slowing the players directional movement
    /// </summary>
    public float horizontalBounds, verticalBounds;

    /// <summary>
    /// this value is used to determine how far the player is from the center before applying the slow down
    /// </summary>
    public float horizontalSlowDownOffset, VerticalSlowDownOffSet;

    /// <summary>
    /// the base speed that we are moving the player forward
    /// </summary>
    public float forwardSpeed;

    /// <summary>
    /// a multiplier that we modify the final speed given to the player
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// how much the player will move this frame
    /// </summary>
    public Vector3 velocity;

    /// <summary>
    /// these values are applied to the axis when moving the character up, down, left or right
    /// </summary>
    public Vector2 axisSpeedMultiplyer;

    /// <summary>
    /// The position where the player started
    /// </summary>
    public Vector3 startPos;

    /// <summary>
    /// The angle that the device uses as flying level with the environment.
    /// </summary>
    public int tiltAngle;

    /// <summary>
    /// Used to rotate the neck of the player dragon
    /// </summary>
    public Transform neck;

    /// <summary>
    /// The curve used to slow down the player when reaching out of bounds
    /// </summary>
    public AnimationCurve aniCurve;

    /// <summary>
    /// Holder value for acceleration
    /// </summary>
    public Vector2 num;

    /// <summary>
    /// Flips the movement controls to inverse (-1, 1)
    /// </summary>
    [ReadOnly] private float inverse;
    public float Inverse {
        get {
            return inverse;
        }
    }

    /// <summary>
    /// Reference to the Player script
    /// </summary>
    private Player player;

    /// <summary>
    /// Resets the player position to the start position
    /// </summary>
    public void ResetPlayerPos() {
        transform.position = startPos;
    }

    /// <summary>
    /// initialization of the variables
    /// </summary>
    void Start() {
        startPos = transform.position;
        inverse = -1;
        axisSpeedMultiplyer.y = 2.293255f;
        axisSpeedMultiplyer.x = 3.834884f;
        player = GetComponent<Player>();
    }

    /// <summary>
    /// sets the speed X multiplier to the sliders in the settings
    /// </summary>
    /// <param name="slider"></param>
    public void ChangeXAxisSpeedMultiplyer(Slider slider) {
        axisSpeedMultiplyer.x = slider.value;
    }

    /// <summary>
    /// sets the speed Y multiplier to the sliders in the settings
    /// </summary>
    /// <param name="slider"></param>
    public void ChangeYAxisSpeedMultiplyer(Slider slider) {
        axisSpeedMultiplyer.y = slider.value;
    }

    /// <summary>
    /// Updates the neck rotation
    /// </summary>
    private void Update() {
        Vector3 v = neck.localPosition;
        v.x = Mathf.Sin(Time.time * 5) / 5;
        neck.localPosition = v;
    }

    /// <summary>
    /// Updates the players movement
    /// </summary>
    void LateUpdate() {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ReadKeyBoardControls();
#endif

#if UNITY_ANDROID
        ReadPhoneControls();
#endif

        SpeedScale();
        

        Vector3 holder = ((velocity * Time.deltaTime) * moveSpeed);

        velocity.z = forwardSpeed;

        transform.LookAt(Vector3.Normalize(velocity) + transform.position);
        transform.Rotate(new Vector3(90, 0, 0));

        

        // Horizontal Vertical
        GetComponent<Rigidbody>().velocity = (velocity * moveSpeed);
    }
    
    /// <summary>
    /// Scales the speed based on the distance form the boundaries
    /// </summary>
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
                else {
                    num.x = 1;
                }
            }
            else {
                num.x = 1;
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
                else {
                    num.x = 1;
                }
            }
            else {
                num.x = 1;
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
                else {
                    num.y = 1;
                }
            }
            else {
                num.y = 1;
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
                else {
                    num.y = 1;
                }
            }
            else {
                num.y = 1;
            }
        }
        
    }

    /// <summary>
    /// Reads the keyboard input controls (WASD)
    /// </summary>
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

    /// <summary>
    /// Reads the phone controls (accelerometer)
    /// </summary>
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

    /// <summary>
    /// Inverses the tilt of the accelerometer
    /// </summary>
    public void InverseTilt() {
        if (inverse == 1) {
            inverse = -1;
        }
        else {
            inverse = 1;
        }
    }

    /// <summary>
    /// Sets the tilt level
    /// </summary>
    public void SetTilt() {
        int holder = (int)(Input.acceleration.z * 90);
        //holder *= -1;

        tiltAngle = holder;

        Debug.Log(Input.acceleration.z);
    }

    /// <summary>
    /// Draws the boundary box around the player
    /// </summary>
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
