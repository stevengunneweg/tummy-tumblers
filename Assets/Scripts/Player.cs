using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public int index;
    public int controllerIndex;

    [HideInInspector]
    public Color color;

    public int score = 0;

    public string GetAxisPrefix() {
        switch (controllerIndex) {
            case 0: return "P1";
            case 1: return "P2";
            case 2: return "P3";
            case 3: return "P4";
            default: return "P1";
        }
    }

    public static readonly Color[] COLORS = new Color[]{
        Color.red, Color.yellow, Color.green, Color.blue
    };
}
