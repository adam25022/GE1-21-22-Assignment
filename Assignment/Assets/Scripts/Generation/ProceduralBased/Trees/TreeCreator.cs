using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreator : MonoBehaviour {
	// get passed in the object tree
	public GameObject Tree;
	//variable for how far apart the tree's are meant to be.
	public float DistanceBetweenTrees = 1;
	//vector 2 region for how big the area that is going to have tree's generated on it is.
	public Vector2 regionSize = Vector2.one;
	//this is how packed the trees have to be in the region but still within the distance
	public int rejectionSamples = 30;

	// this value isnt actually used anymore, i use it to reset the drawn
	public float clear =1;
	// this is the points that the trees are spawned at.
	List<Vector2> points;
	// this is the vector i am using to swap the vector 2 into a vector3.
	Vector3 treevector;
	void OnValidate() {
		//delete the previous trees
		delete();
		//get the poissondisc points
		points = PoissonDiscSampling.GeneratePoints(DistanceBetweenTrees, regionSize, rejectionSamples);
		//draw the trees into the world.
		Draw();
	}
	void onDrawGizmo(){
		Gizmos.DrawWireCube(regionSize/2,regionSize);
	}
	void Draw() {
		//if there is points in the poissondisc vector2. draw.
		if (points != null) {
			// for every point, create a game object
			foreach (Vector2 point in points) {
				//change the vector2 into the vector3
				treevector.Set(point.x, 0, point.y);
				//create the tree
				GameObject _instanceSampleTree = (GameObject)Instantiate (Tree);
				//move the position of the tree
				_instanceSampleTree.transform.position = treevector;
				//set the tree in relation to the parent
				_instanceSampleTree.transform.parent = this.transform;
				// name the tree, using the vector as a unique identifier as its good practice not to have them all the same name.
				_instanceSampleTree.name = "SampleTree"+treevector;
			}
		}
	}
	// you cant delete the children without UnityEditor.EditorApplication.delayCall+=()=> as unity gets mad at you for deleting it in the editor without.
	void delete(){
		UnityEditor.EditorApplication.delayCall+=()=>
     	{	
			//for every child of the parent object.
          	foreach (Transform child in transform)
			{
				DestroyImmediate(child.gameObject);
			}
     	};
	}
}
