using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	
	public GameObject Tree;
	public float radius = 1;
	public Vector3 regionSize = Vector3.one;
	public int rejectionSamples = 30;
	public float displayRadius =1;

	List<Vector3> points;

	void OnValidate() {
		delete();
		points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
		Draw();
	}
	void onDrawGizmo(){
		Gizmos.DrawWireCube(regionSize/2,regionSize);
	}
	void Draw() {
		
		if (points != null) {
			foreach (Vector3 point in points) {
				GameObject _instanceSampleCube = (GameObject)Instantiate (Tree);
				_instanceSampleCube.transform.position = point;
				_instanceSampleCube.transform.parent = this.transform;
				_instanceSampleCube.name = "SampleTree"+point;
			}
		}
	}
	void delete(){
		UnityEditor.EditorApplication.delayCall+=()=>
     	{
          	foreach (Transform child in transform)
			{
				DestroyImmediate(child.gameObject);
			}
     	};
	}
}
