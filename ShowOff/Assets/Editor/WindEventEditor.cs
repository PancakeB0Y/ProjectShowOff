using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindEvent), true)]
public class WindEventEditor : Editor
{
    private void OnSceneGUI()
    {
        WindEvent targetWind = (WindEvent)target;

        //Get current collider targetWind
        float colliderRadius = targetWind.GetColliderRadius();

        //Calculate handle position
        Vector3 offset = new Vector3(-0.5f, 1f, 0);
        Vector3 pos = targetWind.gameObject.transform.position + offset;

        float size = HandleUtility.GetHandleSize(targetWind.transform.position) * 5f;
        float snap = 0.1f;

        EditorGUI.BeginChangeCheck();

        colliderRadius = Handles.ScaleValueHandle(colliderRadius, pos, Quaternion.identity, size, Handles.ArrowHandleCap, snap);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetWind.gameObject, "Scaled Light");
            targetWind.UpdateColliderRadius(colliderRadius);
            EditorUtility.SetDirty(targetWind);
        }
    }
}
