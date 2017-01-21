using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLinq;

public class SpawnPointGroup : MonoBehaviour {

	public Transform[] GetSpawnPoints(int amount) {
        return GetComponentsInChildren<Transform>().Skip(1).Take(amount).ToArray();
    }
}
