using UnityEngine;
using System.Collections;

public class cameraTracksPlayer : MonoBehaviour {

	Transform player;

	float offsetY;

	// Use this for initialization
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");

		if (player_go == null) {
			Debug.LogError ("Couldn't find player");
			return;
		}
		player = player_go.transform;

		offsetY = transform.position.y - player.position.y;

	}

	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			Vector3 pos = transform.position;
			pos.y = player.position.y + offsetY;
			transform.position = pos;
		}

	}
}
