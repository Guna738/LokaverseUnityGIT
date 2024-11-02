using System.Collections;
using System.Collections.Generic;
using UltimateXR.Manipulation;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class PushButton : MonoBehaviour
{
    private bool shouldPlay = true;
    public UxrGrabbableObject _grabbableObject;
    public Transform m_Button;
    //private Vector3 newPosition;

    private void Start()
    {
        //newPosition = m_Button.localPosition;
        Debug.Log("y Pos is" + m_Button.localPosition.y);
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        _grabbableObject.Grabbing += GrabbableObject_Grabbing;
        _grabbableObject.Grabbed += GrabbableObject_Grabbed;
        _grabbableObject.Releasing += GrabbableObject_Releasing;
        _grabbableObject.Released += GrabbableObject_Released;
        _grabbableObject.Placing += GrabbableObject_Placing;
        _grabbableObject.Placed += GrabbableObject_Placed;
        _grabbableObject.ConstraintsApplying += GrabbableObject_ConstraintsApplying;
        _grabbableObject.ConstraintsApplied += GrabbableObject_ConstraintsApplied;
        _grabbableObject.ConstraintsFinished += GrabbableObject_ConstraintsFinished;
    }

    private void OnDisable()
    {
        _grabbableObject.Grabbing -= GrabbableObject_Grabbing;
        _grabbableObject.Grabbed -= GrabbableObject_Grabbed;
        _grabbableObject.Releasing -= GrabbableObject_Releasing;
        _grabbableObject.Released -= GrabbableObject_Released;
        _grabbableObject.Placing -= GrabbableObject_Placing;
        _grabbableObject.Placed -= GrabbableObject_Placed;
        _grabbableObject.ConstraintsApplying -= GrabbableObject_ConstraintsApplying;
        _grabbableObject.ConstraintsApplied -= GrabbableObject_ConstraintsApplied;
        _grabbableObject.ConstraintsFinished -= GrabbableObject_ConstraintsFinished;
    }
    private void GrabbableObject_Grabbing(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} is about to be grabbed by avatar {e.Grabber.Avatar.name}");
    }

    private void GrabbableObject_Grabbed(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} was grabbed by avatar {e.Grabber.Avatar.name}");
       

    }

    private void GrabbableObject_Releasing(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} is about to be released by avatar {e.Grabber.Avatar.name}");
    }

    private void GrabbableObject_Released(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} was released by avatar {e.Grabber.Avatar.name}");
        Debug.Log("y Pos is" + m_Button.localPosition.y);
        ToggleSound();
    }

    private void GrabbableObject_Placing(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} is about to be placed on anchor {e.GrabbableAnchor.name} by avatar {e.Grabber.Avatar.name}");
    }

    private void GrabbableObject_Placed(object sender, UxrManipulationEventArgs e)
    {
        Debug.Log($"Object {e.GrabbableObject.name} was placed on anchor {e.GrabbableAnchor.name} by avatar {e.Grabber.Avatar.name}");
        Debug.Log("y Pos is" + m_Button.localPosition.y);
        ToggleSound();
    }

    private void GrabbableObject_ConstraintsApplying(object sender, UxrApplyConstraintsEventArgs e)
    {
        Debug.Log($"Object {_grabbableObject.name} is about to be constrained (if required)");
        Debug.Log("y Pos is" + m_Button.localPosition.y);
    }

    private void GrabbableObject_ConstraintsApplied(object sender, UxrApplyConstraintsEventArgs e)
    {
        Debug.Log($"Object {_grabbableObject.name} was constrained and can now be constrained using user specific code");
        Debug.Log("y Pos is" + m_Button.localPosition.y);
        ToggleSound();
    }

    private void GrabbableObject_ConstraintsFinished(object sender, UxrApplyConstraintsEventArgs e)
    {
        Debug.Log($"All constraints on object {_grabbableObject.name} were applied");
        Debug.Log("y Pos is" + m_Button.localPosition.y);
      
    }

    void ToggleSound()
    {

        if (m_Button.localPosition.y <= -0.0195)
        {
            this.GetComponent<PlayQuickSound>().Play();
        }
        else
        {
            this.GetComponent<PlayQuickSound>().Stop();
        }
    
    }
}
