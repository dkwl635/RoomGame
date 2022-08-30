using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CountGame))]
public class CountGameEditor : Editor
{
   public override void OnInspectorGUI()
    { 
        base.OnInspectorGUI();
        CountGame generator = (CountGame)target;
        if (GUILayout.Button("SettingNum"))
        { 
            generator.SettingNum(); 
        } 
    } 
}
