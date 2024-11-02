using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;   // You may remove this if you are not using TextMeshPro
public class InstructionClass : MonoBehaviour
{
    public static InstructionClass options;
    public enum InstructionType { InstructionObject, CustomInstruction }
    public enum TextAnimationType { None, TypeIn, TypeInOut, TypeInFadeOut, FadeInOut }
    public enum TextType { TextMeshPro, DefaultUIText }
    [Header("Panel UIs Reference")]
    public GameObject Instructionpanel;
    public GameObject Continuebutton, SkipButton, NextButton;

    [Header("Others")]
    [HideInInspector] public TextType textType;
    [HideInInspector] public Text textToDisplay;
    [HideInInspector] public TextMeshProUGUI textToDisplay1;
    [HideInInspector] public InstructionType instructionType;
    [HideInInspector] public InstructionObject instructionObject;
    [HideInInspector] public TextAnimationType TextanimationType;
    [HideInInspector] public float TypeSpeed = 10, fadeRate = 1.2f, AutoNextDelay, OpenDelay, CloseDelay;
    //Non serialized variables
    [HideInInspector] public bool showAdvanced, AutoNext, persistBetweenScence; //For the editorScript
    [HideInInspector] public GameObject PrevButton;
    [HideInInspector] public bool UsePreviousButton, AnimatedPanel = true; //For the editorScript
    [HideInInspector] public int selectedTextType, selectedInstrType, selectedAnimType = 2; //For the editorScript
    Button nextButtonUI, prevButtonUI, continueButtonUI, SkipButtonUI;
    [HideInInspector] public int startingIndex, CurrentIndex, indextoreach;
    int? previousIndex = null;
    bool isInstructing = false, UseInstructionEvent, HideContinue;
    [Header("Instruction Object To Display")]
    string[] StringToDisplay;
    InstructionEvent eventHolder;
    void Awake()
    {
        ManageScenePersistence();
        SetupImmediate();
    }
    void ManageScenePersistence(){
        if (persistBetweenScence)
        {
            if (options != null && options != this)
            {
                Destroy(gameObject);
            }
            else
            {
                options = this;
                DontDestroyOnLoad(gameObject);
            }

        }
        else { options = this; }
    }
    void SetupImmediate(){
        Instructionpanel.SetActive(false);
        if (instructionObject != null) { SetUpInstructions(); }
        if (GetComponent<InstructionEvent>()) { eventHolder = GetComponent<InstructionEvent>(); }
        UsePreviousButton = PrevButton != null;
        if (textToDisplay) { textToDisplay1 = null; }
        else { textToDisplay = null; }
        if (!AnimatedPanel) { OpenDelay = 0; CloseDelay = 0; }
    }
    public void SetUpInstructions(InstructionObject instObj = null, InstructionType Type = InstructionType.InstructionObject, string[] customInstruction = null)
    {
        if (instObj != null)
        {
            instructionObject = instObj;
        }

        if (Type == InstructionType.InstructionObject)
        {
            StringToDisplay = instructionObject.Instructions;
        }
        else
        {
            StringToDisplay = customInstruction;
        }
        Instructionpanel.SetActive(false);
    }

    public void ShowInstruction(int InstructionIndex)
    {
        ShowInstruction(InstructionIndex, 0);
    }
    public void ShowInstruction(string InstructionPath)
    {
        var indexes = InstructionPath.Split('-');
        int start = -1, end = -1;
        int.TryParse(indexes[0], out start);
        int.TryParse(indexes[1], out end);
        if (start >= 0 && end >= start)
            ShowInstruction(start, end);
    }
    public void ShowInstruction(int InstructionIndex, int StopIndex = 0)
    {
        if (StopIndex == 0) { StopIndex = InstructionIndex; }

        isInstructing = true;
        startingIndex = InstructionIndex;
        CurrentIndex = startingIndex;
        indextoreach = StopIndex;

        Instructionpanel.SetActive(true);
        StartCoroutine(SetUp(OpenDelay));
        if (!NextButton.GetComponent<Button>() || (UsePreviousButton && !PrevButton.GetComponent<Button>()) || !SkipButton.GetComponent<Button>() || !Continuebutton.GetComponent<Button>())
        {
            Debug.LogError("Error! One (or more) of Next,Previous,Skip or Continue button(s) Object assigned in the inspector does not have a button component");
            return;
        }

        nextButtonUI = NextButton.GetComponent<Button>();
        SkipButtonUI = SkipButton.GetComponent<Button>();
        continueButtonUI = Continuebutton.GetComponent<Button>();
        if (UsePreviousButton) { prevButtonUI = PrevButton.GetComponent<Button>(); }

        nextButtonUI.onClick.RemoveAllListeners();
        nextButtonUI.onClick.AddListener(NextWord);

        if (UsePreviousButton)
        {
            prevButtonUI.onClick.RemoveAllListeners();
            prevButtonUI.onClick.AddListener(PrevWord);
        }

        SkipButtonUI.onClick.RemoveAllListeners();
        SkipButtonUI.onClick.AddListener(() => { ContinueCall(1); });

        continueButtonUI.onClick.RemoveAllListeners();
        continueButtonUI.onClick.AddListener(() => { ContinueCall(); });

        Continuebutton.SetActive(false);
        NextButton.SetActive(false);
        SkipButton.SetActive(false);
        if (UsePreviousButton) { PrevButton.SetActive(false); }


        if (CurrentIndex == indextoreach)
        {
            Continuebutton.SetActive(true);
        }
        else
        {
            SkipButton.SetActive(true);
            if (!AutoNext)
            {
                NextButton.SetActive(true);
            }
        }
    }

