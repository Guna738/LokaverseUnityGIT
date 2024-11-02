using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInstruction",menuName ="InstrctionObject")]
public class InstructionObject : ScriptableObject
{
    public string InstructionName;
    [TextArea]
    public string[] Instructions;
}
