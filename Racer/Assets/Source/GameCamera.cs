using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour {

	[SerializeField]
	private bool smoothFollow = true;
	[SerializeField]
	private float cameraHeightOffset;
	[SerializeField]
	private float horizontalSpeed = 2.0f;
	[SerializeField]
	private float verticalSpeed = 2.0f;
	[SerializeField]
	private float cameraDrag = 0.1f;
	[SerializeField]
	private float maxCameraDistance = 0.2f;
	[SerializeField]
	private float standardCameraDistance = 0.1f;
	[SerializeField]
	private Transform cameraRig;
	[SerializeField]
	private Transform pitchTransform;
	[SerializeField]
	private List<Transform> followTarget = new List<Transform>();
	//followTarget 0 = player
	//followTarget 1 = player's mech

	private Transform mainFollowTarget;
	private Transform settleDistance;
	private Vector3 previousPosition;
	private float verticalRate;
	private float horizontalRate;
	private bool isMouseYLocked = false;

	public float VerticalSpeed { get{ return verticalSpeed;} }

	//public Transform PitchTransform { get { return pitchTransform; } }
	void Awake()
	{
		mainFollowTarget = followTarget[0];
		cameraRig = this.transform;
		settleDistance = this.transform;
	}
	
	void Update() 
	{
		MouseLook();
		//transform.LookAt(mainFollowTarget);
		SmoothFollow();
	}

	public void LockMouseLookY()
	{
		isMouseYLocked = true;
	}

	public void UnlockMouseLookY()
	{
		isMouseYLocked = false;
	}

	private void MouseLook()
	{
		if(!isMouseYLocked)
		{
    		verticalRate += -verticalSpeed * Input.GetAxis("Mouse Y");
			horizontalRate += horizontalSpeed * Input.GetAxis("Mouse X");
		}
    	verticalRate = Mathf.Clamp(verticalRate, -30, 30);
    	transform.rotation = Quaternion.Euler(verticalRate, horizontalRate, 0);
	}

	//mainFollowTarget is switched to
	public void SwitchFollowTarget(int targetIndex)
	{
		mainFollowTarget = followTarget[targetIndex];
	}

	private void SmoothFollow()
	{
		float distance = Vector3.Distance(mainFollowTarget.position, cameraRig.position);
		Vector3 targetPosition = mainFollowTarget.position + new Vector3(0,cameraHeightOffset,0);
		
		//targetPosition.y = cameraRig.position.y;
		//	Debug.Log(cameraRig.position + " " + mainFollowTarget.position);
		if(mainFollowTarget.position != previousPosition)
		{	
			//change in position delta will slow down the camera more
			if(maxCameraDistance < distance)
			{
				cameraRig.position = Vector3.MoveTowards(cameraRig.position, targetPosition, 1);
			}
		}
		else if( standardCameraDistance < distance)
		{
			cameraRig.position = Vector3.MoveTowards(cameraRig.position, targetPosition, 1);
		}

		previousPosition = mainFollowTarget.position;
	}
}
