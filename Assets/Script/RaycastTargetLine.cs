using UnityEngine;
using UnityEngine.UI;
public class RaycastTargetLine : MonoBehaviour
{
	static Vector3[] fourCorners = new Vector3[4];
	void OnDrawGizmos()
	{
		foreach (MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
		{
			if (g.raycastTarget)
			{
				RectTransform rectTransform = g.transform as RectTransform;
				rectTransform.GetWorldCorners(fourCorners);
				Gizmos.color = Color.red;
				for (int i = 0; i < 4; i++)
					Gizmos.DrawLine(fourCorners[i]+Vector3.one, fourCorners[(i + 1) % 4] + Vector3.one);
			}
		}
	}
}
