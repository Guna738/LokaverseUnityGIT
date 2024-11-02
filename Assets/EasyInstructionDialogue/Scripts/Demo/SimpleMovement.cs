using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SmoothMouseLook))]
public class SimpleMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField]float movespeed = 2;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var moveDir = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(moveDir * movespeed * Time.deltaTime);
        Camera.main.transform.position = transform.position + new Vector3(0,0.3f,0);
        transform.localEulerAngles = 
        new Vector3(transform.localEulerAngles.x,Camera.main.transform.localEulerAngles.y,transform.localEulerAngles.z);
    }
}
