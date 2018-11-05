//**************************************************************************************/
// ---- Includes ---- //
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

    //**************************************************************************************/
    // ---- Variables ---- //

    // private
    AsyncOperation AO_level1, AO_level2, AO_level3, AO_level4;

    [Header("Manager Options")]
    [ReadOnly] public float loaded_percent = 0;
    [ReadOnly] public bool isLoaded_level1, isLoaded_level2, isLoaded_level3;
    [ReadOnly] public bool loaded = false;
    [ReadOnly] public bool hasWon = false;
    private bool isInGame = false;

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
        GameManager.instance.GetAudioManager().volume = 0.5f;
        GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level);
        GameManager.instance.GetAudioManager().PlayMusic(Helper.Audio_Music_Level, true);
        GameManager.instance.GetAudioManager().FadeInMusic(Helper.Audio_Music_Level, 3);
    }

    public void StartLoad() {
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        StartCoroutine(LoadAllInBackground());
        if (GameManager.firstTimeLoading) {
            GameManager.instance.GetTutorialManager().Init();
        }

        AO_level4 = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        AO_level4.allowSceneActivation = true;
    }

    IEnumerator LoadAllInBackground() {
        
        AO_level1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        AO_level1.allowSceneActivation = false;
        while (AO_level1.progress < 0.9f) {
            yield return null;
        }
        AO_level1.allowSceneActivation = true;
        loaded_percent = AO_level1.progress;

        AO_level2 = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        AO_level2.allowSceneActivation = false;
        while (AO_level2.progress < 0.9f) {
            yield return null;
        }
        AO_level2.allowSceneActivation = true;
        loaded_percent += AO_level2.progress;

        AO_level3 = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        AO_level3.allowSceneActivation = false;
        while (AO_level3.progress < 0.9f) {
            yield return null;
        }
        AO_level3.allowSceneActivation = true;
        loaded_percent += AO_level3.progress;

        loaded_percent = Mathf.CeilToInt(loaded_percent / 2.7f * 100);
        loaded = true;
    }

    private void Update() {
        if (!loaded) return;

        if (!isInGame) {
            if (AO_level1.isDone == true && AO_level2.isDone == true && AO_level3.isDone == true) {
                GameManager.instance.GetUnloadObjects().SetUnloadObjects();
                __event<e_UI>.InvokeEvent(this, e_UI.GAME);
                isInGame = true;
            }
        }

        if (GameManager.playerObject != null) {

            // Get Player position
            Vector3 pos = GameManager.playerObject.transform.position;

            // levels 1
            if (pos.z >= level1LoadDistance) {

                // Load level
                if (pos.z <= level1UnloadDistance) {
                    LoadLevel(1);
                } else { // Unload levels
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
                } else { // Unload levels
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
                } else { // Unload levels

                    // if unloaded by reaching end
                    // then player has won the game
                    hasWon = true;
                    UnloadLevel(3);
                }
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
        switch (level) {
            case 1:
                // If level 1 is not loaded then load it in
                if (!isLoaded_level1) {
                    isLoaded_level1 = true;
                    //GameManager.instance.GetAudioManager().volume = 0.5f;
                    //GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level1);
                    //GameManager.instance.GetAudioManager().PlayMusic(Helper.Audio_Music_Level1, true);
                    //GameManager.instance.GetAudioManager().FadeInMusic(Helper.Audio_Music_Level1, 3);
                }
                break;
            case 2:
                // If level 2 is not loaded then load it in
                if (!isLoaded_level2) {
                    isLoaded_level2 = true;
                    //GameManager.instance.GetAudioManager().volume = 0.5f;
                    //GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level2);
                    //GameManager.instance.GetAudioManager().PlayMusic(Helper.Audio_Music_Level2, true);
                    //GameManager.instance.GetAudioManager().FadeInMusic(Helper.Audio_Music_Level2, 3);
                }
                break;
            case 3:
                // If level 3 is not loaded then load it in
                if (!isLoaded_level3) {
                    isLoaded_level3 = true;
                    //GameManager.instance.GetAudioManager().volume = 0.5f;
                    //GameManager.instance.GetAudioManager().AddMusic(Helper.Audio_Music_Level3);
                    //GameManager.instance.GetAudioManager().PlayMusic(Helper.Audio_Music_Level3, true);
                    //GameManager.instance.GetAudioManager().FadeInMusic(Helper.Audio_Music_Level3, 3);
                }
                break;
        }
    }

    public void UnloadAll() {
        isLoaded_level1 = false;
        isLoaded_level2 = false;
        isLoaded_level3 = false;
        loaded = false;
        AO_level1 = null;
        AO_level2 = null;
        AO_level3 = null;
        isInGame = false;
        UnloadEntities();
    }

    public void UnloadEntities() {
        int enemyCount = GameManager.enemyList.Count;
        int ItemCount = GameManager.itemList.Count;

        for (int i = 0; i < enemyCount; i++) {
            GameManager.enemyList[0].GetComponent<Enemy>().TurnOff();
        }

        for (int i = 0; i < ItemCount; i++) {
            GameManager.itemList[0].GetComponent<Item>().TurnOff();
        }
    }

    public void UnloadLevel(int level) {
        switch (level) {
            case 1:
                // If level 1 is loaded then unload
                if (isLoaded_level1) {
                    GameManager.instance.GetUnloadObjects().UnloadLevel(1);
                    if (AO_level1 != null) SceneManager.UnloadSceneAsync(1);
                    AO_level1 = null;
                    isLoaded_level1 = false;
                    //GameManager.instance.GetAudioManager().FadeOutMusic(Helper.Audio_Music_Level1, 3);
                }
                break;
            case 2:
                // If level 2 is loaded then unload
                if (isLoaded_level2) {
                    GameManager.instance.GetUnloadObjects().UnloadLevel(2);
                    if (AO_level2 != null) SceneManager.UnloadSceneAsync(2);
                    AO_level2 = null;
                    isLoaded_level2 = false;
                    //GameManager.instance.GetAudioManager().FadeOutMusic(Helper.Audio_Music_Level2, 3);
                }
                break;
            case 3:
                // If level 3 is loaded then unload
                if (isLoaded_level3) {
                    GameManager.instance.GetUnloadObjects().UnloadLevel(3);
                    if (AO_level4 != null) SceneManager.UnloadSceneAsync(4);
                    AO_level4 = null;
                    if (AO_level3 != null) SceneManager.UnloadSceneAsync(3);
                    AO_level3 = null;
                    isLoaded_level3 = false;
                    //GameManager.instance.GetAudioManager().FadeOutMusic(Helper.Audio_Music_Level3, 10);

                    UnloadAll();

                    Time.timeScale = 0;

                    if (!hasWon)
                        __event<e_UI>.InvokeEvent(this, e_UI.MENU, true);
                    else
                        __event<e_UI>.InvokeEvent(this, e_UI.WINGAME, true);
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
