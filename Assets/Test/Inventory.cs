using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private GameObject textGameObject;

    private Text textbox;

    private long count = 0;

	// Use this for initialization
	void Start ()
    {
        this.textbox = this.textGameObject.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update ()
    {
      
        this.textbox.text = count.ToString();
    }


    public void add(long cubes)
    {
        count += cubes;
    }
}
