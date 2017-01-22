using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStructure : MonoBehaviour {
	
    [SerializeField]
    private Material editMaterial;

    [SerializeField]
    private Renderer visualRenderer;

    private Material defaultMaterial;

	public bool isEditing = false;

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
			isEditing = true;
            ChangeMaterial(editMaterial);
        }else{
			isEditing = false;
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
		if (isBlocked) {
			visualRenderer.material.SetColor("_ObjectColor", new Color(1, 0, 0, 0.5f));
		} else {
			visualRenderer.material.SetColor("_ObjectColor", new Color(0, 1, 0.216f, 0.5f));
		}
		if (visualRenderer.transform.childCount > 0)
		{
			for (int i = 0; i < visualRenderer.transform.childCount; i++)
			{
				if (visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
					visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>().material = visualRenderer.material;
			}
		}
	}
}
