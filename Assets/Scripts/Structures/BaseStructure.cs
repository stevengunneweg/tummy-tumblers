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

    private void Start(){
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
            visualRenderer.material = editMaterial;
        }else{
            visualRenderer.material = defaultMaterial;
        }
    }
}
