using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingController : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 1;
    GameObject player;
    bool damaged, playerInRange; 
    float timer;
    Animator anim;
    int startingHealth = 5;
    public int currentHealth;
    public Slider healthSlider;
    public AudioSource attackedAudio;
    public AudioSource destroyedAudio; 
    RonyControls playerControl;
    public int score;
    CameraController cameraControl;
    public bool isMissionBuilding = false;
    public bool isAttacked = false;
    public GameObject breakingEffect;
    public ParticleSystem particleSystem;
    Vector3 position; 
    // Start is called before the first frame update

    void Awake()
    {
        position = GetComponent<Renderer>().bounds.center;
        breakingEffect = GameObject.FindGameObjectWithTag("BreakEffect"); 
        particleSystem = breakingEffect.GetComponent<ParticleSystem>(); 
        cameraControl = Camera.main.gameObject.GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<RonyControls>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && playerControl.isAttacking == true)
        {
            healthSlider.value = currentHealth;
            TakeDamage(playerControl.damage); 
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
    

    public void TakeDamage(int amount)
    {
        breakingEffect.transform.position = new Vector3(position.x, position.y * 2, position.z);
        currentHealth -= amount;
        cameraControl.shake();
        healthSlider.value = currentHealth;
        timer = 0; 
        if (currentHealth <= 0)
        {
            Destruction();
        }else {
            particleSystem.Play();  
            attackedAudio.Play();
        }
  
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && playerControl.isAttacking)
        {
            TakeDamage(playerControl.damage); 
        }
    }

    void Destruction()
    {
        destroyedAudio.Play();
        Destroy(gameObject);
        playerControl.updateScore(score);
        if (isMissionBuilding)
        {
            playerControl.CompleteMission();
        }
    }



}
