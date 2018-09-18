using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public Transform player;
    public float distanceFromPlayer;
    public float hightFromPlayer;

    [Header("bounds")]
    public float xbounds;
    public float ybounds;

    public bool realined;
    
    public float speed;

    public void Start() {
        realined = true;
    }

    public void Update() {

        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + distanceFromPlayer);

        if (realined == true) {
            Vector3 holder = player.transform.position;

            if (holder.x - transform.position.x > xbounds || holder.x - transform.position.x < -xbounds || holder.y - transform.position.y > ybounds || holder.y - transform.position.y < -ybounds) {
                realined = false;
            }
        }
        else {
            UpdatePosition();
        }
    }

    private void UpdatePosition() {
        Vector3 targetDir =  player.transform.position - transform.position;
        transform.position += (targetDir * Time.deltaTime * speed);
        Debug.Log(targetDir * Time.deltaTime * speed);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
    }
}
