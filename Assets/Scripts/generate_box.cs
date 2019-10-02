using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_box : MonoBehaviour
{
	public GameObject proj;
    // Start is called before the first frame update
    void Start()
    {
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate(){
        Vector3[] v = new Vector3[3];
        bool inside = false;
        for (int i = 0; i < 3; i++){
            v[i] = new Vector3(3+Random.value*14,3+Random.value*5,3+Random.value*24);
            if (v[i].x < 11 && v[i].z > 19){
                inside = true;
            }
            while (inside){
                v[i] = new Vector3(3+Random.value*14,3+Random.value*5,3+Random.value*24);
                inside = false;
                if (v[i].x < 11 && v[i].z > 19){
                    inside = true;
                }
            }
        }
        for (int k = 0; k < 3; k++){
            Instantiate(proj, v[k], Quaternion.identity);
        }
    }
}
