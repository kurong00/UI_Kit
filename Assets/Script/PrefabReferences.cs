using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PrefabReferences : EditorWindow
{
    private static Object searchObject;
    private static string PrefabFolder = "Assets/Res/UI";
    Vector2 scrollPos;
    List<Object> result = new List<Object>();
    [MenuItem("MiniTool/Search Dependence")]
    static void Open()
    {
        PrefabReferences window = (PrefabReferences)EditorWindow.GetWindow(typeof(PrefabReferences));
        window.Show();
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        searchObject = EditorGUILayout.ObjectField(searchObject, typeof(Object), true, GUILayout.Width(200));
        if (GUILayout.Button("Search", GUILayout.Width(100)))
        {
            result.Clear();
            if (searchObject == null)
                return;
            string prefabPath = AssetDatabase.GetAssetPath(searchObject);
            string[] paths = AssetDatabase.GetDependencies(prefabPath);
            foreach (string path in paths)
            {
                Object fileObject = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
                System.Type type = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (type.FullName != "UnityEditor.MonoScript" && type.FullName != "UnityEngine.GameObject" && type.FullName != "UnityEngine.Material")
                    result.Add(fileObject);
            }
        }
        EditorGUILayout.EndHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(300), GUILayout.Height(500));
        for (int i = 0; i < result.Count; i++)
        {
            EditorGUILayout.ObjectField(result[i], typeof(Object), true, GUILayout.Width(280));
        }
        EditorGUILayout.EndScrollView();
    }
}
