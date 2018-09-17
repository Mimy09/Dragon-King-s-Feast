using UnityEngine;

public class Helper {

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // OBJECTS

    // full path
    public static Object[] PickupPath           = Resources.LoadAll("Pickups", typeof(Object)) as Object[];
    public static Object[] EnemyPath            = Resources.LoadAll("Enemies", typeof(Object)) as Object[];

    // Enemy paths
    public static Object EnemyPath_AdultDragon  = Resources.Load("Enemies\\AdultDragon") as Object;
    public static Object EnemyPath_Ghost        = Resources.Load("Enemies\\Ghost") as Object;
    public static Object EnemyPath_Witch        = Resources.Load("Enemies\\Witch") as Object;
    public static Object EnemyPath_StormCloud   = Resources.Load("Enemies\\StormCloud") as Object;
    public static Object EnemyPath_Phoenix      = Resources.Load("Enemies\\Phoenix") as Object;

    // boost path
    public static Object ItemPath_Boost_speed   = Resources.Load("Pickups\\speed") as Object;
    public static Object ItemPath_Boost_attack  = Resources.Load("Pickups\\attack") as Object;
    public static Object ItemPath_Boost_defense = Resources.Load("Pickups\\defense") as Object;

    // Pickup path
    public static Object ItemPath_pickUp        = Resources.Load("Pickups\\PickUp") as Object;

    //projectile Path
    public static Object ProjectilePath         = Resources.Load("Projectile\\Projectile") as Object;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // AUDIO

    // Audio - character
    public static AudioClip Audio_Character_BigDragonGrowl    = Resources.Load("Sounds\\Characters\\BigDragonGrowl") as AudioClip;
	public static AudioClip Audio_Character_DragonFireball    = Resources.Load("Sounds\\Characters\\DragonFireball") as AudioClip;
	public static AudioClip Audio_Character_BigDragonFireball    = Resources.Load("Sounds\\Characters\\BigDragonFireball") as AudioClip;
    public static AudioClip Audio_Character_BlueWhale         = Resources.Load("Sounds\\Characters\\bluewhale") as AudioClip;
    public static AudioClip Audio_Character_Brazire           = Resources.Load("Sounds\\Characters\\Brazire") as AudioClip;
    public static AudioClip Audio_Character_DragonRoar        = Resources.Load("Sounds\\Characters\\DragonRoar") as AudioClip;
    public static AudioClip Audio_Character_Explosion         = Resources.Load("Sounds\\Characters\\explosion") as AudioClip;
    public static AudioClip Audio_Character_ExplosionLarge    = Resources.Load("Sounds\\Characters\\explosionLarge") as AudioClip;
	public static AudioClip Audio_Character_FireImpact 	    = Resources.Load("Sounds\\Characters\\FireImpact") as AudioClip;
    public static AudioClip Audio_Character_FemaleScream      = Resources.Load("Sounds\\Characters\\FemaleScream") as AudioClip;
    public static AudioClip Audio_Character_Lightning         = Resources.Load("Sounds\\Characters\\Thunder") as AudioClip;
    public static AudioClip Audio_Character_MonsterAttack     = Resources.Load("Sounds\\Characters\\MonsterAttack") as AudioClip;
    public static AudioClip Audio_Character_PainDeath         = Resources.Load("Sounds\\Characters\\PainDeath") as AudioClip;
    public static AudioClip Audio_Character_RoarGrowl         = Resources.Load("Sounds\\Characters\\RoarGrowl") as AudioClip;
    public static AudioClip Audio_Character_Spell             = Resources.Load("Sounds\\Characters\\Spell") as AudioClip;
    public static AudioClip Audio_Character_WetImpact         = Resources.Load("Sounds\\Characters\\WetImpact") as AudioClip;
	public static AudioClip Audio_BDBreathe					   = Resources.Load("Sounds\\Characters\\BDBreathe") as AudioClip;
	public static AudioClip Audio_Character_BDDeepHurt   	= Resources.Load("Sounds\\Characters\\BDDeepHurt") as AudioClip;
	public static AudioClip Audio_Character_BDShortGrowl    = Resources.Load("Sounds\\Characters\\BDShortGrowl") as AudioClip;
	public static AudioClip Audio_Character_BDTired		    = Resources.Load("Sounds\\Characters\\BDTired") as AudioClip;
	public static AudioClip Audio_Character_DragonGrr  		  = Resources.Load("Sounds\\Characters\\DragonGrr") as AudioClip;
	public static AudioClip Audio_Character_SwingEffect    = Resources.Load("Sounds\\Characters\\SwingEffect") as AudioClip;
	public static AudioClip Audio_Character_SwingHit	    = Resources.Load("Sounds\\Characters\\SwingHit") as AudioClip;
	public static AudioClip Audio_Character_TimeWarp    = Resources.Load("Sounds\\Characters\\TimeWarp") as AudioClip;

    // Audio - Environment
    public static AudioClip Audio_Environment_Beach           = Resources.Load("Sounds\\Environment\\beach") as AudioClip;
    public static AudioClip Audio_Environment_Grass           = Resources.Load("Sounds\\Environment\\Grass") as AudioClip;
    public static AudioClip Audio_Environment_Lake            = Resources.Load("Sounds\\Environment\\Lake") as AudioClip;
    public static AudioClip Audio_Environment_Ocean           = Resources.Load("Sounds\\Environment\\Ocean") as AudioClip;

    // Audio - Music
    public static AudioClip Audio_Music_Level1                = Resources.Load("Sounds\\Music\\DKF01") as AudioClip;
    public static AudioClip Audio_Music_Level2                = Resources.Load("Sounds\\Music\\DKF02") as AudioClip;
    public static AudioClip Audio_Music_Level3                = Resources.Load("Sounds\\Music\\DKF03") as AudioClip;
    public static AudioClip Audio_Music_Level                 = Resources.Load("Sounds\\Music\\LevelMusic") as AudioClip;

    public static AudioClip Audio_Music_BossFight             = Resources.Load("Sounds\\Music\\DKF04") as AudioClip;
    public static AudioClip Audio_Music_Menu                  = Resources.Load("Sounds\\Music\\Menu") as AudioClip;

    // Audio - Pickups
    public static AudioClip Audio_Pickup_1                    = Resources.Load("Sounds\\Music\\Metalics04") as AudioClip;
    public static AudioClip Audio_Pickup_2                    = Resources.Load("Sounds\\Music\\Metalics05") as AudioClip;
    public static AudioClip Audio_Pickup_3                    = Resources.Load("Sounds\\Music\\Metalics07") as AudioClip;
    public static AudioClip Audio_Pickup_4                    = Resources.Load("Sounds\\Music\\Metalics08") as AudioClip;
	public static AudioClip CoinCollection                    = Resources.Load("Sounds\\Pickups\\CoinCollection") as AudioClip;

}
