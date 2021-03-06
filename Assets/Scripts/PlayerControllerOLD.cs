﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControllerOLD : MonoBehaviour {

	private float walkSpeed = 8f;
	private float jumpForce = 600f;
	private float move;
	private float groundRayRange = 1.1f;

	private bool grounded = true;
	private bool facingRight = true;
	private bool faded = false;

	private Rigidbody rb;

	//PUSH AND PULL
	private RaycastHit hit;
	private float touchRayRange = 1f;
	public float pullForce = 100f;
	private Vector3 D;

	//private float pushForce = 1f;
	//private ForceMode forceMode = ForceMode.VelocityChange;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		Jump ();
		Move ();
		Interact ();
		//TurnAround ();

	}
	void Move(){
		move = CrossPlatformInputManager.GetAxis ("Horizontal");
		if (grounded) {
			rb.velocity = new Vector3 (move * walkSpeed, rb.velocity.y, 0);
		}
	}
	void Jump(){
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hit;
		Debug.DrawRay(transform.position, Vector3.down * groundRayRange);

		if (Physics.Raycast (ray, out hit, groundRayRange)) {
			grounded = true;
			if (Input.GetButtonDown ("Jump") && grounded) {
				rb.velocity = new Vector3 (rb.velocity.x, 0, 0);
				rb.AddForce  (new Vector3 (0, jumpForce, 0));
			} 
		} else {
			grounded = false;
		}
	}
	void Interact(){
		Vector3 rayStartPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1f);
		Ray ray = new Ray(rayStartPos, Vector3.right);
		RaycastHit hit;
		Debug.DrawLine(rayStartPos, rayStartPos + Vector3.right * touchRayRange, Color.red);

		if(Physics.Raycast(ray, out hit, touchRayRange)){
			print (hit.transform.name);

			if (CrossPlatformInputManager.GetButton("Fire1")){
				walkSpeed = 2f;
				rb.constraints = RigidbodyConstraints.FreezeRotation;
				hit.rigidbody.isKinematic = true;
				hit.transform.parent = transform;
				hit.transform.localRotation = Quaternion.identity;
// 	DETERMINING PUSH VS. PULL
//				Vector3 D  = transform.position - hit.transform.position;
//				// If on the left of object and move towards the object
//				if (Input.GetAxis("Horizontal") < 0 && D.x < 0){
//					if (hit.distance > 0.5) {
//						walkSpeed = 2f;
//						//hit.rigidbody.AddForce(D.normalized*pullForce*Time.smoothDeltaTime, forceMode);
//					//hit.transform.position = Vector3.Lerp (hit.transform.position, transform.position, 3f * Time.deltaTime);
//					}
//				}
//				//
//				// If on the right of object and move towards the object	}
//				if (Input.GetAxis("Horizontal") < 0 && D.x > 0){
//					if (hit.distance > 0.5) {
//						hit.rigidbody.AddForce(D.normalized*pullForce*Time.smoothDeltaTime, forceMode);
//					}
//				}
//
//				// If on the right of object and move away from the object
//				if (Input.GetAxis("Horizontal") > 0 && D.x > 0){
//					if (hit.distance > 0.5) {
//						hit.rigidbody.AddForce(D.normalized*pullForce*Time.smoothDeltaTime, forceMode);
//					}
//				}
// 
			} 
//			if (hit.transform.tag == "Gameplay" && CrossPlatformInputManager.GetButtonDown ("Fire3")) {
//				if (!faded) {
//					hit.transform.GetComponent<Rigidbody>().isKinematic = true;
//					hit.transform.GetComponent<Rigidbody> ().useGravity = false;
//					hit.transform.GetComponent<Collider> ().isTrigger = true;
//					hit.transform.GetComponent<Renderer> ().material.color = Color.green;
//					faded = true;
//				} 
//				else if (faded) {
//					hit.transform.GetComponent<Rigidbody>().isKinematic = false;
//					hit.transform.GetComponent<Rigidbody> ().useGravity = true;
//					hit.transform.GetComponent<Collider> ().isTrigger = false;
//					hit.transform.GetComponent<Renderer> ().material.color = Color.red;
//					faded = false;
//				}
//			}
			else if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
				hit.transform.GetComponent<ReParenter> ().ReParent ();
				walkSpeed = 8f;
				rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
			}
		}
	}
	void TurnAround(){
		float rotateRight = transform.rotation.y - 180f;
		Quaternion rightTarget = Quaternion.Euler (0, 0, 0);
		Quaternion leftTarget = Quaternion.Euler(0, rotateRight, 0);
		float smoothing = 10f;

		if (move < 0 && facingRight) {
			transform.rotation = Quaternion.Lerp (transform.rotation, leftTarget, smoothing * Time.deltaTime);
			if (move <= -.98) {
				facingRight = !facingRight;

			}
		} else if (move > 0 && !facingRight) {
			transform.rotation = Quaternion.Lerp (transform.rotation, rightTarget, smoothing * Time.deltaTime);
			if (move >= .98) {
				facingRight = !facingRight;
			}
		}
	}

	void Test(){
		
	}
}


	 

