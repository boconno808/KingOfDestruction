using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    GameObject playerObj;
    Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        cameraOffset = transform.position - playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObj.transform.position + cameraOffset;
    }
}
