using UnityEngine;
using TMPro;
using System;

public class CUI07 : MonoBehaviour
{
    public TMP_InputField duringTime;
    public ERSCommand.c07 RC()
    {
        return new ERSCommand.c07(Convert.ToSingle(duringTime.text));
    }
}