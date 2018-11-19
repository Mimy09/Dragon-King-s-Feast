using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Disables any object that is far away from the player
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class UnloadObjects : MonoBehaviour {
    /// <summary>
    /// Distance the objects get re-enabled
    /// </summary>
    public float LoadDistance;

    /// <summary>
    /// All object in the first level
    /// </summary>
    [ReadOnly] public GameObject[] m_WorldObjectsLevel1;
    /// <summary>
    /// All object in the second level
    /// </summary>
    [ReadOnly] public GameObject[] m_WorldObjectsLevel2;
    /// <summary>
    /// All object in the third level
    /// </summary>
    [ReadOnly] public GameObject[] m_WorldObjectsLevel3;

    /// <summary>
    /// Distance from the starting position
    /// </summary>
    private float dist = 0.0f;

    /// <summary>
    /// Is the game running
    /// </summary>
    private bool running = false;

    /// <summary>
    /// Sets up the object lists
    /// </summary>
    public void SetUnloadObjects() {
        if (running == true) return;
        m_WorldObjectsLevel1 = SceneManager.GetSceneByName("_Level1").GetRootGameObjects();
        m_WorldObjectsLevel2 = SceneManager.GetSceneByName("_Level2").GetRootGameObjects();
        m_WorldObjectsLevel3 = SceneManager.GetSceneByName("_Level3").GetRootGameObjects();
        running = true;
    }

    /// <summary>
    /// sets the lists to null when level is unloaded
    /// </summary>
    /// <param name="index"></param>
    public void UnloadLevel(int index) {
        if (index == 1) {
            m_WorldObjectsLevel1 = null;
        } else if (index == 2) {
            m_WorldObjectsLevel2 = null;
        } else if (index == 3) {
            m_WorldObjectsLevel3 = null;
        }
    }

    /// <summary>
    /// Enables the objects when they get close to the player
    /// </summary>
    private void Update() {
        if (!running) return;
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
