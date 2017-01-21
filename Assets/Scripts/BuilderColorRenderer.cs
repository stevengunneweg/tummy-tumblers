using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderColorRenderer : MonoBehaviour {

    public Renderer colorRenderer;
    public int materialIndex = 0;

    protected void Update() {
        Builder builder = GetComponentInParent<Builder>();
        if (builder == null)
            return;

        colorRenderer.materials[materialIndex].color = builder.player.color;
    }

}
