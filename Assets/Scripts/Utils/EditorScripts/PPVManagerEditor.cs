using UnityEngine;
using UnityEditor;
using YBC.Perceptix;
using YBC.Neemotix;
using System.Management.Instrumentation;

[CustomEditor( typeof( PPVManager ) )]
public class PPVManagerEditor : Editor
{
	
	public override void OnInspectorGUI()
	{
		/*
		PPVManager script = (PPVManager)target;
		
		Object o = EditorGUILayout.ObjectField( "NeemotixAdapter", (Object)script.neemotixAdapter, typeof( INeemotixAdapter ), true );
		script.neemotixAdapter = o as INeemotixAdapter;
		*/
		DrawDefaultInspector();
	}
}