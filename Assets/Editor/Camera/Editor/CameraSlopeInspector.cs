using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraSlope))]
public class MyScriptEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CameraSlope cameraSlope = (CameraSlope)target;

        if (GUILayout.Button("Apply Change")) {
            cameraSlope.SetObliqueness();
        }
    }
}