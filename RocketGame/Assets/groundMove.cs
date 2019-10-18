using UnityEngine;
using System.Collections;

public class groundMove : MonoBehaviour {
	Rigidbody2D player;
	float speed = 0.5f; 
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");

		if (player_go == null) {
			Debug.LogError ("Couldn't find player");
			return;
		}
		player = player_go.GetComponent<Rigidbody2D>();


	}



	void FixedUpdate() {
		float vel = player.velocity.y * 0.9f;

		transform.position = transform.position + Vector3.right * vel * Time.deltaTime;
	}

}
