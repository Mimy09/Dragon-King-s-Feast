//**************************************************************************************/
// ---- Includes ---- //
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Variables ---- //

    bool isLoaded_level1, isLoaded_level2, isLoaded_level3;

    //**************************************************************************************/
    // ---- Functions ---- //

    private void Update() {
        if (GameManager.playerObject != null) {

            // Get Player position
            Vector3 pos = GameManager.playerObject.transform.position;

            // levels 1
            if (pos.z >= 0) {
                
                // Load level
                if (pos.z <= 100) {
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
            if (pos.z >= 25) {

                // Load level
                if (pos.z <= 200) {
                    LoadLevel(2);
                }

                // Unload levels
                else {
                    UnloadLevel(2);
                }
            } else {
                UnloadLevel(2);
            }

            if (pos.z >= 125) {

                // Load level
                if (pos.z <= 200) {
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

    void LoadLevel(int level) {
        switch (level) {
            case 1:
                // If level 1 is not loaded then load it in
                if (!isLoaded_level1) {
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    isLoaded_level1 = true;
                }
                break;
            case 2:
                // If level 2 is not loaded then load it in
                if (!isLoaded_level2) {
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    isLoaded_level2 = true;
                }
                break;
            case 3:
                // If level 3 is not loaded then load it in
                if (!isLoaded_level3) {
                    SceneManager.LoadScene(3, LoadSceneMode.Additive);
                    isLoaded_level3 = true;
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

}
