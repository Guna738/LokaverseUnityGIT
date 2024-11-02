using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoloInstruction))]
[CanEditMultipleObjects()]
class SoloInstructionEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        SoloInstruction soloInstr = (SoloInstruction)target;
        Undo.RecordObject(soloInstr,"Modified");
        PrefabUtility.RecordPrefabInstancePropertyModifications(soloInstr);
        DrawCustom(soloInstr);
    }
    void DrawCustom(SoloInstruction soloInstr){
        bool ShowTag = soloInstr.callMethod == SoloInstruction.CallMethod.OnCollsionEnter|| 
        soloInstr.callMethod == SoloInstruction.CallMethod.OnCollsionExit||
        soloInstr.callMethod == SoloInstruction.CallMethod.OnTriggerEnter||
        soloInstr.callMethod == SoloInstruction.CallMethod.OnTriggerExit;

        if(ShowTag){
            soloInstr.ObjectTag = EditorGUILayout.TextField(new GUIContent("ObjectTag", "Tag of the Colliding or trigger Object to display the instruction"), soloInstr.ObjectTag);
        }
        bool ShowEvent = soloInstr.callMethod == SoloInstruction.CallMethod.Custom;
        if(ShowEvent){
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Reference this script and call ShowInstruction() when needed");
        }
    }
};


