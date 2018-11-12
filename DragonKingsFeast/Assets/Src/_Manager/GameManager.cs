using UnityEngine;
using System.Collections.Generic;

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
    // ---- Manager functions ---- //

    public void ResetLevel() {

    }

    // ---- Get Functions ----
    public AudioManager GetAudioManager() {
        if (audioManager == null)
            audioManager = GetComponent<AudioManager>();
        return audioManager;
    }

    public ObjectPoolManager GetObjectPool() {
        if (objectPoolManager == null)
            objectPoolManager = GetComponent<ObjectPoolManager>();
        return objectPoolManager;
    }

    public MapManager GetMapManager() {
        if (mapManager == null)
            mapManager = GetComponent<MapManager>();
        return mapManager;
    }

    public ItemSpawnManager GetItemSpawnManager() {
        if (itemSpawnManager == null)
            itemSpawnManager = GetComponent<ItemSpawnManager>();
        return itemSpawnManager;
    }

    public UnloadObjects GetUnloadObjects() {
        if (unloadObjects == null)
            unloadObjects = GetComponent<UnloadObjects>();
        return unloadObjects;
    }

    public TutorialManager GetTutorialManager() {
        if (tutorialManager == null)
            tutorialManager = GetComponent<TutorialManager>();
        return tutorialManager;
    }

    public PlayerCamera GetPlayerCamera() {
        return Camera.main.gameObject.GetComponent<PlayerCamera>();
    }
    
    public Player GetPlayer() {
        if (player == null)
            SetupPlayer();
        return player;
    }

    public PlayerIK GetPlayerIK() {
        if (playerIK == null)
            SetupPlayer();
        return playerIK;
    }

    public PlayerMovement GetPlayerMovement() {
        if (playerMovement == null)
            SetupPlayer();
        return playerMovement;
    }

    // ---- Set Functions ----




    //**************************************************************************************/
    // ---- Game manager instancing ---- //

    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

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
            
            // Set up event handler
            __event<e_GameEvents>.Raise(this, EventHandle);

            // Make menu show up
            __event<e_UI>.InvokeEvent(this, e_UI.MENU, false);
            __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL);

            // Set timeout to never
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            coinSP = GameObject.FindGameObjectWithTag("CoinSP");

            // Init music
            InitAudio();
        }
    }

    private void Update() {
        EnemyList = enemyList;
        ItemList = itemList;
    }

    //**************************************************************************************/
    // ---- Audio Init---- //
    private void InitAudio() {
        //if (audioManager) {
        //    audioManager.volume = 0.5f;
        //    audioManager.AddMusic(Helper.Audio_Music_Level1);
        //    audioManager.PlayMusic(Helper.Audio_Music_Level1, true);
        //    audioManager.FadeInMusic(Helper.Audio_Music_Level1, 0.1f);
        //}
    }


    //**************************************************************************************/
    // ---- Game event handler ---- //

    private void EventHandle(object s, __eArg<e_GameEvents> e) {
        switch (e.arg) {
            case e_GameEvents.RESET:
                ResetLevel();
                break;
            default: break;
        }
    }

    //**************************************************************************************/
    // ---- System events ---- //

    private void Initialize() {
        // Send INIT event
        __event<e_SystemEvents>.InvokeEvent(this, e_SystemEvents._INIT_);
    }

    private void OnApplicationQuit() {
        // Send CLOSE event
        __event<e_SystemEvents>.InvokeEvent(this, e_SystemEvents._CLOSE_);
    }
}
