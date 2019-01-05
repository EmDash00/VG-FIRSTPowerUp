using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            col.gameObject.GetComponent<Move>().checkpoint = col.gameObject.transform;
        }
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
