using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightSourceChangeListener))]
public class LightSourceChangeListenerEditor : Editor
{
    LightSourceChangeListener listener;

    void OnEnable()
    {
        listener = (LightSourceChangeListener)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (listener.targetLight != null)
        {
            float currentIntensity = listener.targetLight.intensity;
            float currentRange = listener.targetLight.range;

            if (currentIntensity != listener.lastIntensity || currentRange != listener.lastRange)
            {
                listener.lastIntensity = currentIntensity;
                listener.lastRange = currentRange;
                listener.OnLightValuesChanged();
            }
        }

        if (GUI.changed)
            EditorUtility.SetDirty(listener); // mark for update
    }

}
