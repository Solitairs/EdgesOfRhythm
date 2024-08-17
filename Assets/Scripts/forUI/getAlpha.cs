using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getAlpha : MonoBehaviour
{
    public static float Alpha=1;
    public delegate void AlphaChanged();
    public event AlphaChanged alphaChanged;
    public void OnAlphaSliderChanged()
    {
        Alpha = gameObject.GetComponent<Slider>().value;
        alphaChanged();
    }
    
}
