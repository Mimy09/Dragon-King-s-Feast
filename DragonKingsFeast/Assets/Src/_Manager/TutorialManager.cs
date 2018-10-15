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
        if (!CloudAheadComp || !LearnToFlyComp) Time.timeScale = timeScail;

        if (LearnToFly) {
            StopTime();
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.TILT);

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (Input.GetMouseButtonDown(1)) {
                __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                LearnToFly = false;
                CloudAhead = true;
            }
#endif

#if UNITY_ANDROID
            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began) {
                    __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                    LearnToFly = false;
                    CloudAhead = true;
                }
            }
#endif
        }
        else if (!LearnToFlyComp) {
            StartTime();
            if (Time.timeScale == 1) LearnToFlyComp = true;
        }




        if (CloudAhead && LearnToFlyComp) {
            StopTime();
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.TAP);

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (Input.GetMouseButtonDown(1)) {
                __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                CloudAhead = false;
            }
#endif

#if UNITY_ANDROID

            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began) {
                    __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);
                    CloudAhead = false;
                }
            }
#endif
        }
        else if (!CloudAheadComp && LearnToFlyComp) {
            StartTime();
            if (Time.timeScale == 1) CloudAheadComp = true;
        }



    }

    private void StopTime() {
        if (timeScail > 0) timeScail -= Time.fixedDeltaTime;
        if (timeScail < 0.1f && timeScail != 0) timeScail = 0;
    }
    private void StartTime() {
        if (timeScail < 1) timeScail += Time.fixedDeltaTime * 10;
        if (timeScail > 0.9f && timeScail != 1) timeScail = 1;

    }
}
