﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UxrIKBodySettingsDrawer.cs" company="VRMADA">
//   Copyright (c) VRMADA, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UltimateXR.Animation.IK;
using UltimateXR.Avatar;
using UltimateXR.Avatar.Controllers;
using UltimateXR.Core;
using UnityEditor;
using UnityEngine;

namespace UltimateXR.Editor.Animation.IK
{
    /// <summary>
    ///     Custom inspector for <see cref="UxrBodyIKSettings" />.
    /// </summary>
    [CustomPropertyDrawer(typeof(UxrBodyIKSettings))]
    public class UxrIKBodySettingsDrawer : PropertyDrawer
    {
        #region Public Types & Data

        public const string PropertyLockBodyPivot          = "_lockBodyPivot";
        public const string PropertyBodyPivotRotationSpeed = "_bodyPivotRotationSpeed";
        public const string PropertyHeadFreeRangeBend      = "_headFreeRangeBend";
        public const string PropertyHeadFreeRangeTorsion   = "_headFreeRangeTorsion";
        public const string PropertyNeckHeadBalance        = "_neckHeadBalance";
        public const string PropertySpineBend              = "_spineBend";
        public const string PropertySpineTorsion           = "_spineTorsion";
        public const string PropertyChestBend              = "_chestBend";
        public const string PropertyChestTorsion           = "_chestTorsion";
        public const string PropertyUpperChestBend         = "_upperChestBend";
        public const string PropertyUpperChestTorsion      = "_upperChestTorsion";
        public const string PropertyNeckBaseHeight         = "_neckBaseHeight";
        public const string PropertyNeckForwardOffset      = "_neckForwardOffset";
        public const string PropertyEyesBaseHeight         = "_eyesBaseHeight";
        public const string PropertyEyesForwardOffset      = "_eyesForwardOffset";

        #endregion

        #region Public Overrides PropertyDrawer

        /// <summary>
        ///     Gets the height in pixels for the given serialized property targeting a <see cref="UxrBodyIKSettings" />.
        /// </summary>
        /// <param name="property">Serialized property</param>
        /// <param name="label">UI label</param>
        /// <returns>Height in pixels</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ShowNeckProperties(property))
            {
                return 15 * EditorGUIUtility.singleLineHeight;
            }
            return 13 * EditorGUIUtility.singleLineHeight;
        }

        #endregion

        #region Unity

        /// <summary>
        ///     Draws an <see cref="UxrBodyIKSettings" /> serialized property and handles input.
        /// </summary>
        /// <param name="position">Position where to draw the property</param>
        /// <param name="property">Serialized property</param>
        /// <param name="label">UI label</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            int line = 0;

