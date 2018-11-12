using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallUI : MonoBehaviour {
    public enum UIDisplayType {
        Fireballs,
        AttackBoost,
        ShildBoost,
    } public UIDisplayType m_UIDisplayType;

    public List<Texture2D> elementsUI;
    private bool hasShild = false;
    private bool hasAttack = false;
    RawImage UIImage;
    Texture2D temp;



    private void Start() {
        UIImage = GetComponent<RawImage>();
        temp = new Texture2D(1, 1);
        HideImage();
    }

    void Update () {
        if (elementsUI.Count != 6) return;

        switch (m_UIDisplayType) {
            case UIDisplayType.Fireballs:
                ShowImage();
                UIImage.texture = elementsUI[(int)GameManager.player.bulletAmmount];
                break;
            case UIDisplayType.AttackBoost:
                if (GameManager.player.attackBoostTimer < GameManager.player.maxAttackBoostTime) {
                    if (!hasAttack) {
                        hasAttack = true;
                        ShowImage();
                        UIImage.texture = elementsUI[5];
                        StartCoroutine(AttackBoostCountDown());
                    }
                } else {
                    HideImage();
                    hasAttack = false;
                }
                break;
            case UIDisplayType.ShildBoost:
                if (GameManager.player.sheildBoost) {
                    ShowImage();
                    ShowShildBoost();
                    hasShild = true;
                } else {
                    if (hasShild) {
                        StartCoroutine(ShildBoostBreak());
                        hasShild = false;
                    }
                }
                break;
        }
	}

    void ShowImage() {
        UIImage.color = new Color(1, 1, 1, 1);
    }

    void HideImage() {
        UIImage.texture = temp;
        UIImage.color = new Color(0, 0, 0, 0);
    }

    void ShowShildBoost() {
        UIImage.texture = elementsUI[0];
    }

    IEnumerator ShildBoostBreak() {
        for(int i = 0; i < elementsUI.Count; i++) {
            UIImage.texture = elementsUI[i];
            yield return new WaitForSeconds(0.1f);
        }
        HideImage();
    }

    IEnumerator AttackBoostCountDown() {
        for (int i = elementsUI.Count - 1; i >= 0; i--) {
            UIImage.texture = elementsUI[i];
            yield return new WaitForSeconds(GameManager.player.maxAttackBoostTime / 5);
        }
    }

}
