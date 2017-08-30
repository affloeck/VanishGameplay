using UnityEngine;
using System.Collections;
using Prime31;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 6f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	//interaction
	private RaycastHit2D _raycastHit;
	private float touchRayRange = 1f;
	public float pullForce = 100f;
	private bool faded = false;
	public LayerMask playerMask;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	void Awake()
	{

		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

	}

	void Start(){

	}



	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update (){

		Move ();
		Interact ();
	}

	void Move(){
		float move = CrossPlatformInputManager.GetAxis ("Horizontal");
		if (_controller.isGrounded){
			_velocity.y = 0;
		}
		if ( move > 0) {
			normalizedHorizontalSpeed = 1;
			if (transform.localScale.x < 0f) {
				//transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			}
			if (_controller.isGrounded) {
				_animator.Play (Animator.StringToHash ("Run"));
			}
		} else if (move < 0) {
			normalizedHorizontalSpeed = -1;
			if (transform.localScale.x > 0f) {
			}	//transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if (_controller.isGrounded) {
				_animator.Play (Animator.StringToHash ("Run"));
			}

		} else {
			normalizedHorizontalSpeed = 0;

			if (_controller.isGrounded) {
				_animator.Play (Animator.StringToHash ("Idle"));
			}
		}


		// we can only jump whilst grounded

		if( _controller.isGrounded && CrossPlatformInputManager.GetButton("Jump") )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			_animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( _controller.isGrounded && CrossPlatformInputManager.GetButton("Vertical") )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}



		


	void Interact(){
		Vector2 rayStartPos = new Vector2 (transform.position.x - 1f, transform.position.y);
		Ray ray = new Ray(rayStartPos, Vector3.right);
		_raycastHit = Physics2D.Raycast (rayStartPos, Vector2.right, 2f, ~playerMask); 
		Debug.DrawLine(rayStartPos, rayStartPos + Vector2.right * 2f, Color.red);
	
	//	if (_controller._raycastHitGameplay.collider.tag == "Gameplay") {
		if (_raycastHit) {
			print (_raycastHit.collider.name);
			Vector2 charObjDist = transform.position - _raycastHit.transform.position;
			if (CrossPlatformInputManager.GetButton ("Fire1") && _raycastHit.collider.tag == "Gameplay") {
					runSpeed = 2f;
					//				rb.constraints = RigidbodyConstraints.FreezeRotation;
					//				gameplayObjectHit.rigidbody.isKinematic = true;
				_raycastHit.transform.parent = transform;
				_raycastHit.transform.localRotation = Quaternion.identity;
			}
	
		

			
				if (CrossPlatformInputManager.GetButtonDown ("Fire3") && !faded) {
					print ("making solid object faded");
					//_controller._raycastHitGameplay.transform.GetComponent<Rigidbody2D>().isKinematic = true;
				if (_raycastHit.transform.GetComponent<Rigidbody2D> ()) {
					_raycastHit.transform.GetComponent<Rigidbody2D> ().gravityScale = 0f;
				}
				_raycastHit.transform.GetComponent<Collider2D> ().enabled = false;
				_raycastHit.transform.GetComponent<Renderer> ().material.color = Color.green;
					faded = true;
				}
				else if (CrossPlatformInputManager.GetButtonDown ("Fire3") && faded) {
						print ("making faded object solid");
							//gameplayObjectHit.transform.GetComponent<Rigidbody2D>().isKinematic = false;
				if (_raycastHit.transform.GetComponent<Rigidbody2D> ()) {
					_raycastHit.transform.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
				_raycastHit.transform.GetComponent<Collider2D> ().enabled = true;
				_raycastHit.transform.GetComponent<Renderer> ().material.color = Color.red;
							faded = false;

				}
			else if (!CrossPlatformInputManager.GetButton ("Fire1")){
					print ("fire up");
				_raycastHit.transform.parent = null;
					//_controller._raycastHitGameplay.transform.GetComponent<ReParenter> ().ReParent ();

					runSpeed = 8f;
					//				rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
				}
				}

		
			
		}
}





//
//		if (_controller.collisionState.right == true) {
//			print ("moving right");
//				if (_controller._raycastHitGameplay.collider.tag == "Gameplay" && CrossPlatformInputManager.GetButton("Fire1")){
//				runSpeed = 2f;
//				print ("hit");
////				rb.constraints = RigidbodyConstraints.FreezeRotation;
////				gameplayObjectHit.rigidbody.isKinematic = true;
//				_controller._raycastHitGameplay.transform.parent = transform;
//				_controller._raycastHitGameplay.transform.localRotation = Quaternion.identity;
//			
//			} 
//			if (_controller._raycastHitGameplay.collider.tag == "Gameplay" && CrossPlatformInputManager.GetButtonDown ("Fire3")) {
//				if (!faded) {
//					_controller._raycastHitGameplay.transform.GetComponent<Rigidbody2D>().simulated = false;
//					_controller._raycastHitGameplay.transform.GetComponent<Rigidbody2D> ().simulated = false;
//					_controller._raycastHitGameplay.transform.GetComponent<Collider2D> ().isTrigger = true;
//					_controller._raycastHitGameplay.transform.GetComponent<Renderer> ().material.color = Color.green;
//					faded = true;
//				} 
//				else if (faded) {
//					//gameplayObjectHit.transform.GetComponent<Rigidbody2D>().isKinematic = false;
//					_controller._raycastHitGameplay.transform.GetComponent<Rigidbody2D> ().simulated = true;
//					_controller._raycastHitGameplay.transform.GetComponent<Collider2D> ().isTrigger = false;
//					_controller._raycastHitGameplay.transform.GetComponent<Renderer> ().material.color = Color.red;
//					faded = false;
//				}
//			}
//			else if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
//				_controller._raycastHitGameplay.transform.GetComponent<ReParenter> ().ReParent ();
//				//_controller._raycastHitGameplay.transform.parent = null;
//				runSpeed = 8f;
////				rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
//			}
//		}
//	}
//}
//
//
//
