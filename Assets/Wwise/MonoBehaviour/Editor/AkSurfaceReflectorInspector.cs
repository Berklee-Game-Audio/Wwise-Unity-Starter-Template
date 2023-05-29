#if UNITY_EDITOR
/*******************************************************************************
The content of this file includes portions of the proprietary AUDIOKINETIC Wwise
Technology released in source code form as part of the game integration package.
The content of this file may not be used without valid licenses to the
AUDIOKINETIC Wwise Technology.
Note that the use of the game engine is subject to the Unity(R) Terms of
Service at https://unity3d.com/legal/terms-of-service
 
License Usage
 
Licensees holding valid licenses to the AUDIOKINETIC Wwise Technology may use
this file in accordance with the end user license agreement provided with the
software or, alternatively, in accordance with the terms contained
in a written agreement between you and Audiokinetic Inc.
Copyright (c) 2023 Audiokinetic Inc.
*******************************************************************************/

[UnityEditor.CustomEditor(typeof(AkSurfaceReflector))]
[UnityEditor.CanEditMultipleObjects]
public class AkSurfaceReflectorInspector : UnityEditor.Editor
{
	private AkSurfaceReflector m_AkSurfaceReflector;

	private UnityEditor.SerializedProperty Mesh;
	private UnityEditor.SerializedProperty AcousticTextures;
	private UnityEditor.SerializedProperty TransmissionLossValues;
	private UnityEditor.SerializedProperty EnableDiffraction;
	private UnityEditor.SerializedProperty EnableDiffractionOnBoundaryEdges;
	private UnityEditor.SerializedProperty AssociatedRoom;

	public void OnEnable()
	{
		m_AkSurfaceReflector = target as AkSurfaceReflector;

		Mesh = serializedObject.FindProperty("Mesh");
		AcousticTextures = serializedObject.FindProperty("AcousticTextures");
		TransmissionLossValues = serializedObject.FindProperty("TransmissionLossValues");
		EnableDiffraction = serializedObject.FindProperty("EnableDiffraction");
		EnableDiffractionOnBoundaryEdges = serializedObject.FindProperty("EnableDiffractionOnBoundaryEdges");
		AssociatedRoom = serializedObject.FindProperty("AssociatedRoom");
	}

	public override void OnInspectorGUI()
	{
		bool GeometryNeedsUpdate = false;
		bool GeometryInstanceNeedsUpdate = false;

		serializedObject.Update();

		// Start a code block to check for GUI changes
		UnityEditor.EditorGUI.BeginChangeCheck();

		UnityEditor.EditorGUILayout.PropertyField(Mesh);

		UnityEditor.EditorGUILayout.PropertyField(AcousticTextures, true);
		CheckArraySize(m_AkSurfaceReflector, m_AkSurfaceReflector.AcousticTextures.Length, "acoustic textures");

		UnityEditor.EditorGUILayout.PropertyField(TransmissionLossValues, true);
		CheckArraySize(m_AkSurfaceReflector, m_AkSurfaceReflector.TransmissionLossValues.Length, "transmission loss values");

		UnityEditor.EditorGUILayout.PropertyField(EnableDiffraction);
		if (EnableDiffraction.boolValue)
		{
			UnityEditor.EditorGUILayout.PropertyField(EnableDiffractionOnBoundaryEdges);
		}

		if (UnityEditor.EditorGUI.EndChangeCheck())
		{
			GeometryNeedsUpdate = true;
		}

		//Start a code block to check for GUI changes
		UnityEditor.EditorGUI.BeginChangeCheck();

		UnityEditor.EditorGUILayout.PropertyField(AssociatedRoom);

		if (UnityEditor.EditorGUI.EndChangeCheck())
		{
			GeometryInstanceNeedsUpdate = true;
		}

		serializedObject.ApplyModifiedProperties();

		if (GeometryNeedsUpdate)
		{
			m_AkSurfaceReflector.SetGeometry();
		}

		if (GeometryInstanceNeedsUpdate)
		{
			m_AkSurfaceReflector.UpdateAssociatedRoom();
		}
	}

	public static void CheckArraySize(AkSurfaceReflector surfaceReflector, int length, string name)
	{
		if (surfaceReflector == null || surfaceReflector.Mesh == null)
		{
			return;
		}

		int maxSize = surfaceReflector.Mesh.subMeshCount;
		if (length <= maxSize)
		{
			return;
		}

		UnityEngine.GUILayout.Space(UnityEditor.EditorGUIUtility.standardVerticalSpacing);

		using (new UnityEditor.EditorGUILayout.VerticalScope("box"))
		{
			UnityEditor.EditorGUILayout.HelpBox(
				"There are more " + name + " than the Mesh has submeshes. Additional ones will be ignored.",
				UnityEditor.MessageType.Warning);
		}
	}
}
#endif