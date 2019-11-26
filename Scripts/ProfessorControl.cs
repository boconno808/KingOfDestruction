using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorControl : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 1;
    GameObject player;
    bool playerInRange;
    float timer;
    Animator playerAnim;
    PlayerHealth playerHealth; 

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAnim = player.GetComponent<Animator>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
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

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack(); 
        }
        if(playerHealth.currentHealth <= 0)
        {
            playerAnim.SetTrigger("isDead"); 
        }
    }

    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
            playerAnim.SetTrigger("isGettingDamage");
        }
    }
}
