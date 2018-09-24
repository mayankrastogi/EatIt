using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject NutritionInfo;
    public List<GameObject> AlwaysVisibleObjects;
    public AudioClip PlacematReactionAudio;

    public string PlacematTagName = "Placemat";

    private bool IsOnPlacemat = false;
    private bool IsReacting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ShowScene() {
        foreach (Transform childObject in this.transform) {
            childObject.gameObject.SetActive(true);
        }
    }

    public void HideScene() {
        foreach (Transform childObject in this.transform) {
            GameObject childGameObject = childObject.gameObject;
            if (!AlwaysVisibleObjects.Contains(childGameObject)) {
                childGameObject.SetActive(false);
            }
        }
    }

    public void ShowNutritionInfo() {
        if (NutritionInfo) {
            NutritionInfo.SetActive(true);
        }
    }

    public void HideNutritionInfo() {
        if (NutritionInfo) {
            NutritionInfo.SetActive(false);
        }
    }

    private void StartReacting() {
        IsReacting = true;
        Debug.Log(this.name + "has started reacting");
    }

    private void StopReacting() {
        Debug.Log(this.name + "has stopped reacting");
        IsReacting = false;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + "has entered " + this.name);

        if(IsOnPlacemat) {
            return;
        }

        if(other.CompareTag(PlacematTagName)) {
            IsOnPlacemat = true;
            if (IsReacting) {
                StopReacting();
            }
            HideScene();
            ShowNutritionInfo();
        }
        else if(!IsReacting){
            StartReacting();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(other.name + "has exited " + this.name);

        if(IsOnPlacemat && other.CompareTag(PlacematTagName)) {
            ShowScene();
            HideNutritionInfo();
            IsOnPlacemat = false;
        }
        else if(IsReacting) {
            StopReacting();
        }
    }
}
