using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEndUI : MonoBehaviour {

    public Image background;
    public Text nameText, scoreText;

    public void SetFrom(Player p) {
        background.color = p.color;
        nameText.text = p.name;
        scoreText.text = "Score: " + p.score.ToString();
    }
}
