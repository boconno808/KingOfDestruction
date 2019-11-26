using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Rony still moves while the punching animation plays and it looks weird lol
// TODO: Rony goes through walls and also defies gravity :(
// TODO: Rony can still rotate in the victory state which is also weird

public class RonyControls : MonoBehaviour
{
    static Animator anim;
    public float Speed = 5f;
    public float initalSpeed = 5f; 
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    //Count related field variables
    public int score;
    private bool dontMove = false;
    public Text scoreText;
    public Text winText;
    private bool missionComplete = false;
    public bool isAttacking = false;
    public int damage; 
    // Audio
    public AudioSource attackSound;

    public AudioSource meowSound;
    // collide with cat1
    public bool invertKeys = false;
    // collide with rabbit
    //public bool freezeKeys = false;

    GameObject RonysScooter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        score = 0;
        SetScoreText();
        winText.text = "";
        //start with scooter off
        RonysScooter = GameObject.FindGameObjectWithTag("RonysScooter");
        RonysScooter.SetActive(false);

    }
    public void freezePlayerMovement(float coolTime)
    {
        dontMove = true;
        StartCoroutine(waitForFreezeCoolTime(coolTime));
    }

    IEnumerator waitForFreezeCoolTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        dontMove = false;
      
    }

    public void invertDirectionKeys(float coolTime)
    {
        invertKeys = true;
        meowSound.Play();
        StartCoroutine(waitForDirectionCoolTime(coolTime));
    }
    IEnumerator waitForDirectionCoolTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        this.invertKeys = false;
    }

    public void setDefaultSpeed(float coolTime)
    {
        StartCoroutine(waitForCoolTime(coolTime)); 
     
    }
    
    public void increaseSpeed(float S, float coolTime)
    {
        this.Speed += S;
        anim.SetBool("isScooter", true);
        RonysScooter.SetActive(true);
        setDefaultSpeed(coolTime);

    }

    IEnumerator waitForCoolTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        this.Speed = initalSpeed;
        anim.SetBool("isScooter", false);
        RonysScooter.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //dont move if rony is dead
        if (anim.GetBool("isDead")){
            dontMove = true;
        }
        // Rony moves and rotates according to arrow keys
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        if(Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0 ||
           Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") < 0){
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }
            _inputs = Vector3.zero;
            _inputs.x = Input.GetAxis("Horizontal");
            _inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero && !dontMove)
        {
            if(invertKeys)
            {
                _inputs.x *= -1;
                _inputs.z *= -1;
            }

            transform.forward = _inputs;
        }

        //Rony will RightHook on space
        if (Input.GetButtonDown("RightHook"))
        {
            isAttacking = true;
            attackSound.Play();
            anim.SetTrigger("isRightHook");
            StartCoroutine(Attack());
        }

     
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        float attackTime = 1f;
       yield return new WaitForSeconds(attackTime);
        isAttacking = false;
        
    }
    //Move based on physics so fixed update is better
    void FixedUpdate()
    {
        if (!dontMove){
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        }
    }

    public void CompleteMission()
    {
        missionComplete = true;
        winText.text = "Stage Clear!";
        anim.SetTrigger("isVictory");
        dontMove = true;
    }

    public void updateScore(int score)
    {
        this.score += score;
        SetScoreText();
    }

    //Updates the score
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
