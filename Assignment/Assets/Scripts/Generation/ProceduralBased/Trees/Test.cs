using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	
	public GameObject Tree;
	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius =1;

	List<Vector2> points;
	Vector3 treevector;
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
			foreach (Vector2 point in points) {
				treevector.Set(point.x, 0, point.y);
				GameObject _instanceSampleCube = (GameObject)Instantiate (Tree);
				_instanceSampleCube.transform.position = treevector;
				_instanceSampleCube.transform.parent = this.transform;
				_instanceSampleCube.name = "SampleTree"+treevector;
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
