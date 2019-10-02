using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    float gravity = 8;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.transform.position.y >= 20){
            Destroy(gameObject);
            GameObject player = GameObject.Find("Character");
            player.GetComponent<fire>().fired = false;
        }
        if (timer >= 3){
            Destroy(gameObject);
            GameObject player = GameObject.Find("Character");
            player.GetComponent<fire>().fired = false;
        }

    }

    void OnCollisionEnter(Collision otherObj) {
    	if (otherObj.gameObject.tag == "Wall"){
    		Destroy(gameObject);
            GameObject player = GameObject.Find("Character");
            player.GetComponent<fire>().fired = false;
    	}
        else{
            Vector3 moveDir = new Vector3(0,0,0);
            moveDir.y -= gravity * Time.deltaTime;
            gameObject.transform.position += (moveDir * Time.deltaTime);
        }
    }
}
