using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arkaplanHareket : MonoBehaviour {

    public Transform arkaplan;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (arkaplan.position.y > transform.position.y)
        {
            Vector3 position = new Vector3(0, arkaplan.position.y, -10);
            transform.position = position;
        }
    }
}
