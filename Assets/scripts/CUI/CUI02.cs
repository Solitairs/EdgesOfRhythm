using UnityEngine;
using TMPro;
using System;

public class CUI02 : MonoBehaviour
{
    public TMP_InputField id,speed;
    public ERSCommand.c02 RC()
    {
        return new ERSCommand.c02(Convert.ToInt32(id.text), Convert.ToSingle(speed.text));
    }
}
