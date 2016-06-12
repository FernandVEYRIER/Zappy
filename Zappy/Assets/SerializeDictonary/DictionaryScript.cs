#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(DictionaryUnity))]
public class DictionaryScript : Editor
{
    public string[] options_1 = new string[] { "Int", "String" };
    private int index_1 = 0;
    public string[] options_2 = new string[] { "Int", "String", "Vector2", "Vector3" };
    private int index_2 = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DictionaryUnity dico = (DictionaryUnity)target;
        index_1 = EditorGUILayout.Popup(index_1, options_1);
        index_2 = EditorGUILayout.Popup(index_2, options_2);
        if (GUILayout.Button("Create"))
        {
            dico.createDictionary(index_1, index_2);
        }
    }
}
#endif
