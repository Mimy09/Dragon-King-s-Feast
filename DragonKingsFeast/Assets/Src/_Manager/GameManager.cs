using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// This controls all the interactions between the other managers
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// </summary>
[RequireComponent(typeof(ObjectPoolManager))]
[RequireComponent(typeof(MapManager))]
public class GameManager : MonoBehaviour {

    // Player scripts
    public static Player player;
    public static PlayerIK playerIK;
    public static PlayerMovement playerMovement;

    // Player Object
    public static GameObject playerObject;

    // Managers
    public static UnloadObjects unloadObjects;
    public static ObjectPoolManager objectPoolManager;
    public static ItemSpawnManager itemSpawnManager;
    public static TutorialManager tutorialManager;
    public static AudioManager audioManager;
    public static MapManager mapManager;

    // Init variables
    public static bool firstTimeLoading = false;

    // Store the coin drop position
    public static GameObject coinSP;
    
    // List of active objects
    public static List<GameObject> enemyList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> EnemyList = new List<GameObject>();
    public static List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> ItemList = new List<GameObject>();

    //**************************************************************************************/
    // ---- Get Functions ---- //

    /// <summary>
    /// Returns the AudioManager
    /// </summary>
    public AudioManager GetAudioManager() {
        if (audioManager == null)
            audioManager = GetComponent<AudioManager>();
        return audioManager;
    }

    /// <summary>
    /// Returns the ObjectPool
    /// </summary>
    public ObjectPoolManager GetObjectPool() {
        if (objectPoolManager == null)
            objectPoolManager = GetComponent<ObjectPoolManager>();
        return objectPoolManager;
    }

    /// <summary>
    /// Returns the MapManager
    /// </summary>
    public MapManager GetMapManager() {
        if (mapManager == null)
            mapManager = GetComponent<MapManager>();
        return mapManager;
    }

    /// <summary>
    /// Returns the SpawnManager
    /// </summary>
    public ItemSpawnManager GetItemSpawnManager() {
        if (itemSpawnManager == null)
            itemSpawnManager = GetComponent<ItemSpawnManager>();
        return itemSpawnManager;
    }

    /// <summary>
    /// Returns the UnloadManager
    /// </summary>
    public UnloadObjects GetUnloadObjects() {
        if (unloadObjects == null)
            unloadObjects = GetComponent<UnloadObjects>();
        return unloadObjects;
    }

    /// <summary>
    /// Returns the TutorialManager
    /// </summary>
    public TutorialManager GetTutorialManager() {
        if (tutorialManager == null)
            tutorialManager = GetComponent<TutorialManager>();
        return tutorialManager;
    }

    /// <summary>
    /// Returns the PlayerCamera
    /// </summary>
    public PlayerCamera GetPlayerCamera() {
        return Camera.main.gameObject.GetComponent<PlayerCamera>();
    }

    /// <summary>
    /// Returns the Player
    /// </summary>
    public Player GetPlayer() {
        if (player == null)
            SetupPlayer();
        return player;
    }

    /// <summary>
    /// Returns the PlayerIK
    /// </summary>
    public PlayerIK GetPlayerIK() {
        if (playerIK == null)
            SetupPlayer();
        return playerIK;
    }

    /// <summary>
    /// Returns the PlayerMovement
    /// </summary>
    public PlayerMovement GetPlayerMovement() {
        if (playerMovement == null)
            SetupPlayer();
        return playerMovement;
    }

    //**************************************************************************************/
    // ---- Game manager instancing ---- //

    /// <summary>
    /// instance to accesses the game manager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// set the instance
    /// </summary>
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets up the player components to be accessed
    /// </summary>
    void SetupPlayer() {
        Player player_go = FindObjectOfType<Player>();
        if (player_go != null) {
            // Player object
            playerObject = player_go.gameObject;

            // Player scripts
            player = player_go;
            playerIK = player_go.GetComponent<PlayerIK>();
            playerMovement = player_go.GetComponent<PlayerMovement>();
        }
    }

    /// <summary>
    /// Sets up manager components to be accessed
    /// </summary>
    private void Start() {
        if (PlayerPrefs.GetInt("hasLoaded") == 0) {
            PlayerPrefs.SetInt("hasLoaded", 1);
            firstTimeLoading = false;
        } else {
            firstTimeLoading = false;
        }

        Time.timeScale = 0;

        // If was instanced
        if (instance != null) {
            // Initialize manager
            Initialize();

            SetupPlayer();

            // get object pool manager
            if (objectPoolManager == null)
                objectPoolManager = GetComponent<ObjectPoolManager>();

            // get map manager
            if (mapManager == null)
                mapManager = GetComponent<MapManager>();

            // get audio manager
            if (audioManager == null)
                audioManager = GetComponent<AudioManager>();

            // get Unload manager
            if (unloadObjects == null)
                unloadObjects = GetComponent<UnloadObjects>();

            if (tutorialManager == null)
                tutorialManager = GetComponent<TutorialManager>();
            
            // Make menu show up
            __event<e_UI>.InvokeEvent(this, e_UI.MENU, false);
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);

            // Set timeout to never
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            coinSP = GameObject.FindGameObjectWithTag("CoinSP");
        }
    }

    /// <summary>
    /// Updates the public lists for access in the inspector
    /// </summary>
    private void Update() {
        EnemyList = enemyList;
        ItemList = itemList;
    }

    //**************************************************************************************/
    // ---- System events ---- //

    /// <summary>
    /// Sends the INIT event
    /// </summary>
    private void Initialize() {
        // Send INIT event
        __event<e_SystemEvents>.InvokeEvent(this, e_SystemEvents._INIT_);
    }

    /// <summary>
    /// Sends the close event
    /// </summary>
    private void OnApplicationQuit() {
        // Send CLOSE event
        __event<e_SystemEvents>.InvokeEvent(this, e_SystemEvents._CLOSE_);
    }
}
