using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EtherObject))]
public class EtherObjectInspector : Editor {

	private EtherObject editortarget;

	void OnEnable() {
		editortarget = target as EtherObject;
	}

	public override void OnInspectorGUI() {

		editortarget.redState = GUILayout.Toggle( editortarget.redState, "Red" );
		editortarget.yellowState = GUILayout.Toggle( editortarget.yellowState, "Yellow" );
		editortarget.blueState = GUILayout.Toggle( editortarget.blueState, "Blue" );

		if ( GUI.changed ) {

			editortarget.objectColor = ( editortarget.redState ? EtherObject.stateColors[0].ToRGB() : Color.black ) + 
				                       ( editortarget.yellowState ? EtherObject.stateColors[1].ToRGB() : Color.black ) +
					                   ( editortarget.blueState ? EtherObject.stateColors[2].ToRGB() : Color.black );

			EditorUtility.SetDirty( editortarget );
		}
	}
}
