using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{

    public GameObject temp;

    public Vector3 laneOne;
    public GameObject player;
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
    public bool newRight = false;
    public bool newLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = player.transform.position;
        rightPos = player.transform.position + Vector3.right * distance;
        leftPos = player.transform.position + Vector3.left * distance;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.A))
        {
            keyHit = true;
            dLeft = true;
            if (startPos.x < 0)
            {
                Instantiate(temp, new Vector3((distance * 2), startPos.y, 0), Quaternion.identity);
                newRight = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            keyHit = true;
            dRight = true;
            if (startPos.x > 0)
            {
                Instantiate(temp, new Vector3((distance * 2) * -1, startPos.y, 0), Quaternion.identity);
                newLeft = true;
            }

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
            player.transform.position = Vector3.Lerp(startPos, endPos, Perc);

            

            if(player.transform.position == endPos)
            {

                if (newLeft == true)
                {
                    player = temp;
                    Destroy(temp);
                }
                if (newRight == true)
                {
                    player = temp;
                    Destroy(temp);
                }
                keyHit = false;
                dRight = false;
                dLeft = false;
                startPos = endPos;
                rightPos = player.transform.position + Vector3.right * distance;
                leftPos = player.transform.position + Vector3.left * distance;
                currentLerpTime = 0;
                Debug.Log("hi");

            }
            
        }
    }
}
