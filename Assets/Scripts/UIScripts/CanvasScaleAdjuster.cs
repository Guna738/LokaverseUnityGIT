using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleAdjuster : MonoBehaviour
{

    void Awake()
    {
        float smallestRatio = (float)1125 / (float)2436;
        float largestRatio = (float)1668/ (float)2388;

        float screenRatio = (float)Screen.height / (float)Screen.width;

        if (screenRatio < smallestRatio)
        {
            screenRatio = smallestRatio;
        }
        else if (screenRatio > largestRatio)
        {
            screenRatio = largestRatio;
        }

        //For the longest screen (smallest screenRatio), match should be 1
        //For the squarest screen (largest screenRatio), match should be 0

        float match = 1 - (screenRatio - smallestRatio) / (largestRatio - smallestRatio);        

        GetComponent<CanvasScaler>().matchWidthOrHeight = match;
    }

}
