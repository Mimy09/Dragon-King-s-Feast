using UnityEngine;

public class UI_TUTRIAL_Call : MonoBehaviour {
    public e_UI_TUTRIAL active_type;

    public void Call () {
        __event<e_UI_TUTRIAL>.InvokeEvent(this, active_type, true);
    }
}
