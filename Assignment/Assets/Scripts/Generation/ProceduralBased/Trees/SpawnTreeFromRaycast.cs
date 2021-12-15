using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreeFromRaycast : MonoBehaviour
{   
    public float spawnsize=50f;
    public int treenum;
    public GameObject Tree;

    public int delay=20;
    
    // Start is called before the first frame update
    void Start()
    {  
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //if the delay has been reached draw the tree's
        if(delay==1){
            Draw();
            //remove the delay so it doesnt keep drawing
            delay--;
        }//count down from the requested delay till it draws.
        else if(delay>1)
        {
            delay--;
        }
    }
    void Draw()
    {   
        //create the raycast hit
        RaycastHit hit;
        //create a tree until you reach the number of tree's requested
        for(int i=0;i<treenum;i++)
        {
            //get a random position in the chosen range and send a ray straight down from heaven to hit the ground.
            Vector3 position = new Vector3(Random.Range(-1*(spawnsize), spawnsize), 100, Random.Range(-1*(spawnsize), spawnsize));
            //if the ray hits the ground
            if (Physics.Raycast(position, Vector3.down, out hit))
            {   
                //create the tree object.
                GameObject _instanceSampleTree = (GameObject)Instantiate (Tree);
                //move the position of the tree
                _instanceSampleTree.transform.position = hit.point;
                //set the tree in relation to the parent
                _instanceSampleTree.transform.parent = this.transform;
                // name the tree, using the vector as a unique identifier as its good practice not to have them all the same name.
                _instanceSampleTree.name = "SampleTree"+i;
            }
        }
    }
}
