using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Controls the camera that follows the player
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class PlayerCamera : MonoBehaviour {

    /// <summary>
    /// Reference to the players transform
    /// </summary>
    public Transform player;

    /// <summary>
    /// The distance from the player
    /// </summary>
    public float distanceFromPlayer;

    /// <summary>
    /// The hight from the player
    /// </summary>
    public float hightFromPlayer;

    /// <summary>
    /// The speed of the following
    /// </summary>
    public float speed;
    
    /// <summary>
    /// The position the camera follow
    /// </summary>
    private Vector3 targtPos;

    /// <summary>
    /// the cameras velocity
    /// </summary>
    private Vector3 camVelocity = Vector3.zero;

    /// <summary>
    /// Is the player camera colliding with any walls
    /// </summary>
    public bool isHittingWall = false;

    /// <summary>
    /// Updates the cameras position.
    /// Also avoids walls.
    /// </summary>
    private void FixedUpdate() {
        if (isHittingWall) {
            targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z - 1);
        } else {
            targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
        }

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
    }

    /// <summary>
    /// Draw gizmos where camera is targating
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targtPos, 0.1f);
    }

    /// <summary>
    /// Did camera collide with mountain
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Mountain") {
            isHittingWall = true;
        }

    }

    /// <summary>
    /// Did camera stop colliding with mountain
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Mountain") {
            isHittingWall = false;
        }
    }
}
