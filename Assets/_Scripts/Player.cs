using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int Index { get; private set; }
    public Color Color { get; private set; }

    public Player(int index, Color color){
        Index = index;
        Color = color;
    }

}
