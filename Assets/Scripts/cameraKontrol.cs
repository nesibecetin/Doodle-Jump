using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraKontrol : MonoBehaviour {

    public Transform karakter;

	void LateUpdate ()
    {
		if(karakter.position.y>transform.position.y)
        {
            Vector3 position = new Vector3(0, karakter.position.y,-10);
            transform.position = position;
        }
	}
}
