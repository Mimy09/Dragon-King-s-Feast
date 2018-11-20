using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// these are the diffrent formations that can be spawned in game
/// 
/// </summary>
public enum e_FormationType {
    Line,
    VWing,
    Box,
    Cross,
    Spiral
}

/// <summary>
/// 
/// this is used to spawn enemies into the game in a set formation
/// 
/// <para>Author: Callum Dunstone </para>
/// 
/// </summary>
public class Spawner : MonoBehaviour {

    /// <summary> the formation we want to spawn the enemies in </summary> 
    [Header("Formation")]
    public e_FormationType formation;

    /// <summary> the enemies that will make up the formation </summary> 
    public List<e_EnemyType> wingOfEnemies;

    /// <summary> these are the gameObejcts of the enemies that have spawned in </summary> 
    private List<Enemy> listOfEnemies = new List<Enemy>();


    /// <summary> 
    /// 
    /// returns the list of enemies that have been spawned in
    /// this is used in flocking and helping the enemies keep formation
    /// 
    /// </summary> 
    public List<Enemy> GetListOfEnemies() {
        return listOfEnemies;
    }

    /// <summary> the distance the enemies will spawn from each other in formation </summary> 
    [Header("Spawn Info")]
    public float spawnDist;

    /// <summary>
    /// IN Editor Show Gizmos
    /// </summary>

    /// <summary>
    /// 
    /// this is used to show the Gizmos in editor based on what
    /// spawn formation has been slected
    /// 
    /// </summary>
    private void OnDrawGizmos() {
        switch (formation) {
            case e_FormationType.Line:
                DrawLineFormationGizmos();
                break;
            case e_FormationType.VWing:
                DrawVFormationGizmo();
                break;
            case e_FormationType.Box:
                DrawBoxFormationGizmos();
                break;
            case e_FormationType.Cross:
                DrawCrossFormationGizmos();
                break;
            case e_FormationType.Spiral:
                DrawSpiralFormationGizmos();
                break;
            default:
                Debug.LogError("NO VALID FORMATION TYPE SELECTED");
                break;
        }
    }

