using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

[CustomEditor(typeof(LightSource), true)]
public class LightSourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightSource targetLight = (LightSource)target;

        if (GUILayout.Button("Turn On"))
        {
            targetLight.TurnLightOn();
        }
        if (GUILayout.Button("Turn Off"))
        {
            targetLight.TurnLightOff();
        }
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        LightSource targetLight = (LightSource)target;

        //Get current light radius
        float colliderRadius = targetLight.GetColliderRadius();

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

