using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsController : MonoBehaviour
{
    public TMP_InputField BMP,dNum,DS;
    private void Start()
    {
        BMP.text = SpectralController.spectralData.BPM.ToString();
    }
    public void BPMChanged()
    {
        if (BMP.text=="" || Convert.ToInt32(BMP.text) <= 0)
        {
            BMP.text = SpectralController.spectralData.BPM.ToString();
            return;
        }
        SpectralController.spectralData.BPM = Convert.ToInt32(BMP.text);
    }
    public void DecidersChanged()
    {
        if (dNum.text == "" || Convert.ToInt32(dNum.text) <= 2)
        {
            dNum.text = SpectralController.spectralData.deciderNum.ToString();
            return;
        }
        SpectralController.spectralData.setDeciders(Convert.ToInt32(dNum.text),GameObject.FindGameObjectWithTag("SpectralController").GetComponent<SpectralController>().deciderPool);
    }
    public void SpeedChanged()
    {
        if (DS.text == "" || Convert.ToInt32(DS.text) <= 2)
        {
            DS.text = SpectralController.DefaultSpeed.ToString();
            return;
        }
        SpectralController.DefaultSpeed = Convert.ToInt32(DS.text);
    }
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
