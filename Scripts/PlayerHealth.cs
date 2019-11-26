using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator anim;
    int startingHealth = 5;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageHealthImage;
    public AudioClip hurtClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool isDead;
    bool damaged;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageHealthImage.color = flashColour;
        }
        else
        {
            damageHealthImage.color = Color.Lerp(damageHealthImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false; 
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead)
        {
            Death(); 
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetBool("isDead", true);

        // playerMovement.enabled = false; 
    }

    public void increaseHealth(int h)
    {
        if (this.currentHealth < startingHealth){
            this.currentHealth += h;
            healthSlider.value = currentHealth;
        } 
    }
}
