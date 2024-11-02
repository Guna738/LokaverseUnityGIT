using UnityEngine;
using System.Collections;

public class EventDemo : MonoBehaviour
{
    [SerializeField] GameObject[] Objects;
    float RotRate = 0.05f;

    bool AnimateSphere;
    float mvt;

    public GameObject characterObj;
    public InstructionClass instructionClass;


   // private bool IsRotating = false;
    IEnumerator Start()
    {
       
        yield return new WaitForSeconds(1.5f);
        InstructionClass.options.ShowInstruction("0-6");
    }
    void Update()
    {
        if (AnimateSphere)
        {
            mvt += Time.deltaTime/2;
          //  SphereObject.Translate(0, Mathf.Cos(mvt * 15) / 4, 0);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
           
            instructionClass.ContinueCall(1);
            ResetCamera();

        }

       
    }
    public void SetSphereAnim(bool value){
        AnimateSphere = value;
    }
    public void RotatePlayer()
    {
        int Index = InstructionClass.options.CurrentIndex - 1;
        print("Index is" + Index);
        if (Index < 0) { return; }
        RotRate = 0.05f;
        StopAllCoroutines();
      //  StartCoroutine(Rotate(Objects[Index]));
    }
   
    //IEnumerator Rotate(GameObject obj)
    //{
    //    var rot = (obj.transform.position - characterObj.transform.position);
    //    var Quat = Quaternion.LookRotation(rot, Vector3.up);

    //    while (characterObj.transform.rotation != Quat)
    //    {
    //    //    IsRotating = true;
    //        print("true");
    //        characterObj.transform.rotation = Quaternion.Slerp(characterObj.transform.rotation, Quat, RotRate);
    //        RotRate += Time.deltaTime;
    //        yield return null;
    //        print("false");
    //    }
       
    //}

    public void ResetCamera()
    {
        print("Quaterneon called");
        characterObj.transform.localRotation = Quaternion.identity;


    }

}
