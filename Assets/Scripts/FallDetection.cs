using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour {
	Vector3 checkpoint;

	void Start (){
		CheckPointLoader.checkpoint = new Vector3(0, 0, 0);

	}

	void Update (){
		if (transform.position.y < -10) {
			transform.position = CheckPointLoader.checkpoint;
		}
	}
}