            UxrAvatar avatar = ((MonoBehaviour)property.serializedObject.targetObject).GetComponent<UxrAvatar>();
            property.FindPropertyRelative(PropertyLockBodyPivot).boolValue = EditorGUI.Toggle(GetRect(position, line++, -1), ContentLockBodyPivot, property.FindPropertyRelative(PropertyLockBodyPivot).boolValue);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyBodyPivotRotationSpeed), 0.0f, 1.0f,   ContentBodyPivotRotationSpeed);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyHeadFreeRangeBend),      0.0f, 180.0f, ContentHeadFreeRangeBend);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyHeadFreeRangeTorsion),   0.0f, 180.0f, ContentHeadFreeRangeTorsion);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyNeckHeadBalance),        0.0f, 1.0f,   ContentNeckHeadBalance);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertySpineBend),              0.0f, 1.0f,   ContentSpineBend);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertySpineTorsion),           0.0f, 1.0f,   ContentSpineTorsion);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyChestBend),              0.0f, 1.0f,   ContentChestBend);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyChestTorsion),           0.0f, 1.0f,   ContentChestTorsion);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyUpperChestBend),         0.0f, 1.0f,   ContentUpperChestBend);
            EditorGUI.Slider(GetRect(position, line++, -1), property.FindPropertyRelative(PropertyUpperChestTorsion),      0.0f, 1.0f,   ContentUpperChestTorsion);

            if (ShowNeckProperties(property))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.PropertyField(GetRect(position, line++, 0), property.FindPropertyRelative(PropertyNeckBaseHeight), ContentNeckBaseHeight);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUI.PropertyField(GetRect(position, line++, 0), property.FindPropertyRelative(PropertyNeckForwardOffset), ContentNeckForwardOffset);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUI.PropertyField(GetRect(position, line, 0), property.FindPropertyRelative(PropertyEyesBaseHeight), ContentEyesBaseHeight);
            bool useAvatarEyesPressedBaseHeight = GUI.Button(GetRect(position, line, 1), new GUIContent("Use Avatar Eyes"));
            EditorGUILayout.EndHorizontal();
            
            if (useAvatarEyesPressedBaseHeight)
            {
                if (avatar.AvatarRig.Head.LeftEye == null || avatar.AvatarRig.Head.RightEye == null)
                {
                    EditorUtility.DisplayDialog("Assign field first", "The avatar component Rig field has eye(s) missing. Try to assign the value manually and use the eye gizmos for guidance.", UxrConstants.Editor.Ok);
                    GUIUtility.ExitGUI();
                }
                else
                {
                    property.FindPropertyRelative(PropertyEyesBaseHeight).floatValue = (avatar.AvatarRig.Head.LeftEye.position.y + avatar.AvatarRig.Head.RightEye.position.y) * 0.5f - avatar.transform.position.y;
                }
            }

            line++;

            EditorGUILayout.BeginHorizontal();
            EditorGUI.PropertyField(GetRect(position, line, 0), property.FindPropertyRelative(PropertyEyesForwardOffset), ContentEyesForwardOffset);
            EditorGUILayout.EndHorizontal();

            bool useAvatarEyesPressedForwardOffset = GUI.Button(GetRect(position, line, 1), new GUIContent("Use Avatar Eyes"));
            
            if (useAvatarEyesPressedForwardOffset)
            {
                if (avatar.AvatarRig.Head.LeftEye == null || avatar.AvatarRig.Head.RightEye == null)
                {
                    EditorUtility.DisplayDialog("Assign field first", "The avatar component Rig field has eye(s) missing. Try to assign the value manually and use the eye gizmos for guidance.", UxrConstants.Editor.Ok);
                    GUIUtility.ExitGUI();
                }
                else
                {
                    Vector3 eyeLeft  = avatar.transform.InverseTransformPoint(avatar.AvatarRig.Head.LeftEye.position);
                    Vector3 eyeRight = avatar.transform.InverseTransformPoint(avatar.AvatarRig.Head.RightEye.position);
                    property.FindPropertyRelative(PropertyEyesForwardOffset).floatValue = (eyeLeft.z + eyeRight.z) * 0.5f + 0.02f;
                }
            }

            EditorGUI.EndProperty();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Checks whether to show the neck properties of the serialized <see cref="UxrBodyIKSettings" />.
        /// </summary>
        /// <param name="property">Serialized property</param>
        /// <returns>Whether to show the neck properties</returns>
        private static bool ShowNeckProperties(SerializedProperty property)
        {
            UxrAvatarController controller = property.serializedObject.targetObject as UxrAvatarController;

            if (controller == null)
            {
                return true;
            }

            if (!controller.TryGetComponent<UxrAvatar>(out var avatar))
            {
                return true;
            }

            return avatar.AvatarRig.Head.Neck == null;
        }

        /// <summary>
        ///     Gets the rect for a given line in the property, since it will have multiple lines.
        /// </summary>
        /// <param name="position">Serialized property draw position</param>
        /// <param name="line">Line index</param>
        /// <param name="column">Column index</param>
        /// <returns>Rect to draw the given line in</returns>
        private static Rect GetRect(Rect position, int line, int column)
        {
            const int buttonWidth = 150;
            const int margin      = 5;

            return column switch
                   {
                               -1 => new Rect(position.x,                                position.y + EditorGUIUtility.singleLineHeight * line, position.width,                        EditorGUIUtility.singleLineHeight),
                               0  => new Rect(position.x,                                position.y + EditorGUIUtility.singleLineHeight * line, position.width - buttonWidth - margin, EditorGUIUtility.singleLineHeight),
                               _  => new Rect(position.x + position.width - buttonWidth, position.y + EditorGUIUtility.singleLineHeight * line, buttonWidth,                           EditorGUIUtility.singleLineHeight)
                   };
        }

        #endregion

        #region Private Types & Data

        private GUIContent ContentLockBodyPivot          { get; } = new GUIContent("Lock Body Pivot",           "For applications that require the avatar to remain in a fixed position");
        private GUIContent ContentBodyPivotRotationSpeed { get; } = new GUIContent("Body Pivot Rotation Speed", "The speed the body will twist to keep up with the head orientation");
        private GUIContent ContentHeadFreeRangeBend      { get; } = new GUIContent("Head Free Range Bend",      "The amount of degrees the head can rotate up and down without requiring support from the neck and bones below");
        private GUIContent ContentHeadFreeRangeTorsion   { get; } = new GUIContent("Head Free Range Torsion",   "The amount of degrees the head can rotate left and right without requiring support from the neck and bones below");
        private GUIContent ContentNeckHeadBalance        { get; } = new GUIContent("Neck-Head Balance",         "The balance between the neck and the head when solving the head orientation. Lower values will have the neck play a bigger role, while higher values will make the head play a bigger role");
        private GUIContent ContentSpineBend              { get; } = new GUIContent("Spine Bend",                "The amount of weight the spine will apply to solve up/down rotations");
        private GUIContent ContentSpineTorsion           { get; } = new GUIContent("Spine Torsion",             "The amount of weight the spine will apply to solve left/right rotations");
        private GUIContent ContentChestBend              { get; } = new GUIContent("Chest Bend",                "The amount of weight the chest will apply to solve up/down rotations");
        private GUIContent ContentChestTorsion           { get; } = new GUIContent("Chest Torsion",             "The amount of weight the chest will apply to solve left/right rotations");
        private GUIContent ContentUpperChestBend         { get; } = new GUIContent("Upper Chest Bend",          "The amount of weight the upper chest will apply to solve up/down rotations");
        private GUIContent ContentUpperChestTorsion      { get; } = new GUIContent("Upper Chest Torsion",       "The amount of weight the upper chest will apply to solve left/right rotations");
        private GUIContent ContentNeckBaseHeight         { get; } = new GUIContent("Neck Base Height",          "The height on the avatar where the base of the neck is located. The neck base will be drawn on the scene window as a white disc gizmo");
        private GUIContent ContentNeckForwardOffset      { get; } = new GUIContent("Neck Forward Offset",       "The forward offset from the avatar pivot where the neck is located. The neck base will be drawn on the scene window as a white disc gizmo");
        private GUIContent ContentEyesBaseHeight         { get; } = new GUIContent("Eyes Base Height",          "The height on the avatar where the eyes are located. The eye positions will be drawn on the scene window as white disc gizmos");
        private GUIContent ContentEyesForwardOffset      { get; } = new GUIContent("Eyes Forward Offset",       "The forward offset from the avatar pivot where the eyes are located. The eye positions will be drawn on the scene window as white disc gizmos");

        #endregion
    }
}