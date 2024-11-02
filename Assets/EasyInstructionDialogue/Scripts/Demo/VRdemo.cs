using UnityEngine;
using System.Collections;

public class VRdemo : MonoBehaviour
{
    [SerializeField] Transform UIPanel;
    float uiDistance = 2.2f, lerpRate = 0.05f;
    
    IEnumerator Start()
    {
        UIPanel.gameObject.SetActive(true);
        var pos = Camera.main.transform.position + (Camera.main.transform.forward * uiDistance);
        var rot = Quaternion.LookRotation(pos - Camera.main.transform.position);

        while (UIPanel.transform.position != pos)
        {
            UIPanel.position = Vector3.Lerp(UIPanel.position, pos, lerpRate);
            UIPanel.rotation = Quaternion.Slerp(UIPanel.rotation, rot, lerpRate);
            lerpRate += Time.deltaTime / 10;
            yield return null;
        }
        InstructionClass.options.ShowInstruction("0-3");
    }
}
