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

        colorRenderer.materials[materialIndex].color = victim.player.color;
    }
}
