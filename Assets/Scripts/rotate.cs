using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
	float speedH = 3.0f;
	float speedV = 2.0f;

	float yaw = 0.0f;
	float pitch = 0.0f;

	GameObject character;

    // Start is called before the first frame update
    void Start()
    {
    	character = this.transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        character.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKeyDown("1")){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown("2")){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}