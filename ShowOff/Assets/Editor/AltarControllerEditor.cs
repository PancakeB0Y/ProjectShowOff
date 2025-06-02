using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AltarController), true)]
public class AltarControllerEditor : Editor
{
    private void OnSceneGUI()
    {
        AltarController targetAltar = (AltarController)target;

        ////Get current wind radius
        //Vector3[] itemSlots = targetAltar.itemSlots;

        //EditorGUI.BeginChangeCheck();

        //for (int i = 0; i < itemSlots.Length; i++) {
        //    itemSlots[i] = Handles.PositionHandle(itemSlots[i], Quaternion.identity);
        //}

        //if (EditorGUI.EndChangeCheck())
        //{
        //    //Record for undo
        //    Undo.RecordObject(targetAltar, "Moved item slots");

        //    //Mark wind as dirty
        //    EditorUtility.SetDirty(targetAltar);
        //}
    }
}
