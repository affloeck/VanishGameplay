using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReParenter : MonoBehaviour {

	public GameObject spawner;


	public void ReParent(){
		transform.parent = spawner.transform;
	}
}
