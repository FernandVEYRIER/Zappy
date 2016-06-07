#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Plateform))]
public class BuilderScript : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Plateform myScript = (Plateform)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.Build();
        }
        else if (GUILayout.Button("Delete Object"))
        {
            myScript.Delete();
        }
    }
}
#endif