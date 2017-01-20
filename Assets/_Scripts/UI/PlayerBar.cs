using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour {

    [SerializeField]
    private Image image;

    public void SetPlayer(Player player){
        image.color = player.Color;
    }
}
