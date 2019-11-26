using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    GameObject player;
    RonyControls ronyControls;

    float timer;
    bool playerInRange;
    public float timeBetweenAttacks = 1.0f;
    public float coolTimeInSec = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ronyControls = player.GetComponent<RonyControls>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.time;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            freezePlayerMove();
        }
    }
    void freezePlayerMove()
    {
        timer = 0.0f;
        ronyControls.freezePlayerMovement(coolTimeInSec);
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
}
