using UnityEngine;

public class UI_Call : MonoBehaviour {
    public e_UI active_type;

    public void Call () {
        __event<e_UI>.InvokeEvent(this, active_type, true);
    }
}
