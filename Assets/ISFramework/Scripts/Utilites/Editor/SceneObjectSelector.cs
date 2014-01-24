using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneObjectSelector : MonoBehaviour {
	
	public static bool inUse = false;
	
	private static MonoBehaviour[] targetArray;
	private static List<SceneObjectSelector> selectorList;
	
	public delegate void SelectedObjectDelegate( MonoBehaviour returnObject );
	
	public SelectedObjectDelegate returnDelegate;
	public MonoBehaviour returnObject;
	
	public static void Setup <T> ( SelectedObjectDelegate returnDelegate ) {
		
		if ( inUse ) {
			Deconstruct(); // Remove old selection if it exist.
		}
		
		object[] objectArray = GameObject.FindObjectsOfType( typeof( T ) );
		targetArray = Array.ConvertAll( objectArray, item => (MonoBehaviour)item );
		selectorList = new List<SceneObjectSelector>();
		
		foreach ( MonoBehaviour targetObject in targetArray ) {
			GameObject go = new GameObject( "EditorSceneObjectSelector" );
			go.transform.position = targetObject.transform.position;
			
			SceneObjectSelector selector = go.AddComponent<SceneObjectSelector>();
			selector.returnDelegate = returnDelegate;
			selector.returnObject = targetObject;
			
			selectorList.Add(selector);
			
			go.hideFlags = HideFlags.DontSave;
		}
		
		SceneView.RepaintAll(); // Redraw scene view to show selection gizmos
		
		inUse = true;
	}
	
	public static void Deconstruct () {
		foreach ( SceneObjectSelector selector in selectorList ) {
			DestroyImmediate( selector.gameObject );
		}
		
		selectorList = null;
		inUse = false;
		SceneView.RepaintAll();
	}
	
	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere( transform.position, 1f );
	}
	
	void OnDrawGizmosSelected () {
		returnDelegate( returnObject );
		Deconstruct();
	}
}
