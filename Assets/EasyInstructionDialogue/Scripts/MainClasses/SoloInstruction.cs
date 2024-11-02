using UnityEngine;

public class SoloInstruction : MonoBehaviour
{
    public enum CallMethod
    {
        OnGameObjectEnabled,
        OnGameObjectDisabled, OnCollsionEnter, OnCollsionExit, OnTriggerEnter, OnTriggerExit,Custom
    }
    [TextArea]public string[] CustomInstructionText;
    [Tooltip("How you want the instrction to be called")]
    public CallMethod callMethod;
    [Tooltip("When disabled, the instruction would be called everytime the trigger, collider or call method of the object is activated")]
    [SerializeField]bool DisplayOnce = true;
    [Header("Tag of the object")][HideInInspector]public string ObjectTag = "Untagged";
    [Tooltip("Where to Input the instruction(s)")]
    bool Shown;
    void OnEnable()
    {
        if (callMethod == CallMethod.OnGameObjectEnabled && !Shown)
        {
            Invoke(nameof(CallSoloInstruction),0.1f);
            Shown = true;
        }
    }
    void OnDisable()
    {
        if (callMethod == CallMethod.OnGameObjectDisabled && !Shown)
        {
            CallSoloInstruction();
            Shown = true;
        }
        if(!DisplayOnce){Invoke(nameof(EnabledShow),2);}
    }
    void EnabledShow(){
        Shown = false;
    }
    void OnCollisionEnter(Collision c)
    {
        if (!(c.collider.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnCollsionEnter && !Shown)
        {
            CallSoloInstruction();
            Shown = true;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (!(c.collider.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnCollsionExit && !Shown)
        {
            
            CallSoloInstruction();
            Shown = true;
        }
        if(!DisplayOnce){Invoke(nameof(EnabledShow),2);}
    }
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("1");

        if (!(c.CompareTag(ObjectTag)))
        {
            Debug.Log("2");
            return;
        }
        if (callMethod == CallMethod.OnTriggerEnter && !Shown)
        {
            Debug.Log("3");
            CallSoloInstruction();
            Shown = true;
        }
    }
    void OnTriggerExit(Collider t)
    {
        if (!(t.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnTriggerExit && !Shown)
        {
            CallSoloInstruction();
            Shown = true;
        }

        if (callMethod == CallMethod.OnTriggerEnter)
        {
            InstructionClass.options.ContinueCall(1);
            
        }
        if (!DisplayOnce){Invoke(nameof(EnabledShow),2);}
    }

    void CallSoloInstruction()
    {
        if(CustomInstructionText.Length == 0) return;
        InstructionClass.options.SetUpInstructions(null,InstructionClass.InstructionType.CustomInstruction,CustomInstructionText);
		InstructionClass.options.ShowInstruction(0,CustomInstructionText.Length-1);
    }
    public void ShowInstruction(){
        if(callMethod == CallMethod.Custom){
            CallSoloInstruction();
        }
    }
}
