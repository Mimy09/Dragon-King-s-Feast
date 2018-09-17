using UnityEngine;

public class UI_Manager : MonoBehaviour {
	void Start () {
        __event<e_UI>.InvokeEvent(this, e_UI.MENU, true);
        __event<e_UI_TUTRIAL>.InvokeEvent(this, e_UI_TUTRIAL.NULL, true);
    }
}
