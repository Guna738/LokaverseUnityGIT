using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleMovement))]
public class SimpleMovementEditor : Editor {
    const string ControlText = "Controls:\n" +
                                     "   • RightMouse Click to lookAround\n"+
                                     "   • WASD or Arrowkeys to move\n";
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Rect rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight * 3);
        Draw(rect, ControlText, MessageType.None);
    }
    void Draw(Rect position, string text, MessageType messageType)
    {
        position.height -= EditorGUIUtility.standardVerticalSpacing;

        int oldFontSize = EditorStyles.helpBox.fontSize;
        EditorStyles.helpBox.fontSize = 12;
        FontStyle oldFontStyle = EditorStyles.helpBox.fontStyle;
        EditorStyles.helpBox.fontStyle = FontStyle.Bold;
        bool oldWordWrap = EditorStyles.helpBox.wordWrap;
        EditorStyles.helpBox.wordWrap = false;

        EditorGUI.HelpBox(position, text, messageType);

        EditorStyles.helpBox.fontSize = oldFontSize;
        EditorStyles.helpBox.fontStyle = oldFontStyle;
        EditorStyles.helpBox.wordWrap = oldWordWrap;
    }
}

