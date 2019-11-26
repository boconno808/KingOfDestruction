using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int TIME_VALUE= 60; // you can modify the time limit with this variable

    public static int timeLeft = TIME_VALUE; //Seconds Overall
    public Text countdown; //UI Text Object
    public GameObject GameOverText;

    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        countdown.text = ("Time : " + timeLeft); //Showing the Score on the Canvas
    }
  
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (timeLeft > 0)
            {
                timeLeft--;
            }else
            {
                GameEnd(); 
            }
        }

    }

    void GameEnd()
    {
        GameOverText.SetActive(true); 
    }

}