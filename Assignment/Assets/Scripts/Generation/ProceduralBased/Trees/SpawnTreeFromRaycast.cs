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
        if(delay==1){
            Draw();
            delay--;
        }
        else if(delay>1)
        {
            delay--;
        }
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void Draw()
    {   

        RaycastHit hit;
        for(int i=0;i<treenum;i++)
        {
            Vector3 position = new Vector3(Random.Range(-1*(spawnsize), spawnsize), 100, Random.Range(-1*(spawnsize), spawnsize));
            if (Physics.Raycast(position, Vector3.down, out hit))
            {
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
