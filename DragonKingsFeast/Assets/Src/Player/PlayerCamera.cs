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

        if (GameManager.playerMovement.num.x != 1 || GameManager.playerMovement.num.y != 1) {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targtPos - new Vector3(
                    ((GameManager.playerMovement.velocity.x > 0 ? 1 : 0) - GameManager.playerMovement.num.x) * 2,
                    ((GameManager.playerMovement.velocity.y > 0 ? 1 : 0) - GameManager.playerMovement.num.y) * 2,
                    0),
                ref camVelocity,
                speed,
                Mathf.Infinity,
                Time.smoothDeltaTime
                );
        } else {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targtPos,
                ref camVelocity,
                speed,
                Mathf.Infinity,
                Time.smoothDeltaTime
                );
        }
        // transform.LookAt(GameManager.instance.GetPlayer().transform.position);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targtPos, 0.1f);
    }
}
