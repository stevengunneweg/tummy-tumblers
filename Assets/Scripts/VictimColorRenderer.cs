using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimColorRenderer : MonoBehaviour {

    public Renderer colorRenderer;
    public int materialIndex = 0;

	protected void Update() {
        Victim victim = GetComponentInParent<Victim>();
        if (victim == null)
            return;

        Color color = victim.player.color;
        if (victim.index == 0) {
            color = color.Lighter();
        } else {
            color = color.Darker();
        }
        colorRenderer.materials[materialIndex].color = color;
    }
}
