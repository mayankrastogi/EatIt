using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAnimationController : MonoBehaviour {

    public string IsFlyingParameterName = "IsFlying";

    private Animator BeeAnimator;

	// Use this for initialization
	void Start () {
        BeeAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIsFlying(int shouldFly) {
        BeeAnimator.SetBool(IsFlyingParameterName, shouldFly == 1);
    }
}
