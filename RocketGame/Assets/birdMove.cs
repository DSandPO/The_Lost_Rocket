
using UnityEngine;
using System.Collections;

Vector2 firstPressPos;
Vector2 secondPressPos;
Vector2 currentSwipe;

public class birdMove : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	//public Vector3 gravity;
	//public Vector3 flapVelocity;
	//public float maxSpeed = 5f;
	public float forwardSpeed = 1f;
	public float flapSpeed = 10f;

	bool didFlap = false;

	Animator animator;

	public bool dead = false;

	float deathCooldown;

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();

		if (animator == null) {
			Debug.LogError ("didnt find animator");
		}
	}

	// graphic input update
	void Update() {
		if (dead) {
			deathCooldown -= Time.deltaTime;
			if (deathCooldown <= 0) {
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
					Application.LoadLevel (Application.loadedLevel);
				}
			}
		}
		else {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
				didFlap = true;
			}
		}
	}
	
	// Update is called once per frame. physics update here
	void FixedUpdate () {

		if (dead) 
			return;
		

		GetComponent<Rigidbody2D>().AddForce (Vector2.up * forwardSpeed);
		if (didFlap) {
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * flapSpeed);
			animator.SetTrigger ("DoFlap");
			didFlap = false;
		}

		if (GetComponent<Rigidbody2D>().velocity.y > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		} else {
			float angle = Mathf.Lerp (0, 0, -GetComponent<Rigidbody2D>().velocity.y / 2f);
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		if (godMode)
			return;
		animator.SetTrigger ("Death");
		dead = true;
		deathCooldown = 0.5f;
		//Debug.LogError ("collision detected");
	}

    

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f )
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f )
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0  currentSwipe.y > -0.5f  currentSwipe.y < 0.5f )
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0  currentSwipe.y > -0.5f  currentSwipe.y < 0.5f )
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }
}
