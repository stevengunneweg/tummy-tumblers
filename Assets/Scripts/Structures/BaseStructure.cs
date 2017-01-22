using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStructure : MonoBehaviour {
	
    [SerializeField]
    private Material editMaterial;

    [SerializeField]
    private Renderer visualRenderer;

    private Material defaultMaterial;

    public abstract void OnCollisionWithVictim(Collision collision, Victim victim);

    private void Start()
    {
        defaultMaterial = visualRenderer.material;
    }

    private void OnCollisionEnter(Collision collision){
        Victim victim = collision.collider.GetComponent<Victim>();

        if(victim != null){
            OnCollisionWithVictim(collision, victim);
        }
    }   

    public void SetEditMode(bool inEditMode){
        if(inEditMode){
            ChangeMaterial(editMaterial);
        }else{
            ChangeMaterial(defaultMaterial);
        }
    }
    private void ChangeMaterial(Material material)
    {
        visualRenderer.material = material;
        if (visualRenderer.transform.childCount > 0)
        {
            for (int i = 0; i < visualRenderer.transform.childCount; i++)
            {
                if (visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                    visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>().material = material;
            }
        }
    }

	public void SetBlocked(bool isBlocked) {
		visualRenderer.material = editMaterial;
		if (visualRenderer.transform.childCount > 0) {
			for (int i = 0; i < visualRenderer.transform.childCount; i++) {
				MeshRenderer renderer = visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>();
				if (renderer != null) {
					if (isBlocked) {
						renderer.material.SetColor("_ObjectColor", Color.red);
					} else {
						renderer.material.SetColor("_ObjectColor", Color.green);
					}
				}
			}
		}
	}
}
