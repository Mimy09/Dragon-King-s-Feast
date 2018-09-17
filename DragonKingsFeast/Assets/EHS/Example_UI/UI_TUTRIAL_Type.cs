using UnityEngine;

public class UI_TUTRIAL_Type : MonoBehaviour {
    public e_UI_TUTRIAL type;

    private void Awake () {
        __event<e_UI_TUTRIAL>.Raise(this, EventHandle);
    }

    private void EventHandle (object s, __eArg<e_UI_TUTRIAL> e) {
        if (e.arg == type) {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        } else {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
