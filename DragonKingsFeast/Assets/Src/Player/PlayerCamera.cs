using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour {

    public Transform player;
    public float distanceFromPlayer;
    public float hightFromPlayer;

    public float speed;

    [Header("Debugning")]
    public Text debugText;

    private Vector3 targtPos;
    private Vector3 camVelocity = Vector3.zero;

    public void Start() {
    }

    public void Update() {

    }
    private void FixedUpdate() {
        //Vector3 targetDir = targtPos - transform.position;
        ////transform.position += (targetDir * Time.deltaTime * speed);

        //transform.position = Vector3.Lerp(transform.position, targtPos, Time.smoothDeltaTime * speed);

        //Vector3 t = transform.position;
        //t.z = player.transform.position.z + distanceFromPlayer;
        //transform.position = t;

        targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
        transform.position = Vector3.SmoothDamp(transform.position, targtPos, ref camVelocity, speed, Mathf.Infinity, Time.smoothDeltaTime);

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targtPos, 0.1f);

    }
}
