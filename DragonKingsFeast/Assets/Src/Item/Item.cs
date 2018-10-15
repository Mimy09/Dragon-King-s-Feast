using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum e_ItemType {
    Boost_Speed,
    Boost_Attack,
    Boost_Defense,
    Pickup,
}

public class Item : MonoBehaviour {
    public int value = 1;
    public e_ItemType m_itemType;
    public e_ItemType ItemType { get { return m_itemType; } }
    public bool spawner = false;
    
    public virtual void TurnOff() {
        Reset();
        GameManager.instance.GetObjectPool().AddItemTooPool(this);
        GameManager.itemList.Remove(gameObject);
    }
    public virtual void TurnOn() {
        Reset();
        GameManager.itemList.Add(gameObject);
        this.gameObject.SetActive(true);
    }
    public virtual void Reset() { }

    public void OnTriggerEnter(Collider other) {
        if (spawner == false) return;

        if (other.tag == "Player") {
            if (m_itemType != e_ItemType.Pickup) {
                GameManager.player.ApplyBoost(m_itemType);
            }
            else {
                GameManager.player.ApplyLoot(value);
                Transform t = GameObject.FindGameObjectWithTag("CoinSP").transform;
                for (int i = 0; i < value; i++) {
                    GameObject g = Instantiate(Helper.CoinPath, new Vector3(0, 0, 0), Quaternion.identity, t) as GameObject;
                    g.GetComponent<RectTransform>().localPosition = new Vector3(
                        Random.Range(-10, 10),
                        0,
                        0
                        );
                }
            }

            TurnOff();
        }
    }

    public void Update() {
        if (spawner == false) Debug.LogError("WRONG SPAWN!!!!!!!!!!!!");

        Vector3 r = transform.rotation.eulerAngles;
        r.y += Time.deltaTime * 90;
        transform.rotation = Quaternion.Euler(r);

        if (transform.position.z < (GameManager.player.transform.position.z - 5)) {
            TurnOff();
        }
    }

    private void OnDrawGizmos() {
        if (spawner == false) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
        }
    }
}