    string getText()
    {
        if (textType == TextType.TextMeshPro)
        {
            return textToDisplay1.text;
        }
        else { return textToDisplay.text; }
    }
    IEnumerator SetUp(float Delay)
    {
        UseAttachmentEvent("ShowImmediate");
        yield return new WaitForSeconds(Delay);
        if (TextanimationType == TextAnimationType.None)
        {
            if (textToDisplay) { textToDisplay.text = StringToDisplay[CurrentIndex]; }
            else { textToDisplay1.text = StringToDisplay[CurrentIndex]; }
        }
        else
        {
            AnimateText();
        }
        UseAttachmentEvent("Show");

    }

    void AnimateText()
    {
        StopAllCoroutines();
        switch (TextanimationType)
        {
            case TextAnimationType.TypeIn:
                StartCoroutine(TypeIn());
                break;
            case TextAnimationType.TypeInOut:
                if (previousIndex != null) { StartCoroutine(TypeOut(previousIndex.Value)); }
                else { StartCoroutine(TypeIn()); }
                break;
            case TextAnimationType.TypeInFadeOut:
                if (previousIndex != null) { StartCoroutine(FadeOut()); }
                else { StartCoroutine(TypeIn()); }
                break;
            case TextAnimationType.FadeInOut:
                if (previousIndex != null) { StartCoroutine(FadeOut()); }
                else { StartCoroutine(FadeIn()); }
                break;
            default:
                break;
        }
    }
    #region type      
    IEnumerator TypeIn()
    {
        if (textToDisplay) { textToDisplay.text = ""; }
        else { textToDisplay1.text = ""; }
        //NextButton.SetActive(false);
        var tpspeed = TypeSpeed > 10 ? 10 : 11 - TypeSpeed;
        foreach (char letter in StringToDisplay[CurrentIndex].ToCharArray())
        {
            if (textToDisplay) { textToDisplay.text += letter; }
            else { textToDisplay1.text += letter; }

            yield return new WaitForSeconds(Time.deltaTime * tpspeed);
        }

        if (getText() == StringToDisplay[CurrentIndex])
        {
            FinishedAnimatingIn();
        }

    }
    IEnumerator TypeOut(int previousIndex)
    {
        var str = StringToDisplay[previousIndex];
        var charlist = str.ToCharArray().ToList();

        for (int i = 0; i < str.Length; i++)
        {
            if (charlist.Count > 0)
            {
                charlist.RemoveAt(charlist.Count - 1);
                if (textToDisplay) { textToDisplay.text = new string(charlist.ToArray()); }
                else { textToDisplay1.text = new string(charlist.ToArray()); }
            }
            yield return null;
        }

        if (getText() == "")
        {
            StartCoroutine(TypeIn());
        }

    }
    #endregion
    IEnumerator FadeIn()
    {
        if (textToDisplay)
        {
            var col = textToDisplay.color;
            float alpha = 0;
            textToDisplay.color = new Color(col.r, col.g, col.b, alpha);
            textToDisplay.text = StringToDisplay[CurrentIndex];
            while (textToDisplay.color.a < 1)
            {
                textToDisplay.color = new Color(col.r, col.g, col.b, alpha);
                alpha += fadeRate * Time.deltaTime;
                yield return null;
            }
            if (textToDisplay.color.a >= 1)
            {
                FinishedAnimatingIn();
            }
        }
        else
        {
            var col = textToDisplay1.color;
            float alpha = 0;
            textToDisplay1.color = new Color(col.r, col.g, col.b, alpha);
            textToDisplay1.text = StringToDisplay[CurrentIndex];
            while (textToDisplay1.color.a < 1)
            {
                textToDisplay1.color = new Color(col.r, col.g, col.b, alpha);
                alpha += fadeRate * Time.deltaTime;
                yield return null;
            }
            if (textToDisplay1.color.a >= 1)
            {
                FinishedAnimatingIn();
            }
        }
    }
    IEnumerator FadeOut()
    {
        if (textToDisplay)
        {
            var col = textToDisplay.color;
            float alpha = 1;
            textToDisplay.color = new Color(col.r, col.g, col.b, alpha);
            while (textToDisplay.color.a > 0)
            {
                textToDisplay.color = new Color(col.r, col.g, col.b, alpha);
                alpha -= fadeRate * Time.deltaTime;
                yield return null;
            }
            if (textToDisplay.color.a <= 0)
            {
                if (TextanimationType == TextAnimationType.TypeInFadeOut)
                {
                    textToDisplay.color = new Color(col.r, col.g, col.b, 1);
                    StartCoroutine(TypeIn());
                }
                else { StartCoroutine(FadeIn()); }

            }
        }
        else
        {
            var col = textToDisplay1.color;
            float alpha = 1;
            textToDisplay1.color = new Color(col.r, col.g, col.b, alpha);
            while (textToDisplay1.color.a > 0)
            {
                textToDisplay1.color = new Color(col.r, col.g, col.b, alpha);
                alpha -= fadeRate * Time.deltaTime;
                yield return null;
            }
            if (textToDisplay1.color.a <= 0)
            {
                if (TextanimationType == TextAnimationType.TypeInFadeOut)
                {
                    textToDisplay1.color = new Color(col.r, col.g, col.b, 1);
                    StartCoroutine(TypeIn());
                }
                else { StartCoroutine(FadeIn()); }

            }
        }
    }
    void FinishedAnimatingIn()
    {
        if (AutoNext)
        {
            if (CurrentIndex != indextoreach)
                StartCoroutine(CallNextWord());
        }

    }
    IEnumerator CallNextWord()
    {
        yield return new WaitForSeconds(AutoNextDelay);
        NextWord();
    }
    public void NextWordCall()
    {
        if (NextButton.activeInHierarchy)
            NextWord();
    }
    void NextWord()
    {
        if (CurrentIndex == startingIndex)
        {
            if (UsePreviousButton && !AutoNext) { PrevButton.SetActive(true); }
        }
        previousIndex = CurrentIndex;
        CurrentIndex += 1;
        UseAttachmentEvent("Next");
        StartCoroutine(SetUp(0));
        if (CurrentIndex == indextoreach)
        {
            NextButton.SetActive(false);
            SkipButton.SetActive(false);
            Continuebutton.SetActive(true);
        }

    }
    public void PrevWordCall()
    {
        if (PrevButton.activeInHierarchy)
            PrevWord();
    }
    public void PrevWord()
    {
        if (CurrentIndex == indextoreach)
        {
            NextButton.SetActive(true);
            SkipButton.SetActive(true);
            Continuebutton.SetActive(false);
        }
        previousIndex = CurrentIndex;
        CurrentIndex -= 1;
        UseAttachmentEvent("Prev");
        StartCoroutine(SetUp(0));
        if (CurrentIndex == startingIndex)
        {
            PrevButton.SetActive(false);
        }

    }
    public void ContinueCall(int skip = 0)
    {
        StartCoroutine(ContinueOnClick(skip));
    }
    IEnumerator ContinueOnClick(int skip)
    {
        UseAttachmentEvent("CloseImmediate");
        yield return new WaitForSeconds(CloseDelay);
        Instructionpanel.SetActive(false);
        isInstructing = false;
        Continuebutton.SetActive(false);
        if (textToDisplay) { textToDisplay.text = ""; }
        else { textToDisplay1.text = ""; }
        if (skip == 1) { UseAttachmentEvent("Skip"); }
        else { UseAttachmentEvent("Continue"); }
        previousIndex = null;
        if (instructionType == InstructionType.CustomInstruction && instructionObject != null)
        {
            StringToDisplay = instructionObject.Instructions;
            StringToDisplay = instructionObject.Instructions;
        }
        StopAllCoroutines();
    }

    void DisableInstruction()
    {
        Instructionpanel.SetActive(false);
        isInstructing = false;
    }

    void UseAttachmentEvent(string Type)
    {
        Type = Type.ToUpper();
        if (!eventHolder) return;
        var attach = eventHolder.GetComponent<IAttachment>();

        switch (Type)
        {
            case "SHOW":
                attach.AttachmentCall(CurrentIndex);
                break;
            case "SHOWIMMEDIATE":
                attach.AttachmentCallImmediate(CurrentIndex);
                break;
            case "NEXT":
                attach.AttachmentCallNext(CurrentIndex);
                break;
            case "PREV":
                attach.AttachmentCallPrev(CurrentIndex);
                break;
            case "SKIP":
                attach.AttachmentCallSkip(CurrentIndex);
                break;
            case "CONTINUE":
                attach.AttachmentCallContinue(CurrentIndex);
                break;
            case "CLOSEIMMEDIATE":
                attach.AttachmentCallContinueImmediate(CurrentIndex);
                break;
            default:
                break;
        }
    }
}


