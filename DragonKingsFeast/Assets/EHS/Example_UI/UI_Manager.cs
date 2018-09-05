using UnityEngine;

public class UI_Manager : MonoBehaviour {
	void Start () {
        __event<e_UI>.InvokeEvent(this, e_UI.MENU, true);
	}
}
