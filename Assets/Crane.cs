using System.Collections;
using System.Collections.Generic;
using NOT_Lonely;
using UnityEngine;

public class Crane : MonoBehaviour
{
    [SerializeField] NL_OverheadCrane crane;
    [SerializeField] private Transform StickTransform;
    [SerializeField] private Transform LeverTransform;
    Vector2 m_JoystickValue;
    Vector2 m_LeverValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_JoystickValue.x = WrapAngle(StickTransform.localEulerAngles.x);
        m_JoystickValue.y = WrapAngle(StickTransform.localEulerAngles.z);

        m_LeverValue.x = WrapAngle(LeverTransform.localEulerAngles.x);
     
       // Debug.Log("Lever Val is " + m_LeverValue);

        crane.MoveCraneForward(-m_JoystickValue.x);
        crane.MoveCraneLeft(-m_JoystickValue.y);

        if (m_LeverValue.x <= -10)
        {
            crane.MoveHookUp();
        }

        if (m_LeverValue.x >= 10)
        {
            crane.MoveHookDown();
        }
       
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }
}
