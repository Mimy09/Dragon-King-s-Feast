using UnityEngine;

public class UI_Type : MonoBehaviour {
    public e_UI type;

    private void Awake () {
        __event<e_UI>.Raise(this, EventHandle);
    }

    private void EventHandle (object s, __eArg<e_UI> e) {

        if (e.arg == e_UI.LOADING && type == e_UI.LOADING)
            GameManager.instance.GetMapManager().StartLoad();

        if (e.arg == e_UI.EXIT) 
            Application.Quit();

        if (e.arg == type) {
            //for (int i = 0; i < transform.childCount; i++)
            //    transform.GetChild(i).gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        } else {
            //for (int i = 0; i < transform.childCount; i++)
            //    transform.GetChild(i).gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        if (e.arg == e_UI.INGAMEMENU) {
            Time.timeScale = 0;
        }

        if (e.arg == e_UI.GAME) {
            Time.timeScale = 1;
        }

        if (e.arg == e_UI.MENU && (bool)e.value == true && type == e_UI.MENU) {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }
    }
}
