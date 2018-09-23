using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + "has entered " + this.name);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(other.name + "has exited " + this.name);
    }
}
