// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Laser.cs" company="VRMADA">
//   Copyright (c) VRMADA, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Core.Components;
using UltimateXR.Devices;
using UltimateXR.Extensions.Unity.Math;
using UltimateXR.Extensions.Unity.Render;
using UltimateXR.Haptics.Helpers;
using UltimateXR.Manipulation;
using UnityEngine;
using UnityEngine.Rendering;

namespace UltimateXR.Examples.FullScene.Lab
{
    /// <summary>
    ///     Component that handles the laser in the lab room.
    /// </summary>
    public partial class CraneInput : UxrComponent
    {
        #region Inspector Properties/Serialized Fields


        [SerializeField] private LayerMask _collisionMask = -1;
        [SerializeField] private UxrGrabbableObject _triggerGrabbable;
        [SerializeField] private Transform _trigger;
        [SerializeField] private Vector3 _triggerOffset;
        [SerializeField] private UxrFixedHapticFeedback _laserHaptics;

        #endregion

        #region Unity

        /// <summary>
        ///     Sets up internal data.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();



            // Line renderer setup

        }

        /// <summary>
        ///     Subscribe to avatar updated event.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            UxrManager.AvatarsUpdated += UxrManager_AvatarsUpdated;
        }

        /// <summary>
        ///     Unsubscribe from avatar updated event.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();

            UxrManager.AvatarsUpdated -= UxrManager_AvatarsUpdated;
        }

        #endregion

        #region Event Handling Methods

        /// <summary>
        ///     We update the laser after all VR avatars have been updated to make sure it's processed after all manipulation.
        /// </summary>
        private void UxrManager_AvatarsUpdated()
        {
            // Check if there is a hand grabbing the laser

            if (UxrGrabManager.Instance.GetGrabbingHand(_triggerGrabbable, 0, out UxrGrabber grabber))
            {
                // There is! see which hand to check for a trigger squeeze

                float trigger = UxrAvatar.LocalAvatarInput.GetInput1D(grabber.Side, UxrInput1D.Trigger);



                _triggerGrabbable.GetGrabPoint(0).GetGripPoseInfo(grabber.Avatar).PoseBlendValue = trigger;

                if (UxrAvatar.LocalAvatarInput.GetButtonsPress(grabber.Side, UxrInputButtons.Trigger))
                {
                    // Trigger is squeezed


                }
                else
                {
                    //_laserLineRenderer.enabled = false;
                }
            }
            else
            {
                //_laserLineRenderer.enabled = false;
            }

            // Check laser raycast



            if (_laserHaptics)
            {
                // _laserHaptics.enabled = _laserLineRenderer.enabled;

                if (grabber)
                {
                    _laserHaptics.HandSide = grabber.Side;
                }
            }


        }
    }

        #endregion



    
}