using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour {
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
		ReadyPlayers = new bool[4];
    }

    public int NRPlayer { get; set; }

	public bool[] ReadyPlayers { get; set; }
}
