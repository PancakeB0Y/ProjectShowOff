using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindSection), true)]
public class WindSectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WindSection targetSection = (WindSection)target;

        if (GUILayout.Button("Add Wind Sphere"))
        {
            GameObject newSphere = targetSection.AddWindSphere();

            if (newSphere != null)
            {
                Undo.RegisterCreatedObjectUndo(newSphere, "Added Wind Sphere");
                Undo.RecordObject(targetSection, "Added Wind Sphere to Section");
                EditorUtility.SetDirty(targetSection);
            }
        }

        if (GUILayout.Button("Refresh Wind Sphere List"))
        {
            Undo.RecordObject(targetSection, "Cleaned up Wind Sphere List");
            targetSection.CleanUpSpheres();
            EditorUtility.SetDirty(targetSection);
        }

        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        WindSection targetSection = (WindSection)target;

        //Get current section bounds
        Vector3 currentBounds = targetSection.GetBounds();

        //Calculate handle position
        Vector3 offset = new Vector3(-0.5f, 1f, 0);
        Vector3 handlePos = targetSection.gameObject.transform.position + offset;

        EditorGUI.BeginChangeCheck();

        //Get new section bounds
        Vector3 newBounds = Handles.ScaleHandle(currentBounds, handlePos, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            //Record for undo
            Undo.RecordObject(targetSection, "Scaled Wind Section Bounds");

            //Update section bounds
            targetSection.UpdateBounds(newBounds);

            //Mark section as dirty
            EditorUtility.SetDirty(targetSection);
        }
    }
}
