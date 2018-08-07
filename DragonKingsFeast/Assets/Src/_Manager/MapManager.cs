using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    bool isLoaded_level1, isLoaded_level2, isLoaded_level3;

    private void Update() {
        if (GameManager.playerObject != null) {

            // load levels
            if (GameManager.playerObject.transform.position.z > 0) {
                LoadLevel(1);
            }

            if (GameManager.playerObject.transform.position.z > 25) {
                LoadLevel(2);
            } else {
                UnloadLevel(2);
            }

            if (GameManager.playerObject.transform.position.z > 125) {
                LoadLevel(3);
            } else {
                UnloadLevel(3);
            }
        }

    }

    void LoadLevel(int level) {
        switch (level) {
            case 1:
                // If level 1 is not loaded then load it in
                if (isLoaded_level1) {
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    isLoaded_level1 = true;
                }
                break;
            case 2:
                // If level 2 is not loaded then load it in
                if (isLoaded_level2) {
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    isLoaded_level2 = true;
                }
                break;
            case 3:
                // If level 3 is not loaded then load it in
                if (isLoaded_level3) {
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
