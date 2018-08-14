using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_ItemType {
    Boost_Speed,
    Boost_Attack,
    Boost_Defense,
    Pickup,
}

public class Item : MonoBehaviour {
    protected e_ItemType m_itemType;
    public e_ItemType ItemType { get { return m_itemType; } }

    public virtual void TurnOff() { GameManager.instance.GetObjectPool().AddItemTooPool(this); }
    public virtual void TurnOn() { Reset(); this.gameObject.SetActive(true); }
    public virtual void Reset() { }
}
