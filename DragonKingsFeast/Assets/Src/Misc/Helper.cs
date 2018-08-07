using UnityEngine;

public class Helper {

    // full path
    public static GameObject[] PickupPath = Resources.LoadAll("Pickups\\") as GameObject[];
    public static GameObject[] EnemyPath  = Resources.LoadAll("Enemies\\") as GameObject[];

    // Enemy paths
    public static GameObject EnemyPath_AdultDragon  = Resources.Load("Enemies\\AdultDragon") as GameObject;
    public static GameObject EnemyPath_Ghost        = Resources.Load("Enemies\\Ghost") as GameObject;
    public static GameObject EnemyPath_Witch        = Resources.Load("Enemies\\Witch") as GameObject;
    public static GameObject EnemyPath_StormCloud   = Resources.Load("Enemies\\StormCloud") as GameObject;
    public static GameObject EnemyPath_Phoenix      = Resources.Load("Enemies\\Phoenix") as GameObject;


}
