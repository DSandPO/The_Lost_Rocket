using UnityEngine;
using System.Collections;

public class scorePoint : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Player") {
			Score.AddPoint();
			//gameObject.SetActive (false);
		}
	}
}
