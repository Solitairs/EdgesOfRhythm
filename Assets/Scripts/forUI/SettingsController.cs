using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsController : MonoBehaviour
{
    public TMP_InputField BMP,dNum;
    private SpectralController SC;
    private void Start()
    {
        SC = GameObject.Find("SpectralController").GetComponent<SpectralController>();
        BMP.text = SC.spectralData.BPM.ToString();
    }
    public void BPMChanged()
    {
        if (BMP.text=="" || Convert.ToInt32(BMP.text) <= 0)
        {
            BMP.text = SC.spectralData.BPM.ToString();
            return;
        }
        SC.spectralData.BPM = Convert.ToInt32(BMP.text);
    }
    public void DecidersChanged()
    {
        if (dNum.text == "" || Convert.ToInt32(dNum.text) <= 2)
        {
            dNum.text = SC.spectralData.deciderNum.ToString();
            return;
        }
        SC.spectralData.setDeciders(Convert.ToInt32(dNum.text));
    }
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
