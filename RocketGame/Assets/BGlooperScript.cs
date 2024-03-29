﻿using UnityEngine;
using System.Collections;

public class BGlooperScript : MonoBehaviour {

	int numBGPanels = 6;

	float pipeMax = 1.08f;
	float pipeMin = 0.04f;

	void Start(){
		GameObject[] pipes = GameObject.FindGameObjectsWithTag ("Pipe");

		foreach (GameObject pipe in pipes) {
			Vector3 pos = pipe.transform.position;
			pos.y = Random.Range (pipeMin, pipeMax);
			pipe.transform.position = pos;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("triggered: " + collider.name);

		float widthOfBGObject = ((BoxCollider2D)collider).size.x;

		Vector3 pos = collider.transform.position;

		pos.x += widthOfBGObject * numBGPanels;

		if(collider.tag == "Pipe") {
			pos.y = Random.Range (pipeMin, pipeMax);
		}

		collider.transform.position = pos;

	}
}
