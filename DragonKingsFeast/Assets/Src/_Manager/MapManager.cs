//**************************************************************************************/
// ---- Includes ---- //
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Variables ---- //

    // private
    bool isLoaded_level1, isLoaded_level2, isLoaded_level3;
    AsyncOperation AO_level1, AO_level2, AO_level3;

    public float loaded_percent = 0;
    public bool loaded = false;

    // public
    [Header("Level 1")]
    public bool level1ShowGismoz = true;
    public int level1LoadDistance = 0;
    public int level1UnloadDistance = 100;

    [Header("Level 2")]
    public bool level2ShowGismoz = true;
    public int level2LoadDistance = 25;
    public int level2UnloadDistance = 200;

    [Header("Level 3")]
    public bool level3ShowGismoz = true;
    public int level3LoadDistance = 125;
    public int level3UnloadDistance = 300;

    //**************************************************************************************/
    // ---- Functions ---- //

    private void Start() {
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        StartCoroutine(LoadAllInBackground());
    }

    IEnumerator LoadAllInBackground() {
        
        AO_level1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        AO_level1.allowSceneActivation = false;
        while (AO_level1.progress < 0.9f) {
            yield return null;
        }
        AO_level1.allowSceneActivation = true;
        loaded_percent = AO_level1.progress * 38;

        AO_level2 = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        AO_level2.allowSceneActivation = false;
        while (AO_level2.progress < 0.9f) {
            yield return null;
        }
        AO_level2.allowSceneActivation = true;
        loaded_percent += AO_level2.progress * 38;

        AO_level3 = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        isLoaded_level3 = true;
        AO_level3.allowSceneActivation = false;
        while (AO_level3.progress < 0.9f) {
            yield return null;
        }
        AO_level3.allowSceneActivation = true;
        loaded_percent += AO_level3.progress * 38;

        loaded_percent = Mathf.Min(loaded_percent, 100);

        loaded = true;
    }

    private void Update() {
        if (GameManager.playerObject != null) {

            // Get Player position
            Vector3 pos = GameManager.playerObject.transform.position;

            // levels 1
            if (pos.z >= level1LoadDistance) {

                // Load level
                if (pos.z <= level1UnloadDistance) {
                    LoadLevel(1);
                }
                
                // Unload levels
                else {
                    UnloadLevel(1);
                }
            } else {
                UnloadLevel(1);
            }

            // levels 2
            if (pos.z >= level2LoadDistance) {

                // Load level
                if (pos.z <= level2UnloadDistance) {
                    LoadLevel(2);
                }

                // Unload levels
                else {
                    UnloadLevel(2);
                }
            } else {
                UnloadLevel(2);
            }

            // levels 3
            if (pos.z >= level3LoadDistance) {

                // Load level
                if (pos.z <= level3UnloadDistance) {
                    LoadLevel(3);
                }

                // Unload levels
                else {
                    UnloadLevel(3);
                }
            } else {
                UnloadLevel(3);
            }
        }
    }

    // returns if level is loaded
    bool IsLevelLoaded(int level) {
        switch (level) {
            case 1: return isLoaded_level1;
            case 2: return isLoaded_level2;
            case 3: return isLoaded_level3;
            default: return false;
        }
    }

    void LoadLevel(int level) {
        return;

        switch (level) {
            case 1:
                // If level 1 is not loaded then load it in
                if (!isLoaded_level1) {
                    isLoaded_level1 = true;
                    AO_level1.allowSceneActivation = true;
                }
                break;
            case 2:
                // If level 2 is not loaded then load it in
                if (!isLoaded_level2) {
                    isLoaded_level2 = true;
                    AO_level2.allowSceneActivation = true;
                }
                break;
            case 3:
                // If level 3 is not loaded then load it in
                if (!isLoaded_level3) {
                    isLoaded_level3 = true;
                    AO_level3.allowSceneActivation = true;
                }
                break;
        }
    }
    void UnloadLevel(int level) {
        switch (level) {
            case 1:
                // If level 1 is loaded then unload
                if (isLoaded_level1) {
                    SceneManager.UnloadSceneAsync(1);
                    isLoaded_level1 = false;
                }
                break;
            case 2:
                // If level 2 is loaded then unload
                if (isLoaded_level2) {
                    SceneManager.UnloadSceneAsync(2);
                    isLoaded_level2 = false;
                }
                break;
            case 3:
                // If level 3 is loaded then unload
                if (isLoaded_level3) {
                    SceneManager.UnloadSceneAsync(3);
                    isLoaded_level3 = false;
                }
                break;
        }
    }

    //**************************************************************************************/
    // ---- Gizmos ---- //

    private void OnDrawGizmos() {
        Vector3 scale = new Vector3(50, 50, 1);

        if (level1ShowGismoz) {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(0, 0, level1LoadDistance), scale);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(0, 0, level1UnloadDistance), scale);
        }
        if (level2ShowGismoz) {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(0, 0, level2LoadDistance), scale);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(0, 0, level2UnloadDistance), scale);
        }
        if (level3ShowGismoz) {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(0, 0, level3LoadDistance), scale);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(0, 0, level3UnloadDistance), scale);
        }
    }
}
