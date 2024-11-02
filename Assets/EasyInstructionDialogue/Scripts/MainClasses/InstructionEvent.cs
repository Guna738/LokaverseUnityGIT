using UnityEngine;
using UnityEngine.Events;

public class InstructionEvent : MonoBehaviour,IAttachment
{
    public UnityEvent OnInstructionDisplay,OnInstructionDisplayImmediate,OnInstructionPrev,OnInstructionNext,OnInstructionSkip,OnInstructionClose,OnInstructionCloseImmediate;

    public void AttachmentCall(int index){
        OnInstructionDisplay.Invoke();
    }
    public void AttachmentCallImmediate(int index){
        OnInstructionDisplayImmediate.Invoke();
    }
    public void AttachmentCallPrev(int index){
        OnInstructionPrev.Invoke();
    }
    public void AttachmentCallNext(int index){
        OnInstructionNext.Invoke();
    }
    public void AttachmentCallContinue(int index){
        OnInstructionSkip.Invoke();
    }
    public void AttachmentCallSkip(int index){
        OnInstructionClose.Invoke();
    }
    public void AttachmentCallContinueImmediate(int index){
        OnInstructionCloseImmediate.Invoke();
    }
}
