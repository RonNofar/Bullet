using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameController))]
public class InspectorTool : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		GameController NewSpawner = (GameController)target;
		if (GUILayout.Button ("Add new Spawner")) {
			NewSpawner.AddNewSpawner ();
		}
	}
}
