using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadObjects : MonoBehaviour {
    public float LoadDistance;
    public float LoadTimer;
    [ReadOnly] public GameObject[] m_WorldObjectsLevel1;
    [ReadOnly] public GameObject[] m_WorldObjectsLevel2;
    [ReadOnly] public GameObject[] m_WorldObjectsLevel3;

    private float timer = 0.0f;
    private float dist = 0.0f;
    private bool running = false;

    public void SetUnloadObjects() {
        if (running == true) return;
        m_WorldObjectsLevel1 = SceneManager.GetSceneByName("_Level1").GetRootGameObjects();
        m_WorldObjectsLevel2 = SceneManager.GetSceneByName("_Level2").GetRootGameObjects();
        m_WorldObjectsLevel3 = SceneManager.GetSceneByName("_Level3").GetRootGameObjects();
        running = true;
    }

    public void UnloadLevel(int index) {
        if (index == 1) {
            m_WorldObjectsLevel1 = null;
        } else if (index == 2) {
            m_WorldObjectsLevel2 = null;
        } else if (index == 3) {
            m_WorldObjectsLevel3 = null;
        }
    }

    private void Update() {
        if (!running) return;
        timer += Time.deltaTime;

        if (timer >= LoadTimer) {
            dist = GameManager.instance.GetPlayer().gameObject.transform.position.z;
            
            if (m_WorldObjectsLevel3 == null) return;
            for (int i = 0; i < m_WorldObjectsLevel3.Length; i++) {
                if (m_WorldObjectsLevel3[i].transform.position.z - dist > LoadDistance) {
                    m_WorldObjectsLevel3[i].SetActive(false);
                }
                else {
                    m_WorldObjectsLevel3[i].SetActive(true);
                }
            }

            if (m_WorldObjectsLevel2 == null) return;
            for (int i = 0; i < m_WorldObjectsLevel2.Length; i++) {
                if (m_WorldObjectsLevel2[i].transform.position.z - dist > LoadDistance) {
                    m_WorldObjectsLevel2[i].SetActive(false);
                }
                else {
                    m_WorldObjectsLevel2[i].SetActive(true);
                }
            }

            if (m_WorldObjectsLevel1 == null) return;
            for (int i = 0; i < m_WorldObjectsLevel1.Length; i++) {
                if (m_WorldObjectsLevel1[i].transform.position.z - dist > LoadDistance) {
                    m_WorldObjectsLevel1[i].SetActive(false);
                }
                else {
                    m_WorldObjectsLevel1[i].SetActive(true);
                }
            }
        }
    }
}
