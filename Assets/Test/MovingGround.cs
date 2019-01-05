using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour {

    public Vector2 initVelocity;

    private Rigidbody2D body;

    // Use this for initialization
    void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = initVelocity;
    }
}
