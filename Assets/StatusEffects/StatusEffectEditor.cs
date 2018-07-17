using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatusEffect))]
public class StatusEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        if (EditorApplication.isPlaying) return;

        var se = target as StatusEffect;

        int _fieldChoiceIndex = se.FieldChoiceIndex;
        int _typeChoiceIndex = se.TypeChoiceIndex;

        var scripts = se.ObjectToAffect.GetComponents<MonoBehaviour>();

        List<string> scriptNames = new List<string>();
        foreach (MonoBehaviour script in scripts)
            scriptNames.Add(script.GetType().Name);

        _typeChoiceIndex = EditorGUILayout.Popup(new GUIContent("Script To Affect", "Script that will be affected by this"), _typeChoiceIndex, scriptNames.ToArray());

        //Type scriptType = Type.GetType(scriptNames[_scriptChoiceIndex]);
        
        var fields = Type.GetType(scriptNames[_typeChoiceIndex]).GetFields();

        List<string> fieldNames = new List<string>();
        foreach (FieldInfo field in fields)
            fieldNames.Add(field.Name);

        _fieldChoiceIndex = EditorGUILayout.Popup(new GUIContent("Field To Affect", "Field that will be affected by the percent provided"), _fieldChoiceIndex, fieldNames.ToArray());

        // Update the selected choice in the underlying object
        se.TypeToAffect = scriptNames[_typeChoiceIndex];
        se.TypeChoiceIndex = _typeChoiceIndex;
        se.FieldToAffect = fieldNames[_fieldChoiceIndex];
        se.FieldChoiceIndex = _fieldChoiceIndex;
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}
