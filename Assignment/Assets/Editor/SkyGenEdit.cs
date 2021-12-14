using UnityEngine;
using System.Collections;
using UnityEditor;

//here we designate the cs file we want to edit and say its a custom editor for that cs file
[CustomEditor (typeof (SkyGen))]
public class SkyGenEdit : Editor {

	public override void OnInspectorGUI() {
		//here we get a reference to the cs file we want to edit
		SkyGen skyGen = (SkyGen)target;
		//update on change made to the selections
		if (DrawDefaultInspector ()) {
			if (skyGen.autoUpdate) {
				skyGen.CreateSkyBasedOnSelection ();
			}
		}
		//update on button press made
		if (GUILayout.Button ("Create")) {
			skyGen.CreateSkyBasedOnSelection ();
		}
	}
}
