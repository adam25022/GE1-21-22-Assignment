using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGen))]
public class MapGenEdit : Editor {

	public override void OnInspectorGUI() {
		MapGen mapGen = (MapGen)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.autoUpdate) {
				mapGen.DrawMapInEditor ();
			}
		}

		if (GUILayout.Button ("Create")) {
			mapGen.DrawMapInEditor ();
		}
	}
}
