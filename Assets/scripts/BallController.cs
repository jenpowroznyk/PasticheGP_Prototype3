using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

	public float maxSpeed;
	public float appliedForce;
	public float appliedForceOppisite;
	public float appliedForceThreshold;
	public float growthRate;
	public float shrinkRate;
	public float shrinkRateInstant;
	public float growthRateInstant;
	public float minSize;
	public float maxSize;
	public float jumpForce;
	public Transform spawnPos;
	public Vector3 spawnPosVector;

	public Rigidbody rigidBody;
	public Transform ballTransform;

	private float timer = 0f;
	private float initalLerpValue;
	private bool startGrowLerp;
	private bool startShrinkLerp;
	private bool applyOppisiteForce;
	private bool canJump;


	// Start is called before the first frame update
	void Start()
    {
		
	}

	private void BallGrow ()
	{
		if ( ballTransform.localScale.magnitude < maxSize )
		{
			ballTransform.localScale += new Vector3( growthRate, growthRate, growthRate );
		}	
	}

	private void BallGrowInstant ()
	{
		
		initalLerpValue = ballTransform.localScale.x;
		if ( (initalLerpValue + growthRateInstant) > maxSize )
		{
			ballTransform.localScale = new Vector3( maxSize, maxSize, maxSize );
		}
		else
		{
			startGrowLerp = true;
		}
	}

	private void BallShrink ()
	{
		if (ballTransform.localScale.magnitude > minSize)
		{
			ballTransform.localScale -= new Vector3( shrinkRate, shrinkRate, shrinkRate );
		}
	}

	private void BallShrinkInstant ()
	{
		initalLerpValue = ballTransform.localScale.x;
		if( (initalLerpValue - shrinkRateInstant) < minSize )
		{
			ballTransform.localScale = new Vector3( minSize, minSize, minSize );
		}
		else
		{
			startShrinkLerp = true;
		}
	}

	private void OnTriggerEnter ( Collider other )
	{
		if(other.tag == "ShrinkItem")
		{
			BallShrinkInstant();
			//Destroy(other.gameObject);
		}
		else if ( other.tag == "GrowItem" )
		{
			BallGrowInstant();
			//Destroy( other.gameObject );
		}
		else if ( other.tag == "YouDiedBox" )
		{
            
            rigidBody.velocity = new Vector3 (0, 0, 0);
            rigidBody.angularVelocity = new Vector3(0, 0, 0);
            ballTransform.localScale = new Vector3(minSize, minSize, minSize);
            rigidBody.position = spawnPos.position;
       
            
        }
	}

	//Jump 
	private void OnCollisionEnter ( Collision collision )
	{
		if(collision.collider.tag == "Ground")
		{
			canJump = true;
		}
	}

	//Forces 
	private void UpdateForceSetting(Vector3 moveDirection)
	{
		if(Vector3.Dot(moveDirection, rigidBody.velocity) <= appliedForceThreshold )
		{
			applyOppisiteForce = true;
		}
		else
		{
			applyOppisiteForce = false;
		}
	}

	// Update is called once per frame
	void Update()
    {

		if ( startGrowLerp )
		{
			if ( timer <= 1f )
			{
				timer += Time.deltaTime;
				float lerpValue = Mathf.Lerp( initalLerpValue, initalLerpValue + growthRateInstant, timer );
				ballTransform.localScale = new Vector3( lerpValue, lerpValue, lerpValue );
			}
			else
			{
				startGrowLerp = false;
				timer = 0f;
			}
		}
		else if ( startShrinkLerp )
		{
			if ( timer <= 1f )
			{
				timer += Time.deltaTime;
				float lerpValue = Mathf.Lerp( initalLerpValue, initalLerpValue - shrinkRateInstant, timer );
				ballTransform.localScale = new Vector3( lerpValue, lerpValue, lerpValue );
			}
			else
			{
				startShrinkLerp = false;
				timer = 0f;
			}
		}

        //Manet input
		float forward = Input.GetAxis( "Vertical" );
		float right = Input.GetAxis( "Horizontal" );
		Vector3 moveDirection = new Vector3( right, 0, forward );
		UpdateForceSetting( moveDirection );

		
		if (moveDirection.magnitude > 0f)
		{
			BallGrow();
		}
		
		float forceToApply = applyOppisiteForce ? appliedForceOppisite : appliedForce;

		//limit speed
		if ( moveDirection.magnitude < maxSpeed )
		{
			rigidBody.AddForce( moveDirection * forceToApply, ForceMode.Acceleration );
		}
			
		if(Input.GetButtonDown("Jump") && canJump )
		{
			canJump = false;
			rigidBody.AddForce( new Vector3(0,1,0) * jumpForce, ForceMode.VelocityChange );
		}

		
            rigidBody.AddForce(new Vector3(0, 0, 1) * forceToApply, ForceMode.Acceleration);
            BallGrow();
		

		 if( Input.GetKey( KeyCode.S))
		{
			rigidBody.AddForce( new Vector3( 0, 0, -1 ) * forceToApply, ForceMode.Acceleration );
			BallGrow();
		}
		else if ( Input.GetKey( KeyCode.A ))
		{
			rigidBody.AddForce( new Vector3( -1, 0, 0 ) * forceToApply, ForceMode.Acceleration );
		}
		else if ( Input.GetKey( KeyCode.D ))
		{
			rigidBody.AddForce( new Vector3( 1, 0, 0 ) * forceToApply, ForceMode.Acceleration );
		}

        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }
    }

}
