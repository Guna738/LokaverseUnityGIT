using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        
    }


    void OnClick()
    {
        SoundManager.instance.Play(SoundManager.instance.buttonClick);
    }
}
