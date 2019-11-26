using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrinkControlller : MonoBehaviour
{
    GameObject player;
    float timer;
    Animator playerAnim;
    PlayerHealth playerHealth;
    public int healthIncrease = 1; 
    bool activated = false;

    public AudioSource consumedSound;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        timer += Time.time;

    }
    
    //OnTriggerEnter lets you collide without collision because something weird is happening with collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHealth.increaseHealth(healthIncrease);
            consumedSound.Play();
            Destroy(this.gameObject); 
        }
    }

}
