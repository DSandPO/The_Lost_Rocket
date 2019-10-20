
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


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





    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Press position + " + eventData.pressPosition);
        Debug.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        Debug.Log("norm + " + dragVectorDirection);
        GetDragDirection(dragVectorDirection);
    }


}
