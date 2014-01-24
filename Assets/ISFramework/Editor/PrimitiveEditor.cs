using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrimitiveEditor : EditorWindow {
	
	private static PrimitiveEditor _window = null;
	private static PrimitiveEditor window
	{
		get
		{
			if ( _window == null )
			{
				_window = EditorWindow.GetWindow( typeof( PrimitiveEditor ) ) as PrimitiveEditor;
			}
			return _window;
		}
	}
	
	private enum Primitive { QUAD, QUAD2S, DISK, CYLINDER, CYLINDER2S, CUBE, SPHERE };
	
	private Primitive selectedPrimitive = Primitive.QUAD;
	
	private Vector3 meshPosition = Vector3.zero;
	private Vector3 meshRotation = Vector3.zero;
	private Vector3 meshScale = Vector3.one;
	
	private int input1 = 3;
	private int input2 = 3;
	
	private const int minValue = 3;
	private const int maxValue = 60;
	
	[MenuItem ("ISTools/Primitive creator")]
	public static void Init()
	{
		_window = (PrimitiveEditor)EditorWindow.GetWindow( typeof( PrimitiveEditor ) );
	}
	
	void OnGUI()
	{
		selectedPrimitive = ( Primitive )EditorGUILayout.EnumPopup( new GUIContent( "Selected Primitive" ), selectedPrimitive );
		
		switch ( selectedPrimitive )
		{
		case Primitive.QUAD:
			
			break;
		case Primitive.QUAD2S:
			
			break;
		case Primitive.DISK:
			input1 = EditorGUILayout.IntSlider( new GUIContent( "Subdivisions" ), input1, minValue, maxValue );
			break;
		case Primitive.CYLINDER:
			input1 = EditorGUILayout.IntSlider( new GUIContent( "Subdivisions" ), input1, minValue, maxValue );
			break;
		case Primitive.CYLINDER2S:
			input1 = EditorGUILayout.IntSlider( new GUIContent( "Subdivisions" ), input1, minValue, maxValue );
			break;
		case Primitive.CUBE:
			
			break;
		case Primitive.SPHERE:
			input1 = EditorGUILayout.IntSlider( new GUIContent( "Subdivisions Axis" ), input1, minValue, maxValue );
			input2 = EditorGUILayout.IntSlider( new GUIContent( "Subdivisions Height" ), input2, minValue, maxValue );
			break;
		}
		
		GUILayout.Label( new GUIContent( "Mesh Offsets" ) );
		meshPosition = EditorGUILayout.Vector3Field( "Position", meshPosition );
		meshRotation = EditorGUILayout.Vector3Field( "Rotation", meshRotation );
		meshScale = EditorGUILayout.Vector3Field( "Scale", meshScale );
		
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		if ( GUILayout.Button( new GUIContent( "Create" ), GUILayout.MaxWidth( 80f ) ) )
		{
			Undo.RegisterSceneUndo( "Created primitive:" + selectedPrimitive.ToString() );
			CreatePrimitive();
		}
		
		GUILayout.EndHorizontal();
	}
	
	private void CreatePrimitive()
	{
		Mesh mesh = null;
		
		switch ( selectedPrimitive )
		{
		case Primitive.QUAD:
			mesh = MeshEx.CreateQuad();
			break;
		case Primitive.QUAD2S:
			mesh = MeshEx.CreateTwoSidedQuad();
			break;
		case Primitive.DISK:
			mesh = MeshEx.CreateDisk( input1 );
			break;
		case Primitive.CYLINDER:
			mesh = MeshEx.CreateCylinder( input1 );
			break;
		case Primitive.CYLINDER2S:
			mesh = MeshEx.CreateTwoSidedCylinder( input1 );
			break;
		case Primitive.CUBE:
			mesh = MeshEx.CreateCube();
			break;
		case Primitive.SPHERE:
			mesh = MeshEx.CreateSphere( input1, input2 );
			break;
		}
		
		mesh = MeshEx.TransformMesh( mesh, meshPosition, Quaternion.Euler( meshRotation ), meshScale );
		
		GameObject go = new GameObject( mesh.name );
		go.AddComponent<MeshFilter>().mesh = mesh;
		go.AddComponent<MeshRenderer>();
		
		Selection.objects = new Object[]{ go };
	}
	
	
}
