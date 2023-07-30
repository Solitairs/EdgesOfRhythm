using UnityEngine;
using TMPro;
using System;

public class CUI05 : MonoBehaviour
{
    public TMP_Dropdown choice;
    public ERSCommand.c05 RC()
    {
        bool t=true;
        if (choice.value == 0) t = false;
        return new ERSCommand.c05(t);
    }
}