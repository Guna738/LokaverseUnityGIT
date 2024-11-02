public interface IAttachment
{
    void AttachmentCall(int index);
    void AttachmentCallImmediate(int index);
    void AttachmentCallPrev(int index);
    void AttachmentCallNext(int index);
    void AttachmentCallSkip(int index);
    void AttachmentCallContinue(int index);
    void AttachmentCallContinueImmediate(int index);
}
