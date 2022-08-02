using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FindOfView))]
public class FindOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FindOfView view = (FindOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(view.transform.position, Vector3.up, Vector3.forward, 360, view.viewRadius);//사거리 원 그리기
        Vector3 viewAngleA = view.DirFromAngle(-view.viewAngle / 2, false);
        Vector3 viewAngleB = view.DirFromAngle(view.viewAngle / 2, false);

        Handles.DrawLine(view.transform.position, view.transform.position + viewAngleA * view.viewRadius);
        Handles.DrawLine(view.transform.position, view.transform.position + viewAngleB * view.viewRadius);
    }
}