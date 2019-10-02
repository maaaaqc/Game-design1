using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freemove : MonoBehaviour
{
	public float speed = 3;
	float gravity = 6f;

	Vector3 moveDir = Vector3.zero;
	CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (controller.isGrounded){
    		if (Input.GetKey(KeyCode.Space)){
                moveDir = new Vector3(0,2,0);
                moveDir *= 3;
            }
            if (Input.GetKeyUp(KeyCode.Space)){
                moveDir = new Vector3(0,0,0);
            }
        	float vert = Input.GetAxis("Vertical") * speed;
        	float hori = Input.GetAxis("Horizontal") * speed;
            Vector3 forward = transform.forward * vert;
            Vector3 right = transform.right * hori;

	        controller.SimpleMove(forward + right);
	    }
	    moveDir.y -= gravity * Time.deltaTime;
	    controller.Move(moveDir * Time.deltaTime);
    }
}
