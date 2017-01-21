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
            Debug.Log("visualrender has child");
            for (int i = 0; i < visualRenderer.transform.childCount; i++)
                visualRenderer.transform.GetChild(i).GetComponent<MeshRenderer>().material = material;
        }
        else
        {
            Debug.Log("visualrender has no child");
        }
    }

	public void SetBlocked(bool isBlocked) {
		visualRenderer.material = editMaterial;
		if (visualRenderer.transform.childCount > 0) {
			for (int i = 0; i < visualRenderer.transform.childCount; i++) {
				if (isBlocked) {
					visualRenderer.transform.GetChild (i).GetComponent<MeshRenderer> ().material.SetColor("_ObjectColor", Color.red);
				} else {
					visualRenderer.transform.GetChild (i).GetComponent<MeshRenderer> ().material.SetColor("_ObjectColor", Color.green);
				}
			}
		}
	}
}
