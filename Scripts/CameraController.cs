using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public bool shouldRotate = true;

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    float wantedRotationAngle;
    float wantedHeight;
    float currentRotationAngle;
    float currentHeight;
    Quaternion currentRotation;
    public bool shouldShake = false; 
    public float power = 0.1f;
    public float duration = 0.2f;
    public Transform camera;
    public float slowDownAmount = 1.0f;
    float initialDuration = 0.2f;

    void LateUpdate()
    {
        if (target)
        {
            // Calculate the current rotation angles
            wantedRotationAngle = target.eulerAngles.y;
            wantedHeight = target.position.y + height;
            currentRotationAngle = transform.eulerAngles.y;
            currentHeight = transform.position.y;
            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // Convert the angle into a rotation
            currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            // Always look at the target
            if (shouldRotate)
                transform.LookAt(target);
        }

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

    public void shake()
    {
        shouldShake = true;
    }
}