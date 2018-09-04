using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static AudioManager audioManager;
    public static MapManager mapManager;



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

    public PlayerCamera GetPlayerCamera() {
        return Camera.main.gameObject.GetComponent<PlayerCamera>();
    }
    
    public Player GetPlayer() {
        return player;
    }

    public PlayerIK GetPlayerIK() {
        return playerIK;
    }

    public PlayerMovement GetPlayerMovement() {
        return playerMovement;
    }

    // ---- Set Functions ----




    //**************************************************************************************/
    // ---- Game manager instancing ---- //

    public static GameManager instance;

    private void Awake() {

        // Instance the manager
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(gameObject);
        }

        // If was instanced
        if (instance != null) {
            // Initialize manager
            Initialize();

            // Get player scripts
            Player player_go = FindObjectOfType<Player>();
            if (player_go != null) {
                // Player object
                playerObject    = player_go.gameObject;

                // Player scripts
                player          = player_go;
                playerIK        = player_go.GetComponent<PlayerIK>();
                playerMovement  = player_go.GetComponent<PlayerMovement>();
            }

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

            // Set up event handler
            __event<e_GameEvents>.Raise(this, EventHandle);

            // Set timeout to never
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Init music
            InitAudio();
        }
    }

    private void Update() {
        InputCommand();
    }

    //**************************************************************************************/
    // ---- Audio Init---- //
    private void InitAudio() {
        if (audioManager) {
            audioManager.volume = 0.5f;
            audioManager.AddMusic(Helper.Audio_Music_Level1);
            audioManager.PlayMusic(Helper.Audio_Music_Level1, true);
            audioManager.FadeInMusic(Helper.Audio_Music_Level1, 0.1f);
        }
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

    //**************************************************************************************/
    // ---- Console commands ---- //

    void InputCommand() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            RunCommands();
        }
    }

    void RunCommands() {
        if (UnityEngine.Console.UnityConsole.Instance == null) return;
        string[] msg = UnityEngine.Console.UnityConsole.Instance.ScanConsole().Split(':');

        switch (msg[0]) {
            case "Done":
            case "done":
                return;
            case "Quit":
            case "quit":
                Application.Quit();
                break;
            case "Close":
            case "close":
                UnityEngine.Console.console_api.ConsoleAPI.CloseConsole();
                break;
            case "ResetScene":
            case "resetScene":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Reset":
            case "reset":
                ResetLevel();
                break;
            default:
                print("[COMMAND FAILED] - Unknown command\n");
                break;
        }

        RunCommands();
    }
}
