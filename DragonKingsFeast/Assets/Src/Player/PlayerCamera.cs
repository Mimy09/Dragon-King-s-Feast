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
    private Vector3 vel = Vector3.zero;

    public void Start() {
    }

    public void Update() {

        targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);

        //Vector3 targetDir = targtPos - transform.position;
        ////transform.position += (targetDir * Time.deltaTime * speed);

        //transform.position = Vector3.Lerp(transform.position, targtPos, Time.smoothDeltaTime * speed);

        //Vector3 t = transform.position;
        //t.z = player.transform.position.z + distanceFromPlayer;
        //transform.position = t;

        Vector3 point = Camera.main.WorldToViewportPoint(targtPos);
        Vector3 delta = targtPos - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref vel, Time.smoothDeltaTime);
    }

    private void UpdatePosition() {
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targtPos, 0.1f);

    }
}
