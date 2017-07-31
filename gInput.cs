using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using UnityEngine.UI;
public class gInput : MonoBehaviour {

	public static bool gameOn;
	public LightSource torch;
	public Transform playerTransform;
	public MoveController playerMoveController;
	public static ActionController actionController;
	public float cursorStartDistance = 1;
	public float cursorEndDistance = 15;

	public string actionInputID = "Fire1";
	public string hurryInputID = "Fast";
	public bool actionTapped;
	public float actionTapTimer;
	public float actionTapTime = 0.3f;

	public static Vector3 mouse;
	public static Vector3 mouseWorld;
	public Vector3 move;
	public float moveForce;
	public bool isMoving;
	public bool hurryDown;

	public Transform realtiveMoveTarget;

	public Slider progressBar;

	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		if(playerTransform){
			playerMoveController = playerTransform.GetComponent<MoveController>();
			actionController = playerTransform.GetComponent<ActionController>();
		}
		actionTapTimer = actionTapTime;	
	}


	private Vector3 actionDirection;

	void Update () {
		if(!gameOn){
			playerMoveController.Move(Vector3.zero, 0, false);
			return;

		}
		CheckLight();
		move.x = Input.GetAxis(horizontal);
		move.z = Input.GetAxis(vertical);
		hurryDown = Input.GetButton(hurryInputID);

		if(Input.GetButtonDown(actionInputID) && actionTapTimer >= actionTapTime){
			Timing.RunCoroutine(_StartAction());
		}
			
		if(actionController.inAction){
			
			if(gCursor.hitAction){
				actionDirection = gCursor.hitAction.transform.position - playerTransform.position;

				playerMoveController.SetRotationGoal(actionDirection);
				if(Vector3.Distance(playerTransform.position, gCursor.hitAction.transform.position) > gCursor.hitAction.autoMoveDistance )
					playerMoveController.Move(actionDirection, playerMoveController.moveSpeedFast*2f , true);
			}
			return;
		}

		moveForce = move.sqrMagnitude;
		GetMousePoint();
		relativeDir = realtiveMoveTarget.TransformDirection(move);	
		
		playerMoveController.SetRotationGoal(mouseWorld - playerTransform.position);
		playerMoveController.Move(relativeDir, moveForce, hurryDown);


	}

	IEnumerator<float> _StartAction(){
		actionTapTimer = 0f;

		while(actionTapTimer < actionTapTime){
			actionTapTimer += Time.deltaTime;
			if(Input.GetButtonUp(actionInputID)){
				actionTapped = true;
				InputAction(false);
			}

			yield return 0f;
		}

		if(!actionTapped)
			InputAction(true);

		actionTapped = false;
	}

	void InputAction(bool press){
		
		actionController.PerformAction(gCursor.hitAction, press);
	
	}

	void GetMousePoint(){
		currentEvent = Event.current;

		mouse.x = Input.mousePosition.x;
		mouse.y = Input.mousePosition.y;

		UpdateCursor();  //update cursor to get world point befor character distance to camera;

		mouse.z = 8f;
		mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
	}

	void UpdateCursor(){
		mouse.z = cursorStartDistance;
		mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
		gCursor.cursorWorldPos = mouseWorld;
		mouse.z = cursorStartDistance+cursorEndDistance;
		mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
		gCursor.cursorWorldEndPos = mouseWorld;
	}


	void CheckLight(){
		hits = Physics.OverlapSphere(playerTransform.position,maxLightCheckRadius,lightLayer,QueryTriggerInteraction.Collide);
		hitCount = hits.Length;
		lightStrengthGoal = 0f;
		if (hitCount > 0){
			for(counter = 0; counter<hitCount; counter++){
				lightSource = hits[counter].GetComponent<LightSource>();
				lightStrengthGoal += lightSource.LightStrength(playerTransform.position);
			}
		}
		lightStrengthGoal += torch.LightStrength(torch.transform.position);
		lightStrengthGoal = Mathf.Clamp01(lightStrengthGoal);
		lightStrength = Mathf.SmoothDamp(lightStrength, lightStrengthGoal, ref lightVelocity, lightDamp * Time.deltaTime);

		lightSlider.value = lightStrength;

		if(lightStrength <= 0f){
			wEvent.EndTheGame();

		}
	}

	public int hitCount;
	private int counter;
	private Collider[] hits;
	public LightSource lightSource;
	public float lightStrength;
	private float lightStrengthGoal, lightVelocity;
	public float lightDamp = 3f;
	public Slider lightSlider;
	public float maxLightCheckRadius = 100f;
	public LayerMask lightLayer;
	public Vector3 relativeDir;
	private Event currentEvent;
	private string horizontal = "Horizontal";
	private string vertical = "Vertical";
	private string fire1 = "Fire1";
	private string fire2 = "Fire2";
	public WorldEvents wEvent;
}

