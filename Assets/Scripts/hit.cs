using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public GameObject box;   
	public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision otherObj) {
    	Vector3 pos = gameObject.transform.position;
    	Quaternion rot = gameObject.transform.rotation;
        Destroy(gameObject);
        Instantiate(box, pos, rot);
        GameObject player = GameObject.Find("Character");
        player.GetComponent<fire>().boxes += 1;
        if (player.GetComponent<fire>().boxes >= 3){
            Instantiate(key, pos, rot);
        }
    }
}
