using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallUI : MonoBehaviour {

    /// <summary>
    /// Type of UI
    /// </summary>
    public enum UIDisplayType {
        Fireballs,
        AttackBoost,
        ShildBoost,
    } public UIDisplayType m_UIDisplayType;

    /// <summary>
    /// List of the textures to loop through
    /// </summary>
    public List<Texture2D> elementsUI;

    /// <summary>
    /// Does the player have a shield
    /// </summary>
    private bool hasShild = false;

    /// <summary>
    /// Does the player have an attack boosts
    /// </summary>
    private bool hasAttack = false;

    /// <summary>
    /// UI element to edit
    /// </summary>
    RawImage UIImage;

    /// <summary>
    /// Temp image thats 1x1px to uses when no other texture it present
    /// </summary>
    Texture2D temp;


    /// <summary>
    /// Set up the UI element and temp texture, then hide the image
    /// </summary>
    private void Start() {
        UIImage = GetComponent<RawImage>();
        temp = new Texture2D(1, 1);
        HideImage();
    }

    /// <summary>
    /// Updates the UI element to show the boosts
    /// </summary>
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

    /// <summary>
    /// Shows the UI element
    /// </summary>
    void ShowImage() {
        UIImage.color = new Color(1, 1, 1, 1);
    }

    /// <summary>
    /// Hides the UI element
    /// </summary>
    void HideImage() {
        UIImage.texture = temp;
        UIImage.color = new Color(0, 0, 0, 0);
    }

    /// <summary>
    /// Shows the shield boost
    /// </summary>
    void ShowShildBoost() {
        UIImage.texture = elementsUI[0];
    }

    /// <summary>
    /// Plays the shield boost break animation
    /// </summary>
    /// <returns></returns>
    IEnumerator ShildBoostBreak() {
        for(int i = 0; i < elementsUI.Count; i++) {
            UIImage.texture = elementsUI[i];
            yield return new WaitForSeconds(0.1f);
        }
        HideImage();
    }

    /// <summary>
    /// Counts down on the attack boost animation
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackBoostCountDown() {
        for (int i = elementsUI.Count - 1; i >= 0; i--) {
            UIImage.texture = elementsUI[i];
            yield return new WaitForSeconds(GameManager.player.maxAttackBoostTime / 5);
        }
    }

}
