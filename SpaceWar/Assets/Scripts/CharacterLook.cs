using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float xPosition = Input.GetAxis("Mouse X") *mouseSensitivity*Time.deltaTime;
        //float yPosition = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float xPosition = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        float yPosition = Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;

        xRotation -= yPosition;
        xRotation = Mathf.Clamp(xRotation, -60f, 50f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * xPosition);
    }
}
