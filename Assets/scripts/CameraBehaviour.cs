using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public Transform transformCamera;
	public GameObject target;
	public float maxDistanceZ;
	public float maxDistanceX;
	public float lerpSpeed;


	private Transform targetPos;
	private Quaternion initalRotationVector;
	private float initalPosY;
	private float currentXPos;
	private float currentZPos;
	private float lerpZTimer;
	private float lerpXTimer;
	private bool lerpZ;
	private bool lerpZOppisite;
	private bool lerpX;

    // Start is called before the first frame update
    void Start()
    {
		initalRotationVector = transformCamera.rotation;
		initalPosY = transformCamera.position.y;
		targetPos = target.transform;
	}

    // Update is called once per frame
    void Update()
    {
		//never let camera rotate
		transformCamera.rotation = initalRotationVector;
		currentXPos = transformCamera.position.x;
		currentZPos = transformCamera.position.z;
		lerpZ = false;
		lerpZOppisite = false;
		lerpX = false;

		transformCamera.position = new Vector3( transformCamera.position.x, transformCamera.position.y, targetPos.position.z - maxDistanceZ );
		
	}
}
