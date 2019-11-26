using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (playerHealth.currentHealth <= 0 || Timer.timeLeft<=0)
        {
            StartCoroutine(waitForDeath());

        }
    }

    public void ReplayGame()
    {
        //Load the game over scene
        SceneManager.LoadScene(2);
        Timer.timeLeft = Timer.TIME_VALUE;
    }

    IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(3);
        ReplayGame();

    }
}
