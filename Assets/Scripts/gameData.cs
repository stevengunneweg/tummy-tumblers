using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour {
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int NRPlayer { get; set; }
}
