using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutomaticVerticalSize))]
public class AutomaticVerticalSizeEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Recalculate size"))
        {
            AutomaticVerticalSize script = ((AutomaticVerticalSize)target);
            script.AdjustSize();
        }

    }
}
