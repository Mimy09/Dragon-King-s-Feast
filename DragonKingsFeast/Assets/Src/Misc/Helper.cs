using UnityEngine;

public class Helper {

    // full path
    public static Object[] PickupPath = Resources.LoadAll("Pickups", typeof(Object)) as Object[];
    public static Object[] EnemyPath  = Resources.LoadAll("Enemies", typeof(Object)) as Object[];

    // Enemy paths
    public static Object EnemyPath_AdultDragon  = Resources.Load("Enemies\\AdultDragon") as Object;
    public static Object EnemyPath_Ghost        = Resources.Load("Enemies\\Ghost") as Object;
    public static Object EnemyPath_Witch        = Resources.Load("Enemies\\Witch") as Object;
    public static Object EnemyPath_StormCloud   = Resources.Load("Enemies\\StormCloud") as Object;
    public static Object EnemyPath_Phoenix      = Resources.Load("Enemies\\Phoenix") as Object;

    // boost path
    public static Object ItemPath_Boost_speed = Resources.Load("Pickups\\speed") as Object;
    public static Object ItemPath_Boost_attack = Resources.Load("Pickups\\attack") as Object;
    public static Object ItemPath_Boost_defense = Resources.Load("Pickups\\defense") as Object;

    // Pickup path
    public static Object ItemPath_pickUp = Resources.Load("Pickups\\PickUp") as Object;

}
