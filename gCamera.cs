using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gCamera : MonoBehaviour {
	public Transform trackTarget;
	public Vector3 trackPosition;

	private Vector3 trackVelocity;
	public float trackDamp = 2f;
	private Vector3 trackGoal;
	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		trackGoal = trackTarget.position;
		trackPosition = Vector3.SmoothDamp(trackPosition, trackGoal, ref trackVelocity, trackDamp*Time.deltaTime);
		thisTransform.position = trackPosition;
	}

	private Transform thisTransform;
}
