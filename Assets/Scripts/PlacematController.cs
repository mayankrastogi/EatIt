using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacematController : MonoBehaviour {

    public GameObject PlacematMascot;
    public string MascotAnimatorIsSpeakingParameterName = "IsSpeaking";

    private bool IsMascotSpeaking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + " was placed on the Placemat");

        if(IsMascotSpeaking) {
            return;
        }

        SceneController sceneController = other.GetComponent<SceneController>();

        if(sceneController) {
            AudioClip placematReactionAudio = sceneController.PlacematReactionAudio;

            if(placematReactionAudio) {
                StartCoroutine(Speak(placematReactionAudio));
            }
        }
    }

    IEnumerator Speak(AudioClip audioClip) {
        float animationDuration = audioClip.length;

        Animator mascotAnimator = PlacematMascot.GetComponent<Animator>();
        if (mascotAnimator) {
            IsMascotSpeaking = true;
            mascotAnimator.SetBool(MascotAnimatorIsSpeakingParameterName, true);

            AudioSource audioSource = PlacematMascot.GetComponent<AudioSource>();
            if(audioSource) {
                audioSource.PlayOneShot(audioClip);
            }

            yield return new WaitForSeconds(animationDuration);

            mascotAnimator.SetBool(MascotAnimatorIsSpeakingParameterName, false);
            IsMascotSpeaking = false;
        }
    }
}
