using UnityEngine;
using TMPro;
using System;

public class CUI01 : MonoBehaviour
{
    public TMP_InputField id;
    public ERSCommand.c01 RC()
    {
        return new ERSCommand.c01(Convert.ToInt32(id.text));
    }
}
