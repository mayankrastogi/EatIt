using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaySceneSound : MonoBehaviour, ITrackableEventHandler {
    
    // Use this for initialization
    void Start () {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        Debug.Log("State changed from " + previousStatus + " to " + newStatus);
        if(newStatus == TrackableBehaviour.Status.TRACKED) {
            GetComponent<AudioSource>().enabled = true;
        }
        else {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
