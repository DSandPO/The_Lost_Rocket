using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    public Vector3 laneOne;
    public GameObject Player;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 rightPos;
    public Vector3 leftPos;
    public float distance = 3f;
    public float lerpTime = 2f;
    public float currentLerpTime = 0;
    public bool keyHit = false;
    public bool dRight = false;
    public bool dLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Player.transform.position;
        rightPos = Player.transform.position + Vector3.right * distance;
        leftPos = Player.transform.position + Vector3.left * distance;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.A))
        {
            keyHit = true;
            dLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            keyHit = true;
            dRight = true;
        }

        if (keyHit == true)
        {
            if(dLeft == true)
            {
                endPos = leftPos;
            }
            if (dRight == true)
            {
                endPos = rightPos;
            }
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;
            Player.transform.position = Vector3.Lerp(startPos, endPos, Perc);
            if(Player.transform.position == endPos)
            {
                keyHit = false;
                dRight = false;
                dLeft = false;
                startPos = endPos;
                rightPos = Player.transform.position + Vector3.right * distance;
                leftPos = Player.transform.position + Vector3.left * distance;
                currentLerpTime = 0;
                Debug.Log("hi");

            }
            
        }
    }
}
