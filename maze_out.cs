using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze_out : MonoBehaviour
{
	public GameObject tree;
    public GameObject entrance;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 11; i++){
        	if (i == 1){
        		for (int j = 1; j < 11; j++){
                    if (j == 9){
                        Instantiate(entrance, new Vector3(j, 0, 20+i), Quaternion.identity);
                    }
                    else{
            			Instantiate(tree, new Vector3(j, 0, 20+i), Quaternion.identity);
                    }
        		}
        	}
            else {
                Instantiate(tree, new Vector3(1, 0, 20+i), Quaternion.identity);
        	    Instantiate(tree, new Vector3(10, 0, 20+i), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
