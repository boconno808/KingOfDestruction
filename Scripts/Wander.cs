using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class Wander : MonoBehaviour
{
    public Animator anim;
    public float speed;
    public Transform[] moveSpots;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;
    private float turnSpeed;

    private void Start()
    {
        randomSpot = Random.Range(0,moveSpots.Length);
        waitTime = startWaitTime;
        turnSpeed = 5;
    }

    private void Update()
    {
        Vector3 targetDir = moveSpots[randomSpot].position - transform.position;

        // The step size is equal to speed times frame time.
        float step = turnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.position = Vector3.MoveTowards(transform.position,
                                                 moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < .2f) {
            if(waitTime <= 0){
                anim.SetBool("isIdle", false);
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
                anim.SetBool("isIdle", true);
            }
        }
    }
}
