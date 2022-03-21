using UnityEngine;
using UnityEditor;

public class MiniTools : MonoBehaviour
{
    static Vector3 SnapVector(Vector3 src)
    {
        Vector3 des = Vector3.one;
        des.x = Mathf.RoundToInt(src.x);
        des.y = Mathf.RoundToInt(src.y);
        des.z = Mathf.RoundToInt(src.z);
        return des;
    }
    [MenuItem("MiniTool/隐藏物体 #a")]
    static void HideObj()
    {
        GameObject[] objects = Selection.gameObjects;
        foreach (GameObject obj in objects)
            obj.SetActive(!obj.activeSelf);
    }
    [MenuItem("MiniTool/坐标归为整数 #s")]
    static void SnapObjAll()
    {
        GameObject[] objects = Selection.gameObjects;
        foreach (GameObject o in objects)
        {
            Transform[] tran = o.GetComponentsInChildren<Transform>(true);
            Debug.Log(tran.Length);
            foreach (Transform tr in tran)
            {
                tr.localPosition = SnapVector(tr.localPosition);
                //tr.localScale = SnapVector(tr.localScale);
                string str = "name:" + tr.name + "P:" + tr.localPosition + "R:" + tr.localRotation + "S:" + tr.localScale;
                Debug.Log(str);
            }
        }
    }
}
