using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_FormationType {
    Line,
    VWing
}

public class Spawner : MonoBehaviour {
    [Header("Formation")]
    public e_FormationType formation;
    public List<e_EnemyType> wingOfEnemies;
    private List<Enemy> listOfEnemies = new List<Enemy>();

    public List<Enemy> GetListOfEnemies() {
        return listOfEnemies;
    }

    [Header("Spawn Info")]
    public float spawnDist;

    public bool spawn = false;

    /// <summary>
    /// IN Editor Show Gizmos
    /// </summary>

    private void OnDrawGizmosSelected() {
        switch (formation) {
            case e_FormationType.Line:
                DrawLineFormationGizmos();
                break;
            case e_FormationType.VWing:
                DrawVFormationGizmo();
                break;
            default:
                Debug.LogError("NO VALID FORMATION TYPE SELECTED");
                break;
        }
    }

    public void DrawLineFormationGizmos() {
        Debug.Log("Test");
        float half = (wingOfEnemies.Count / 2) * spawnDist;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            Gizmos.DrawSphere(new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z), 1);
        }
    }

    public void DrawVFormationGizmo() {
        int count = 0;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            if (i%2 == 0 && i != wingOfEnemies.Count - 1) {
                Gizmos.DrawSphere(new Vector3(transform.position.x + (spawnDist * count), transform.position.y, transform.position.z + (spawnDist * count)), 1);
            }
            else if(i != wingOfEnemies.Count - 1) {
                count++;
                Gizmos.DrawSphere(new Vector3(transform.position.x - (spawnDist * count), transform.position.y, transform.position.z + (spawnDist * count)), 1);
            }            
            else if (i == wingOfEnemies.Count - 1) {
                Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + (spawnDist * 2)), 1);
            }

        }
    }

    /// <summary>
    /// IN Game Spawning
    /// </summary>

    public void Update() {
        if (spawn == false) {
            spawn = true;

            SpawnVFormation();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SpawnFormation();
        }
    }

    public void SpawnFormation() {
        switch (formation) {
            case e_FormationType.Line:
                SpawnLineFormation();
                break;
            case e_FormationType.VWing:
                SpawnVFormation();
                break;
            default:
                Debug.LogError("NO VALID FORMATION TYPE SELECTED");
                break;
        }
    }

    public void SpawnLineFormation() {

        float half = (wingOfEnemies.Count / 2) * spawnDist;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            Debug.Log("Test");
            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);
            go.transform.position = new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[i].spawner = this;
        }
    }

    public void SpawnVFormation() {
        
        int count = 0;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            Debug.Log("Test V Formation");

            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);

            if (i % 2 == 0) {
                go.transform.position = new Vector3(transform.position.x + (spawnDist * count), transform.position.y, transform.position.z + (spawnDist * count));
            }
            else {
                count++;
                go.transform.position = new Vector3(transform.position.x - (spawnDist * count), transform.position.y, transform.position.z + (spawnDist * count));
            }

            if (i == wingOfEnemies.Count - 1) {
                go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (spawnDist * 2));
            }

            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[i].spawner = this;
        }
    }
}
