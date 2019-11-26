using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    GameObject playerObj;
    Vector3 cameraOffset;
    private Camera thisCamera;
    //public Transform player;
    public float sensitivity = 0.5f;
    // Private because we can set this value here in the script 
    private Vector3 offset;
    public float MinDist, CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
    public bool shouldShake = false;
    public float speedH = 0.5f;
    public float speedV = 0.5f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float scroll;
    private float speed = 20f;

    public float power = 0.1f;
    public float duration = 0.2f;
    public Transform camera;
    public float slowDownAmount = 1.0f;
    float initialDuration = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        cameraOffset = (transform.position - playerObj.transform.position);
        initialDuration = duration;
    }

    void Update()
    {
       
        scroll = Input.GetAxis("Mouse ScrollWheel")*speed;
        thisCamera.fieldOfView -= scroll;
        transform.position = playerObj.transform.position + cameraOffset;
       
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch + 35, yaw, 0.0f);

        if (shouldShake)
        {
            if (duration > 0)
            {
                float x = transform.position.x + Random.insideUnitSphere.x * power;
                float y = transform.position.y + Random.insideUnitSphere.y * power;
                float z = transform.position.z + Random.insideUnitSphere.z * power;
                transform.position = new Vector3(x, y, z);
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
            }
        }

    }

    //void Update()
    //{

    //    AngleH += Input.GetAxis("Mouse X");
    //    AngleV -= Input.GetAxis("Mouse Y");
    //    CurrentDist += Input.GetAxis("Mouse ScrollWheel");
    //}


    // Update is called once per frame, LateUpdate is good for follow cameras, procedural aniamtion, and last known states 
    // LateUpdate is guarenteed to run after all items have been processed in update
    //void LateUpdate()
    //{

    //    Vector3 tmp;
    //    tmp.x = (Mathf.Cos(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + playerObj.transform.position.x);
    //    tmp.z = (Mathf.Sin(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + playerObj.transform.position.z);

    //    tmp.y = -Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + playerObj.transform.position.y;
    //    if (tmp.y < 5)
    //    {
    //        tmp.y = 5;
    //    }
    //    transform.position = Vector3.Slerp(transform.position, tmp, sensitivity * Time.deltaTime);
    //    cameraOffset = transform.position - playerObj.transform.position;
    //    transform.position = playerObj.transform.position + cameraOffset;
    //    transform.LookAt(playerObj.transform);
    //}


    public void shake()
    {
        shouldShake = true;
    }
}