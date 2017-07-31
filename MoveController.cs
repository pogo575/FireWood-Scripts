using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	public Transform bodyTarget;


	public float moveSpeed = 1.8f;
	public float moveSpeedFast = 3.5f;
	public float moveBackModifier = 0.7f;
	public float moveStrafeModifier = 0.8f;

	public float turnDamp = 4f;
	public float moveDamp = 2f;

	public bool isMoving;
	public bool moveFast;

	private Vector3 moveRelative;
	private Vector3 moveVelocity;
	private float moveMod;
	// Use this for initialization
	void Start () {
		thisTransform = transform;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		angle = mainDir.y;

		deltaTime = Time.deltaTime;

		angle = Mathf.SmoothDampAngle(angle, angleGoal, ref angleVelocity, turnDamp * deltaTime);
		mainDir.y = ClampAngle(angle, -360f,360f);
		thisTransform.eulerAngles = mainDir;


		if(moveDir != Vector3.zero)
			moveRelative = moveDir;
		else 
			moveRelative = Vector3.zero;

		if(moveRelative.z < 0f)
			moveRelative.z *= moveBackModifier;
		if(moveRelative.x != 0f)
			moveRelative.x *= moveStrafeModifier;

		moveGoal = Vector3.SmoothDamp(moveGoal, moveRelative, ref moveVelocity, moveDamp*Time.deltaTime);

		if(anim){
			anim.SetFloat(moveX , moveGoal.x);
			anim.SetFloat(moveZ , moveGoal.z);
			anim.SetFloat(turnVelocity, angleVelocity);
		}
		else {
			characterController.SimpleMove(moveGoal);
		}

	}

	public void Move(Vector3 dir, float mMod, bool isFast){
		moveFast = isFast;
		moveMod = Mathf.Clamp01(mMod);
		if(!moveFast)
			moveDir = dir.normalized*moveSpeed; 
		else 
			moveDir = dir.normalized*moveSpeedFast;
	}

	public void SetRotationGoal(Vector3 dir){
		if(!moveFast)
			angleGoal = (Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg);
		else 
			angleGoal = (Mathf.Atan2(moveDir.x,moveDir.z) * Mathf.Rad2Deg);
	}

	private float ClampAngle (float ang, float min, float max) {
		if (ang < -360) ang += 360;
		if (ang > 360) ang -= 360;
		return Mathf.Clamp (ang, min, max);
	}

	public Vector3 moveGoal;
	public Vector3 moveDir;
	private Quaternion bodyRot,bodyRotGoal;
	public Animator anim;

	private int animPosture = Animator.StringToHash("posture");
	private int moveX =  Animator.StringToHash("MoveX");
	private int moveZ =  Animator.StringToHash("MoveZ");
	private int moveState =  Animator.StringToHash("Move");
	private int turnVelocity = Animator.StringToHash("TurnVelocity");
	private CharacterController characterController;
	private Transform thisTransform;
	public float angle, deltaTime, angleGoal, angleVelocity;

	public Vector3 mainDir;
}
