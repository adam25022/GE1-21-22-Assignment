using UnityEngine;
using System.Collections;
using UnityEditor;

//here we designate the cs file we want to edit and say its a custom editor for that cs file
[CustomEditor (typeof (MapGen))]
public class MapGenEdit : Editor {

	public override void OnInspectorGUI() {
		//here we get a reference to the cs file we want to edit
		MapGen mapGen = (MapGen)target;
		//update on change made to the selections
		//default draw inspector means a change was made to the values inputted
		if (DrawDefaultInspector ()) {
			// this is a tickbox to see if we want the script to automatically update
			if (mapGen.AutomaticallyUpdate) {
				mapGen.UserSelectedDrawingStyle ();
			}
		}
		//update on button press made
		if (GUILayout.Button ("Create")) {
			mapGen.UserSelectedDrawingStyle ();
		}
	}
}
