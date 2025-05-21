using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OneSideLockedDoorController), true)]
public class OneSideLockedDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OneSideLockedDoorController targetDoor = (OneSideLockedDoorController)target;
        
        if (GUILayout.Button("Flip locked side"))
        {
            BoxCollider collider = targetDoor.GetComponent<BoxCollider>(); //record the door collider

            //Record for undo
            Undo.RecordObject(collider, "Flipped locked side of door");

            //Flip locked door side
            targetDoor.FlipLockedSide();

            //Mark door and collider as dirty
            EditorUtility.SetDirty(collider);
            EditorUtility.SetDirty(targetDoor);
        }

        DrawDefaultInspector();
    }
}
