using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PageFlipController : MonoBehaviour,  IVirtualButtonEventHandler {

    public enum FlipDirection {
        Next,
        Previous
    }

    public List<Texture> Pages;
    public GameObject OddPageHolder;
    public GameObject EvenPageHolder;
    public VirtualButtonBehaviour FlipNextButton;
    public VirtualButtonBehaviour FlipPreviousButton;
    public GameObject FlipNextButtonBackground;
    public GameObject FlipPreviousButtonBackgroud;
    public Material VirtualButtonPressedMaterial;
    public Material VirtualButtonReleasedMaterial;

    public int currentPage = 1;
    public float animationDurationInSeconds = 1;

    public string AnimationKeyFramesTag = "MagazineAnimationKeyframeMeshes";
    public string FlipAnimationTriggerName = "flipTrigger";
    public string IdleStateTriggerName = "idleTrigger";
    public string IsDirectionForwardParameterName = "isDirectionForward";


    private Animator PageFlipAnimator;
    private List<GameObject> AnimationKeyFrames;
    private bool IsFlipping = false;

    private Material[] OddPageMaterials;
    private Material[] EvenPageMaterials;

    // Use this for initialization
    void Start () {
        PageFlipAnimator = this.GetComponent<Animator>();

        AnimationKeyFrames = new List<GameObject>();
        foreach(Transform transform in this.transform) {
            GameObject frame = transform.gameObject;
            if(frame.CompareTag(AnimationKeyFramesTag)) {
                AnimationKeyFrames.Add(frame);
                frame.SetActive(false);
            }
        }
        Debug.Log("AnimationKeyFrames: " + AnimationKeyFrames.Count);

        OddPageMaterials = OddPageHolder.GetComponent<Renderer>().materials;
        EvenPageMaterials = EvenPageHolder.GetComponent<Renderer>().materials;

        FlipNextButton.RegisterEventHandler(this);
        FlipPreviousButton.RegisterEventHandler(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool CanFlip(FlipDirection direction) {
        return (direction == FlipDirection.Next && currentPage < Pages.Count) || (direction == FlipDirection.Previous && currentPage > 1);
    }

    public void Flip(FlipDirection direction) {
        if(IsFlipping) {
            Debug.Log("Flipping already in progress");
            return;
        }

        if(!CanFlip(direction)) {
            Debug.Log("Cannot flip further");
            return;
        }

        IsFlipping = true;

        int newPage = currentPage;
        int indexOfRequiredPage = currentPage;
        if (direction == FlipDirection.Next) {
            newPage += 2;
            indexOfRequiredPage = currentPage - 1;
        }
        else {
            newPage -= 2;
            indexOfRequiredPage = newPage - 1;
        }

        foreach(GameObject frame in AnimationKeyFrames) {
            Material[] materials = frame.GetComponent<Renderer>().materials;
            materials[0].mainTexture = Pages[indexOfRequiredPage];
            if (currentPage < Pages.Count) {
                materials[1].mainTexture = Pages[indexOfRequiredPage + 1];
            }
        }

        currentPage = newPage;

        // Trigger Animation
        PageFlipAnimator.SetBool(IsDirectionForwardParameterName, direction == FlipDirection.Next);
        PageFlipAnimator.SetTrigger(FlipAnimationTriggerName);

        GetComponent<AudioSource>().enabled = true;

        StartCoroutine(ResetFlippingState(direction));
    }

    IEnumerator ResetFlippingState(FlipDirection direction) {
        if(!IsFlipping) {
            yield break;
        }

        if (direction == FlipDirection.Next) {
            OddPageMaterials[0].mainTexture = Pages[currentPage - 1];
            if (currentPage < Pages.Count) {
                OddPageMaterials[1].mainTexture = Pages[currentPage];
            }
        }
        else if(currentPage > 1) {
            EvenPageMaterials[0].mainTexture = Pages[currentPage - 3];
            EvenPageMaterials[1].mainTexture = Pages[currentPage - 2];
            EvenPageHolder.SetActive(true);
        }
        else {
            EvenPageHolder.SetActive(false);
        }

        yield return new WaitForSeconds(animationDurationInSeconds);

        if (currentPage == 1) {
            EvenPageHolder.SetActive(false);
        }
        else {
            EvenPageMaterials[0].mainTexture = Pages[currentPage - 3];
            EvenPageMaterials[1].mainTexture = Pages[currentPage - 2];
            EvenPageHolder.SetActive(true);
        }

        if (direction == FlipDirection.Previous) {
            OddPageMaterials[0].mainTexture = Pages[currentPage - 1];
            if (currentPage < Pages.Count) {
                OddPageMaterials[1].mainTexture = Pages[currentPage];
            }
        }

        GetComponent<AudioSource>().enabled = false;
        PageFlipAnimator.SetTrigger(IdleStateTriggerName);
        IsFlipping = false;
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb) {
        Debug.Log("Virtual button pressed: " + vb.VirtualButtonName);

        if (vb == FlipNextButton) {
            FlipNextButtonBackground.GetComponent<Renderer>().material = VirtualButtonPressedMaterial;
            Flip(FlipDirection.Next);
        }
        else {
            FlipPreviousButtonBackgroud.GetComponent<Renderer>().material = VirtualButtonPressedMaterial;
            Flip(FlipDirection.Previous);
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb) {
        Debug.Log("Virtual button released: " + vb.VirtualButtonName);
        if(vb == FlipNextButton) {
            FlipNextButtonBackground.GetComponent<Renderer>().material = VirtualButtonReleasedMaterial;
        }
        else {
            FlipPreviousButtonBackgroud.GetComponent<Renderer>().material = VirtualButtonReleasedMaterial;
        }
    }
}
