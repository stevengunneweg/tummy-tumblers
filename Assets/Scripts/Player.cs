using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public int index;

    [HideInInspector]
    public Color color;

    public int score = 0;

    public static readonly Color[] COLORS = new Color[]{
        Color.red, Color.yellow, Color.green, Color.blue
    };
}
