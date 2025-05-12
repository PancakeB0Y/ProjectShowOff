using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(PointLightScaler), true)]
public class LightEditor : Editor
{ 
    private void OnSceneGUI()
    {
        PointLightScaler targetLight = (PointLightScaler)target;

        //Get current light radius
        float colliderRadius = targetLight.GetLightRadius();

        //Calculate handle position
        Vector3 offset = new Vector3(-0.5f, 1f, 0);
        Vector3 pos = targetLight.gameObject.transform.position + offset;

        float size = HandleUtility.GetHandleSize(targetLight.transform.position) * 5f;
        float snap = 0.1f;

        EditorGUI.BeginChangeCheck();

        colliderRadius = Handles.ScaleValueHandle(colliderRadius, pos, Quaternion.identity, size, Handles.ArrowHandleCap, snap);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetLight.gameObject, "Scaled Light");
            targetLight.UpdateColliderRadius(colliderRadius);
            EditorUtility.SetDirty(targetLight);
        }
    }
}

