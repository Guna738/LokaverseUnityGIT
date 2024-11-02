using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInfo : MonoBehaviour
{
    public GameObject newInfo;

    private void OnEnable()
    {
        newInfo.SetActive(true);
    }
    private void OnDisable()
    {
        newInfo.SetActive(false);
    }
    // Update is called once per frame
   
}
