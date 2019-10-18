using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;
	birdMove bird;
	static Score instance;

	static public void AddPoint(){
		if (instance.bird.dead) {
			return;
		}

		score++;

		if (score > highScore) {
			highScore = score;
		}
	}



	void Start(){
		instance = this;
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");
		if (player_go == null) {
			Debug.LogError ("Couldnt find player");
		}
		bird = player_go.GetComponent<birdMove> ();
		score = 0;
		PlayerPrefs.GetInt ("highScore", 0);
	}

	void onDestroy() {
		instance = null;
		PlayerPrefs.SetInt ("highScore", highScore);
	}
	// Update is called once per frame
	void Update () {
		GetComponent<GUIText>().text = "Score: " + score + "\nBest: " + highScore;
	}
}
