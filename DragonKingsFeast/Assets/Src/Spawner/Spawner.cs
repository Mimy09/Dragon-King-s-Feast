using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_FormationType {
    Line,
    VWing
}

public class Spawner : MonoBehaviour {

    public e_FormationType formation;
    public List<e_EnemyType> wingOfEnemies;
    private List<Enemy> listOfEnemies;

    public List<Enemy> GetListOfEnemies() {
        return listOfEnemies;
    }

}
