using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public float timeScail = 1;
    private bool start = false;

    public bool LearnToFly = false;
    public bool LearnToFlyComp = false;

    private Vector3 tiltAngle;

    public bool CloudAhead = false;
    public bool CloudAheadComp = false;
    GameObject enemy;

    public bool LearnPause = false;
    public bool GakiAhead = false;

    public void Init() {
        GameManager.instance.GetPlayer().gameObject.transform.position -= new Vector3(0, 0, 50);
        start = true;
        LearnToFly = true;
    }

    private void Update() {
        if (!start) return;
        Time.timeScale = timeScail;

        if (LearnToFly) {
            StopTime();
            GameManager.instance.GetPlayerIK().MoveBone();
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.TILT);

            if (Input.GetMouseButtonDown(1) || Input.GetTouch(0).tapCount > 0) {
                __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                LearnToFly = false;
                CloudAhead = true;
            }

        } else if (!LearnToFlyComp) {
            StartTime();
            if (Time.timeScale == 1) LearnToFlyComp = true;
        }

        if (CloudAhead && LearnToFlyComp) {
            StopTime();
            GameManager.instance.GetPlayerIK().MoveBone();
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.TAP);

            if (Input.GetMouseButtonDown(1) || Input.GetTouch(0).tapCount > 0) {
                __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                CloudAhead = false;
            }

        }
        else if (!CloudAheadComp && LearnToFlyComp) {
            StartTime();
            if (Time.timeScale == 1) CloudAheadComp = true;
        }

    }

    private void StopTime() {
        if (timeScail > 0) timeScail -= Time.deltaTime;
        else if (timeScail < 0.01f && timeScail != 0) timeScail = 0;
    }
    private void StartTime() {
        if (timeScail < 1) timeScail += Time.deltaTime * 10;
        else if (timeScail > 0.99f && timeScail != 1) timeScail = 1;

    }
}
