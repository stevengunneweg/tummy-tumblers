using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOneChild : MonoBehaviour {
    
	protected void Start () {
        int i = 0;
        int c = Random.Range(0, transform.childCount);
		foreach (Transform child in transform) {
            child.gameObject.SetActive(i == c);
            i++;
        }
	}
}
