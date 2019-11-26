using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat1Controller : MonoBehaviour
{
    public float timeBetweenAttacks = 1.0f;
    public float coolTimeInSec = 5;
    bool playerInRange;
    float timer;
    
    GameObject player;
    RonyControls ronyControls;

    Vector3 origin;
    Vector3 range;

    float randomX;  //randomly go this X direction
    float randomZ;
    bool move;
    public float duration=10;    //the max time of a walking session (set to ten)
    float elapsedTime = 0f; //time since started walk
    float wait = 0f; //wait this much time
    float waitTime = 0f; //waited this much time

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ronyControls = player.GetComponent<RonyControls>();

    }

    void Update()
    {
       
        timer += Time.time;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            invertDirection();
        }
      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    void invertDirection()
    {
        timer = 0.0f;
        ronyControls.invertDirectionKeys(coolTimeInSec);
    }

    void randomMovement()
    {
        if ((elapsedTime < duration) && move)
        {
            //if its moving and didn't move too much
            transform.Translate(new Vector3(randomX, 0, randomZ) * Time.deltaTime);
            elapsedTime += Time.deltaTime;

        }
        else
        {
            //do not move and start waiting for random time
            move = false;
            wait = Random.Range(5, 10);
            waitTime = 0f;
        }

        if (waitTime < wait && !move)
        {
            //you are waiting
            waitTime += Time.deltaTime;


        }
        else if (!move)
        {
            move = true;
            elapsedTime = 0f;
            randomX = Random.Range(-range.x, range.x);
            randomZ = Random.Range(-range.z, range.z);
        }
    }
 
}