using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (SkyGen))]
public class SkyGenEdit : Editor {

	public override void OnInspectorGUI() {
		SkyGen skyGen = (SkyGen)target;

		if (DrawDefaultInspector ()) {
			if (skyGen.autoUpdate) {
				skyGen.CreateMapBasedOnSelection ();
			}
		}

		if (GUILayout.Button ("Create")) {
			skyGen.CreateMapBasedOnSelection ();
		}
	}
}
