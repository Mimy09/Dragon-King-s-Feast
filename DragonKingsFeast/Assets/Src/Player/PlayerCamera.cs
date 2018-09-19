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


    public void Start() {
    }

    public void Update() {

        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + distanceFromPlayer);
        
        UpdatePosition();
    }

    private void UpdatePosition() {
        Vector3 targtPos = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
        Vector3 targetDir = targtPos - transform.position;
        transform.position += (targetDir * Time.deltaTime * speed);

       //debugText.text = targetDir.ToString();
       //debugText.text += "\n" + targetDir * Time.deltaTime;
       //debugText.text +=  "\n" + ((targetDir * Time.deltaTime) * speed).ToString();

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y - hightFromPlayer, player.transform.position.z + distanceFromPlayer);
    }
}
