/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlateformSizeController))]
public class SaveObjectScale : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlateformSizeController controller_size = (PlateformSizeController)target;

        GUILayout.Label("Initial Data");

        if (GUILayout.Button("Save Original Pos"))
        {
            controller_size.originalPos = controller_size.transform.position;
        }

        if (GUILayout.Button("Save Original Size"))
        {
            controller_size.originalSize = controller_size.transform.localScale;
        }

        GUILayout.Label("Final Data");
        if (GUILayout.Button("Save Final Size"))
        {
            controller_size.finalSize = controller_size.transform.localScale;
        }

        if (GUILayout.Button("Save Final Pos"))
        {
            controller_size.finalPos = controller_size.transform.position;
        }

        GUILayout.Label("Set Data");

        if (GUILayout.Button("Set Original Pos"))
        {
            controller_size.transform.position = controller_size.originalPos;
        }

        if (GUILayout.Button("Set Original Size"))
        {
            controller_size.transform.localScale = controller_size.originalSize;
        }
    }

}
*/