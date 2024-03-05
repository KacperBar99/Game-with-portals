using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PortalCamera))]
public class PortalScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PortalCamera data = (PortalCamera)target;
        EditorGUILayout.LabelField(data.name.ToUpper(), EditorStyles.boldLabel);

        base.OnInspectorGUI();
        
    }
}
