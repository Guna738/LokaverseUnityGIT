using System.Collections;
using System.Collections.Generic;
using NOT_Lonely;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] NL_OverheadCrane overheadCrane;
    private KeyboardAction inputActions;
    [SerializeField] private AudioSource source;
    private bool playMusic = false;
    bool m_ToggleChange = true;

    private void Start()
    {
        inputActions = new KeyboardAction();
        inputActions.Enable();
        inputActions.CraneMovement.Attach.performed += HookMovement_performedUp;
    }

    private void Update()
    {

        MoveCrane();
        MoveHook();
       
       
    }

    private void HookMovement_performedUp(InputAction.CallbackContext obj)
    {
        Debug.Log("1 Pressed" +obj.ToString());
     
    }

    void MoveHook()
    {
        Vector2 hookMovement = inputActions.CraneMovement.HookMovement.ReadValue<Vector2>();
        if (hookMovement.y == 1)
        {

            overheadCrane.MoveHookUp();
        }
        else
        if (hookMovement.y == -1)
        {
            overheadCrane.MoveHookDown();
        }
    }

    void MoveCrane()
    {
        Vector2 hookMovement = inputActions.CraneMovement.HookMovement.ReadValue<Vector2>();
        Debug.Log("Hook is" +hookMovement);
        Vector2 vector2 = inputActions.CraneMovement.Move.ReadValue<Vector2>();
        if (vector2.x == 1)
        {
            overheadCrane.MoveCraneRight();
            playMusic = true;
           

        }
        if (vector2.x == -1)
        {
            overheadCrane.MoveCraneLeft();
            playMusic = true;


        }

        if (vector2.y == 1)
        {
            overheadCrane.MoveCraneForward();
            playMusic = true;


        }
        if (vector2.y == -1)
        {
            overheadCrane.MoveCraneBackward();
            playMusic = true;

        }
        Vector2 myVecZero = Vector2.zero;
        if (vector2 == myVecZero)
        {
          
            source.Stop();
            playMusic = false;
            m_ToggleChange = true;
        }
        if (playMusic == true && m_ToggleChange == true)
        {
            //Stop the audio
            source.Play();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }
    }

   
}
