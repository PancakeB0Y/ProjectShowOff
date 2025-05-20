using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindSphere), true)]
public class WindSphereEditor : Editor
{
    private void OnSceneGUI()
    {
        WindSphere targetWind = (WindSphere)target;

        //Get current wind radius
        float currentRadius = targetWind.GetRadius();

        //Calculate handle position
        Vector3 offset = new Vector3(-0.5f, 1f, 0);
        Vector3 handlePos = targetWind.gameObject.transform.position + offset;

        float handleSize = HandleUtility.GetHandleSize(targetWind.transform.position) * 5f;
        float snap = 0.1f;

        EditorGUI.BeginChangeCheck();

        //Get new wind radius
        float newRadius = Handles.ScaleValueHandle(currentRadius, handlePos, Quaternion.identity, handleSize, Handles.ArrowHandleCap, snap);

        if (EditorGUI.EndChangeCheck())
        {
            //Record for undo
            Undo.RecordObject(targetWind, "Scaled Wind Radius");

            //Update wind radius
            targetWind.UpdateRadius(newRadius);

            //Mark wind as dirty
            EditorUtility.SetDirty(targetWind);
        }
    }
}
