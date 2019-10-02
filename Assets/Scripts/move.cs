using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    float gravity = 8;

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
            if (Input.GetKey(KeyCode.W)){
                moveDir = new Vector3(0,0,1);
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.W)){
                moveDir = new Vector3(0,0,0);
            }
            if (Input.GetKey(KeyCode.S)){
                moveDir = new Vector3(0,0,-1);
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.S)){
                moveDir = new Vector3(0,0,0);
            }
            if (Input.GetKey(KeyCode.A)){
                moveDir = new Vector3(-1,0,0);
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.A)){
                moveDir = new Vector3(0,0,0);
            }
            if (Input.GetKey(KeyCode.D)){
                moveDir = new Vector3(1,0,0);
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.D)){
                moveDir = new Vector3(0,0,0);
            }
            if (Input.GetKey(KeyCode.Space)){
                moveDir = new Vector3(0,2,0);
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.D)){
                moveDir = new Vector3(0,0,0);
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}