using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectAnimationBehavior : MonoBehaviour {

    public string IsFlyingParameterName = "IsFlying";

    private Animator FlyingObjectAnimator;

	// Use this for initialization
	void Start () {
        FlyingObjectAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIsFlying(int shouldFly) {
        FlyingObjectAnimator.SetBool(IsFlyingParameterName, shouldFly == 1);
    }
}
