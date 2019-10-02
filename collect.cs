using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
	public int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision otherObj) {
    	if (otherObj.gameObject.tag == "Key"){
        	Destroy(otherObj.gameObject);
        	count++;
            if (count >= 1){
                GameObject des = GameObject.FindWithTag("Door");
                GameObject.Destroy(des);
            }
        }
    }

    void OnGUI() {
        GUI.Button(new Rect (10,10,150,20), "Key obtained: " + count.ToString());
    }
}
