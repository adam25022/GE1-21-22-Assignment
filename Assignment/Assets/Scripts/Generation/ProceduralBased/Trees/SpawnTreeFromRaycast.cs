using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreeFromRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Ray myRay; // create ray
    RaycastHit hit;//creat the raycast hit

    public GameObject Tree;

    // Update is called once per frame
    void Update()
    {
        myRay=Camera.main.ScreenPointToRay(Input.mousePosition);//comes from the camera to mouse.
        if(Physics.Raycast(myRay, out hit))
        {
            if(Input.GetMouseButtonDown(0))//if you click.
            {
                Instantiate(Tree, hit.point, Quaternion.identity);//this creates a tree on click.
            }
        }
    }
}
