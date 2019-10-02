using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
	public GameObject proj;
    public GameObject wall;
	float speed = 40;

    public bool fired;
    public int boxes;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetMouseButtonDown(0)){
            if (!fired){
                Vector3 pos = Camera.main.gameObject.transform.position;
                Vector3 rot = Camera.main.gameObject.transform.forward;
                Rigidbody p = Instantiate(proj, pos + 0.5f * rot, Quaternion.identity).GetComponent<Rigidbody>();
                p.velocity = rot * speed;
                fired = true;
            }
    	}
    }
}
