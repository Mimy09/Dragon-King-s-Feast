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

    public int tiltAngle;

    public GameObject neck;
    public float constraint;

    public float boostSpeed;

    [ReadOnly]
    private float inverse;

    private Player player;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
        inverse = -1;

        player = GameManager.player;
    }

    void UpdateNeckBone() {
        Quaternion r = neck.transform.rotation;
        Vector3 e = r.eulerAngles;

        e.x = 0;
        e.y = -90 + (velocity.x * constraint);
        e.z = velocity.y * constraint;

        r.eulerAngles = e;
        neck.transform.rotation = r;

        Vector3 t = neck.transform.localPosition;
        t.x += Mathf.Sin(-Time.time) * 0.05f - t.x;
        t.z += Mathf.Sin(Time.time * 2) * 0.05f - t.z;
        neck.transform.localPosition = t;
    }

    // Update is called once per frame
    void Update() {

        UpdateNeckBone();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ReadKeyBoardControls();
#endif

#if UNITY_ANDROID
        ReadPhoneControls();
#endif

        SpeedScale();

        velocity.z += forwardSpeed;

        Vector3 holder = ((velocity * Time.deltaTime) * moveSpeed);
        if (player.speedBoostTimer > player.speedBoostTime) {
            holder *= player.speedBoost;
        }

        //GetComponent<Rigidbody>().AddForce(velocity * moveSpeed);
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

        //Get Speed based off Acelleromator
        Vector3 acelleromatorTiltValues = Quaternion.Euler(tiltAngle, 0, 0) * Input.acceleration;
        Vector3 Speed = Vector3.Scale(new Vector3(1, 1, 0), new Vector3(acelleromatorTiltValues.x, acelleromatorTiltValues.z * inverse, acelleromatorTiltValues.y));

        //apply speed modifiers
        Speed.x *= axisSpeedMultiplyer.x;
        Speed.y = (-Speed.y * axisSpeedMultiplyer.y);

        //apply the speed to the velocity
        velocity = (Speed);
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
}
