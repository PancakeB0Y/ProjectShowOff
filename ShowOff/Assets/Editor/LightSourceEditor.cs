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
        float currentRadius = targetLight.GetRadius();

        //Calculate handle position
        Vector3 offset = new Vector3(-0.5f, 1f, 0);
        Vector3 handlePos = targetLight.gameObject.transform.position + offset;

        float handleSize = HandleUtility.GetHandleSize(targetLight.transform.position) * 5f;
        float snap = 0.1f;

        EditorGUI.BeginChangeCheck();

        //Get new light radius
        float newRadius = Handles.ScaleValueHandle(currentRadius, handlePos, Quaternion.identity, handleSize, Handles.ArrowHandleCap, snap);

        if (EditorGUI.EndChangeCheck())
        {
            //Record for undo
            Undo.RecordObject(targetLight, "Scale Light Radius");

            //Update light radius
            targetLight.UpdateRadius(newRadius);

            //Mark light as dirty
            EditorUtility.SetDirty(targetLight);
        }
    }
}