    /// <summary>
    /// 
    /// this is used to show the line formation in editor, it is a simple formation that runs straight along the x axis
    /// 
    /// </summary>
    public void DrawLineFormationGizmos() {
        float half = (wingOfEnemies.Count / 2) * spawnDist;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            Gizmos.DrawSphere(new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z), 1);
        }
    }

    /// <summary>
    /// 
    /// this draws the V formation in editor, this formation has a diamond like tip and then fans out in a V
    /// 
    /// </summary>
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
    /// 
    /// this draws the box formation in the editor, the box formation
    /// faces the player as a flat square filled with enemies
    /// 
    /// </summary>
    public void DrawBoxFormationGizmos() {
        int rows = (int)Mathf.Sqrt(wingOfEnemies.Count);
        int colums = wingOfEnemies.Count / rows;

        int count = 0;

        float verticalHalf = (colums/2) * spawnDist;
        float horizontalHalf = (rows/2) * spawnDist;

        for (int x = 0; x < colums; x++) {
            for (int y = 0; y < rows; y++) {
                Gizmos.DrawSphere(new Vector3(transform.position.x + (horizontalHalf - spawnDist * x), transform.position.y + (verticalHalf - spawnDist * y), transform.position.z), 1);

                count++;
            }
        }

        if (count != wingOfEnemies.Count) {
            Gizmos.DrawSphere(new Vector3(transform.position.x + (horizontalHalf - spawnDist * (colums)), transform.position.y + verticalHalf, transform.position.z), 1);
        }
    }

    /// <summary>
    /// 
    /// this draws out a cross formation in the editor, the cross formation looks like a + symbols 
    /// when it is drawn out.
    /// 
    /// </summary>
    public void DrawCrossFormationGizmos() {
        
        float half = ((wingOfEnemies.Count / 2) * spawnDist) / 2;

        int halfnum = wingOfEnemies.Count / 2;

        int count = 0;

        for (int i = 0; i < halfnum; i++) {
            if (new Vector3(transform.position.x, transform.position.y + (half - spawnDist * count), transform.position.z) == transform.position) {
                count++;
            }
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + (half - spawnDist * count), transform.position.z), 1);
            count++;
        }

        for (int i = 0; i < halfnum; i++) {
            Gizmos.DrawSphere(new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z), 1);
            if (i == halfnum - 1 && (halfnum * 2) != wingOfEnemies.Count) {
                Gizmos.DrawSphere(new Vector3(transform.position.x + (half - spawnDist * (i + 1)), transform.position.y, transform.position.z), 1);
            }
        }
    }

    /// <summary>
    /// 
    /// this draws out the spiral formation in editor, the spiral formation is very hard to properly see
    /// in editor as gizmos don't have a depth buffer, creating a very cluttered look.
    ///  
    /// </summary>
    public void DrawSpiralFormationGizmos() {
        for (int i = 0; i < wingOfEnemies.Count; i++) {
            Gizmos.DrawSphere(new Vector3(Mathf.Sin(spawnDist * i) * spawnDist * i, Mathf.Cos(spawnDist * i) * spawnDist * i, transform.position.z + spawnDist * i), 1);
        }
    }

    /// <summary>
    /// IN Game Spawning
    /// </summary>
    
    /// <summary>
    /// 
    /// we use a collider marked as a trigger to determine weather or not we spawn in the 
    /// formation. the trigger is set off when the player flies through it
    /// 
    /// </summary>
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SpawnFormation();
        }
    }

    /// <summary>
    /// 
    /// this is used to determine what formation we will spawn in
    /// 
    /// </summary>
    public void SpawnFormation() {
        switch (formation) {
            case e_FormationType.Line:
                SpawnLineFormation();
                break;
            case e_FormationType.VWing:
                SpawnVFormation();
                break;
            case e_FormationType.Box:
                SpawnBoxFormation();
                break;
            case e_FormationType.Cross:
                SpawnCrossFormation();
                break;
            case e_FormationType.Spiral:
                SpawnSpiralFormation();
                break;
            default:
                Debug.LogError("NO VALID FORMATION TYPE SELECTED");
                break;
        }
    }

    /// <summary>
    /// 
    /// this spawns the formation into the world, it is a simple formation that runs straight along the x axis in a straight line
    /// 
    /// </summary>
    public void SpawnLineFormation() {

        float half = (wingOfEnemies.Count / 2) * spawnDist;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);
            go.transform.position = new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[i].spawner = this;
            listOfEnemies[i].GetComponent<Flocking>().Init();
        }
    }

    /// <summary>
    /// 
    /// this spawns the formation into the world, this formation has a diamond like tip and then fans out in a V
    /// 
    /// </summary>
    public void SpawnVFormation() {
        
        int count = 0;

        for (int i = 0; i < wingOfEnemies.Count; i++) {
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
            listOfEnemies[i].GetComponent<Flocking>().Init();
        }
    }

    /// <summary>
    /// 
    /// this spawns the formation into the world, the box formation
    /// faces the player as a flat square filled with enemies
    /// 
    /// </summary>
    public void SpawnBoxFormation() {

        int rows = (int)Mathf.Sqrt(wingOfEnemies.Count);
        int colums = wingOfEnemies.Count / rows;

        int count = 0;

        float verticalHalf = (colums / 2) * spawnDist;
        float horizontalHalf = (rows / 2) * spawnDist;

        for (int x = 0; x < colums; x++) {
            for (int y = 0; y < rows; y++) {
                GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[count]);
                go.transform.position = new Vector3(transform.position.x + (horizontalHalf - spawnDist * x), transform.position.y + (verticalHalf - spawnDist * y), transform.position.z);
                listOfEnemies.Add(go.GetComponent<Enemy>());
                listOfEnemies[count].spawner = this;
                listOfEnemies[count].GetComponent<Flocking>().Init();
                count++;
            }
        }

        if (count != wingOfEnemies.Count) {
            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[count]);
            go.transform.position = new Vector3(transform.position.x + (horizontalHalf - spawnDist * (colums)), transform.position.y + verticalHalf, transform.position.z);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[count].spawner = this;
            listOfEnemies[count].GetComponent<Flocking>().Init();
        }
    }

    /// <summary>
    /// 
    /// this spawns the formation into the world, the cross formation looks like a + symbols 
    /// when it is drawn out.
    /// 
    /// </summary>
    public void SpawnCrossFormation() {
        float half = ((wingOfEnemies.Count / 2) * spawnDist) / 2;

        int halfnum = wingOfEnemies.Count / 2;

        int count = 0;

        for (int i = 0; i < halfnum; i++) {
            if (new Vector3(transform.position.x, transform.position.y + (half - spawnDist * count), transform.position.z) == transform.position) {
                count++;
            }

            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);
            go.transform.position = new Vector3(transform.position.x, transform.position.y + (half - spawnDist * count), transform.position.z);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[i].spawner = this;
            listOfEnemies[i].GetComponent<Flocking>().Init();
            count++;
        }

        for (int i = 0; i < halfnum; i++) {

            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);
            go.transform.position = new Vector3(transform.position.x + (half - spawnDist * i), transform.position.y, transform.position.z);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[halfnum + i].spawner = this;
            listOfEnemies[halfnum + i].GetComponent<Flocking>().Init();

            if (i == halfnum - 1 && (halfnum * 2) != wingOfEnemies.Count) {
                GameObject go2 = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i+1]);
                go2.transform.position = new Vector3(transform.position.x + (half - spawnDist * (i+1)), transform.position.y, transform.position.z);
                listOfEnemies.Add(go2.GetComponent<Enemy>());
                listOfEnemies[halfnum + i + 1].spawner = this;
                listOfEnemies[halfnum + i + 1].GetComponent<Flocking>().Init();
            }
        }
    }

    /// <summary>
    /// 
    /// this spawns the formation into the world, the spiral formation is very hard to properly see
    /// in editor as gizmos don't have a depth buffer, creating a very cluttered look.
    ///  
    /// </summary>
    public void SpawnSpiralFormation() {
        for (int i = 0; i < wingOfEnemies.Count; i++) {
            GameObject go = GameManager.objectPoolManager.FindEnemyOfType(wingOfEnemies[i]);
            go.transform.position = new Vector3(Mathf.Sin(spawnDist * i) * spawnDist * i, Mathf.Cos(spawnDist * i) * spawnDist * i, transform.position.z + spawnDist * i);
            listOfEnemies.Add(go.GetComponent<Enemy>());
            listOfEnemies[i].spawner = this;
            listOfEnemies[i].GetComponent<Flocking>().Init();
        }
    }

}
