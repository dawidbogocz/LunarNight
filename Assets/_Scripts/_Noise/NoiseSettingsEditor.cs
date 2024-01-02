using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoiseSettings))]
public class NoiseSettingsEditor : Editor
{
	private NoiseSettings noiseSettings;
	private Editor noiseSettingsEditor;

	private void OnEnable()
	{
		noiseSettings = (NoiseSettings)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); // Draw the default inspector

		// Check for changes in the inspector
		if (GUI.changed)
		{
			noiseSettings.NotifySettingsUpdated();
			EditorUtility.SetDirty(noiseSettings);
		}
	}
}
