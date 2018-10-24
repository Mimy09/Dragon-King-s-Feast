using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour {

    public Transform player;
    public float distanceFromPlayer;
    public float hightFromPlayer;

    public float speed;

    [Header("Debugging")]
    public Text debugText;

    private Vector3 targtPos;
    private Vector3 camVelocity = Vector3.zero;

    private void FixedUpdate() {
        targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
        transform.position = Vector3.SmoothDamp(transform.position, targtPos, ref camVelocity, speed, Mathf.Infinity, Time.smoothDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targtPos, 0.1f);
    }
}
