using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SearchReferences : EditorWindow
{
    [MenuItem("MiniTool/Search References")]
    static void Open()
    {
        SearchReferences window = (SearchReferences)EditorWindow.GetWindow(typeof(SearchReferences));
        window.Show();
    }
    private static Object searchObject;
    private List<Object> result = new List<Object>();
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        searchObject = EditorGUILayout.ObjectField(searchObject, typeof(Object), true, GUILayout.Width(200));
        if (GUILayout.Button("Search", GUILayout.Width(100)))
        {
            result.Clear();
            if (searchObject == null)
                return;
            string assetPath = AssetDatabase.GetAssetPath(searchObject);
            string assetGuid = AssetDatabase.AssetPathToGUID(assetPath);
            //加入筛选，只检查prefab，Assets/Res/UI：被筛选的集合
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Res/UI" });
            int length = guids.Length;
            for (int i = 0; i < length; i++)
            {
                string filePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                EditorUtility.DisplayCancelableProgressBar("Searching", filePath, i / length * 1.0f);
                string content = File.ReadAllText(filePath);
                if (content.Contains(assetGuid))
                {
                    Object fileObject = AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                    result.Add(fileObject);
                }
            }
            EditorUtility.ClearProgressBar();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < result.Count; i++)
        {
            EditorGUILayout.ObjectField(result[i], typeof(Object), true, GUILayout.Width(300));
        }
        EditorGUILayout.EndHorizontal();
    }

}