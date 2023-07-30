using UnityEngine;
using TMPro;
using System;

public class CUI06 : MonoBehaviour
{
    public TMP_InputField duringTime;
    public ERSCommand.c06 RC()
    {
        return new ERSCommand.c06(Convert.ToSingle(duringTime.text));
    }
}