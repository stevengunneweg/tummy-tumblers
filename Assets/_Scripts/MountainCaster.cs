using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCaster : MonoBehaviour {

    [SerializeField]
    private GameObject _hillPrefab;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      Ray ray = camera.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit)) {
        SpawnObject(hit.point);
        // Do something with the object that was hit by the raycast.
      }
    }
    }
    void SpawnObject(Vector3 pos)
    {
        GameObject hill = Instantiate(_hillPrefab, pos, this.transform.rotation);
        hill.transform.SetParent(this.transform);
    }

}
