using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(InstructionClass))]
public class InstructionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        InstructionClass ic = (InstructionClass)target;
        Undo.RecordObject(ic, "Modified");
        PrefabUtility.RecordPrefabInstancePropertyModifications(ic);
        DrawOtherField(ic);
        DrawAdvanced(ic);
    }
    void DrawOtherField(InstructionClass ic){
        //TextType Serializer
        string[] TextTypeOptions = new string[]
        {
            "TextMeshPro", "DefaultUIText",
        };
        ic.selectedTextType = EditorGUILayout.Popup("Text Type", ic.selectedTextType, TextTypeOptions);

        ic.textType = (InstructionClass.TextType)ic.selectedTextType;

        if (ic.textType == InstructionClass.TextType.TextMeshPro)
        {
            ic.textToDisplay1 = (TextMeshProUGUI)EditorGUILayout.ObjectField("DisplayText", ic.textToDisplay1, typeof(TextMeshProUGUI), true);
        }
        else
        {
            ic.textToDisplay = (Text)EditorGUILayout.ObjectField("DisplayText", ic.textToDisplay, typeof(Text), true);
        }

        //InstructionType Serializer
        string[] InstrTypeOptions = new string[]
        {
            "InstructionObject", "CustomInstruction",
        };
        ic.selectedInstrType = EditorGUILayout.Popup("Instruction Type", ic.selectedInstrType, InstrTypeOptions);

        ic.instructionType = (InstructionClass.InstructionType)ic.selectedInstrType;

        if (ic.instructionType == InstructionClass.InstructionType.InstructionObject)
        {
            ic.instructionObject = (InstructionObject)EditorGUILayout.ObjectField("InstructionObject", ic.instructionObject, typeof(InstructionObject), true);
        }

        //AnimationType Serializer
        string[] AnimTypeOptions = new string[]
        {
            "None", "TypeIn", "TypeInOut", "TypeInFadeOut", "FadeInOut"
        };
        ic.selectedAnimType = EditorGUILayout.Popup("TextAnimationType", ic.selectedAnimType, AnimTypeOptions);

        ic.TextanimationType = (InstructionClass.TextAnimationType)ic.selectedAnimType;

    }
    void DrawAdvanced(InstructionClass ic)
    {
        //Advanced dropdown Serializer
        ic.showAdvanced = EditorGUILayout.Foldout(ic.showAdvanced, "Advanced", true);
        if (ic.showAdvanced)
        {
            EditorGUILayout.LabelField("Additional UI Reference", EditorStyles.boldLabel);
            
            ic.UsePreviousButton = EditorGUILayout.Toggle(new GUIContent("UsePreviousButton", "When Enabled, players can go back to a previous instruction"), ic.UsePreviousButton);
            if (ic.UsePreviousButton)
            {
                ic.PrevButton = (GameObject)EditorGUILayout.ObjectField("Previous Button", ic.PrevButton, typeof(GameObject), true);
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("DisplayText Settings", EditorStyles.boldLabel);

            bool typeMethod = ic.TextanimationType == InstructionClass.TextAnimationType.TypeIn || ic.TextanimationType == InstructionClass.TextAnimationType.TypeInOut ||
            ic.TextanimationType == InstructionClass.TextAnimationType.TypeInFadeOut;
            if (typeMethod) { ic.TypeSpeed = EditorGUILayout.FloatField(new GUIContent("TypeSpeed", "Adjust the speed of typing speed, maximum speed is 10"), ic.TypeSpeed); }
            bool FadeMethod = ic.TextanimationType == InstructionClass.TextAnimationType.TypeInFadeOut || ic.TextanimationType == InstructionClass.TextAnimationType.FadeInOut;
            if (FadeMethod) { ic.fadeRate = EditorGUILayout.FloatField(new GUIContent("FadeRate", "Adjust the fade duration, "), ic.fadeRate); }


            ic.AutoNext = EditorGUILayout.Toggle(new GUIContent("AutoNext", "Enabled this if you want the dialogue to go to next one after a few seconds"), ic.AutoNext);
            if (ic.AutoNext)
            {
                ic.AutoNextDelay = EditorGUILayout.FloatField(new GUIContent("AutoNextDelay", "How long to wait until it goes to the next instruction."), ic.AutoNextDelay);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Panel Display Option", EditorStyles.boldLabel);
            ic.AnimatedPanel = EditorGUILayout.Toggle(new GUIContent("AnimatedPanel", "Enabled this if you want to animate in and out Canvas/Panel"), ic.AnimatedPanel);
            if (ic.AnimatedPanel)
            {
                ic.OpenDelay = EditorGUILayout.FloatField(new GUIContent("OpenDelay", "How long to wait until it begins writing the instruction. This normally should be the open animation duration "), ic.OpenDelay);
                ic.CloseDelay = EditorGUILayout.FloatField(new GUIContent("CloseDelay", "How long to wait before it closes (disable) the instruction Panel. This normally should be the close animation duration "), ic.CloseDelay);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Instructon-Scene Interaction", EditorStyles.boldLabel);

            ic.persistBetweenScence = EditorGUILayout.Toggle(new GUIContent("PersistBetweenScenes", "Enabled this if you want to the Instruction and its canvas to persist between different Scenes."), ic.persistBetweenScence);
            if (ic.persistBetweenScence)
            {
                ic.Instructionpanel.transform.parent = ic.transform;
            }
            else
            {
                ic.Instructionpanel.transform.parent = null;
            }
        }
    }
}
