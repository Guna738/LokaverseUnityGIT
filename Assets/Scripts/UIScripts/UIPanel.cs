using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPanel : MonoBehaviour
{
    public bool ShowOnEnable = false;

    public UnityEvent OnShowStart;
    public UnityEvent OnShowEnd;
    public UnityEvent OnHideStart;
    public UnityEvent OnHideEnd;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        if(ShowOnEnable) Show();
    }

    public void SetVisible(bool visible)
    {
        if (visible) Show();
        else Hide();
    }

    public void Show(float time = .25f, float delay = 0)
    {
        gameObject.SetActive(true);

        StopAllCoroutines();
        OnShowStart.Invoke();
        StartCoroutine(ChangeAlphaCoroutine(1, time, delay, true, OnShowEnd));
    }

    public void Hide(float time = .25f, float delay = 0)
    {
        if (!gameObject.activeSelf) return;

        StopAllCoroutines();
        OnHideStart.Invoke();
        StartCoroutine(ChangeAlphaCoroutine(0, time, delay, false, OnHideEnd));
    }

    private IEnumerator ChangeAlphaCoroutine(float value, float time = .25f, float delay = 0, bool blockRaycast = true, UnityEvent endEvent = null)
    {
        yield return new WaitForSecondsRealtime(delay);
        float t = 0;
        float startValue = canvasGroup.alpha;
        AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startValue, value, curve.Evaluate(t/time));
            yield return null;
        }
        canvasGroup.alpha = value;
        canvasGroup.blocksRaycasts = blockRaycast;
        if(endEvent != null) endEvent.Invoke();
        yield return null;
    }

}
