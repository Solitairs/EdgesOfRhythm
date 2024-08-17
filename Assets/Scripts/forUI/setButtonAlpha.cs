using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setButtonAlpha : MonoBehaviour
{
    Image myImage;
    private void Start()
    {
        myImage=gameObject.GetComponent<Image>();
        GameObject.Find("AlphaSlider").GetComponent<getAlpha>().alphaChanged += changeAlpha;
    }
    public void changeAlpha()
    {
        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, getAlpha.Alpha);
    }
}
