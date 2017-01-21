using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Transform playerParent;

    public Image background;
    public Text nameText;
    public Text scoreText;

    protected void Update() {
        int index = transform.GetSiblingIndex();

        if (index < playerParent.childCount) {
            Transform playerTransform = playerParent.GetChild(index);
            Player player = playerTransform.GetComponent<Player>();
            nameText.text = "Player " + (player.index + 1);
            scoreText.text = player.score.ToString() + "/15";

            nameText.color = player.color;
            scoreText.color = player.color;
        } else {
            gameObject.SetActive(false);
        }
    }
}
