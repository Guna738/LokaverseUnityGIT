using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SetupInstruction : MonoBehaviour
{
    void SetManager()
    {
        var obj = GameObject.Find("InstructionManager");
        var InstructionObject = obj != null ? obj : new GameObject("InstructionManager");

        if (!InstructionObject.GetComponent<InstructionClass>())
        {
            InstructionObject.AddComponent<InstructionClass>();
        }
        if (!InstructionObject.GetComponent<InstructionEvent>())
        {
            InstructionObject.AddComponent<InstructionEvent>();
        }
        SetReferences(InstructionObject);
        
    }

    void SetReferences(GameObject InstructionObject){
        InstructionObject.GetComponent<InstructionClass>().Instructionpanel = gameObject;
        InstructionObject.GetComponent<InstructionClass>().NextButton = transform.GetChild(0).GetChild(1).gameObject;
        InstructionObject.GetComponent<InstructionClass>().SkipButton = transform.GetChild(0).GetChild(2).gameObject;
        InstructionObject.GetComponent<InstructionClass>().Continuebutton = transform.GetChild(0).GetChild(3).gameObject;
        if(transform.GetChild(0).childCount > 4 && transform.GetChild(0).GetChild(4).GetComponent<Button>()){
            InstructionObject.GetComponent<InstructionClass>().UsePreviousButton = true;
            InstructionObject.GetComponent<InstructionClass>().PrevButton = transform.GetChild(0).GetChild(4).gameObject;
        }
        if(transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>()){
            InstructionObject.GetComponent<InstructionClass>().textToDisplay = transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        }else if (transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>()){
            InstructionObject.GetComponent<InstructionClass>().textToDisplay1 = transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        }
        //transform.parent = InstructionObject.transform;
    }
    void OnEnable()
    {
        var InScene = gameObject.scene.name.Equals(SceneManager.GetActiveScene().name);
        if (!InScene) { return; }

        SetManager();
        Debug.Log("To use any animation on the Canvas/Panel, ensure to set the appropraite Open/Close delay of the InstructionClass. Refer to the documentaion for more information");
        gameObject.SetActive(false);
        DestroyImmediate(this);
    }
}
