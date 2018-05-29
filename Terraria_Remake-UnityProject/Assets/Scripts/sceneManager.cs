using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {
	public float moveSpeed = 10f;

	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Input.GetAxis("Horizontal")* moveSpeed*Time.fixedDeltaTime,
							Input.GetAxis("Vertical")* moveSpeed*Time.fixedDeltaTime,
							0);

		
	}
}
