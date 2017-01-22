using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public int index;
    public int controllerIndex;

    [HideInInspector]
    public Color color {
        get {
            return realColor;
        }
    }

    public Color realColor {
        get {
            return COLORS[controllerIndex];
        }
    }

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
        new Color(0.7f,0, 0), new Color(1, 0.5f, 0), new Color(0,0.7f, 0),new Color(0,0, 0.7f)
    };
}
