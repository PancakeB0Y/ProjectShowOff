using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraToRotate;

    [SerializeField]
    private float mouseSens = 500f;
    float startSens;

    float xRotation = 0.0f;
    float yRotation = 0.0f;

    bool firstMouse = true;
    float lastMouseX = 0.0f;
    float lastMouseY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        startSens = mouseSens;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens;
        if (mouseX == lastMouseX && mouseY == lastMouseY)
            return;

        if (firstMouse) // initially set to true
        {
            mouseX = 0.0f;
            mouseY = 0.0f;
            firstMouse = false;
        }

        lastMouseX = mouseX;
        lastMouseY = mouseY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10f, 60f);

        cameraToRotate.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void ToggleCameraMovement()
    {
        if (mouseSens != 0f) {
            mouseSens = 0;
        }
        else
        {
            mouseSens = startSens;
        }
    }

    public void EnableCameraMovement()
    {
        mouseSens = startSens;
    }

    public void DisableCameraMovement()
    {
        mouseSens = 0;
    }
}
