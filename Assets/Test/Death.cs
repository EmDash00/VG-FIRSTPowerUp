using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.rigidbody.velocity = Vector2.zero;
            col.gameObject.transform.position = new Vector2(-34.5f, 14.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
