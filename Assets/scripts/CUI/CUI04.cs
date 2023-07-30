using UnityEngine;
using TMPro;
using System;

public class CUI04 : MonoBehaviour
{
    public TMP_InputField R, G, B, A, duringTime;
    public ERSCommand.c04 RC()
    {
        return new ERSCommand.c04(new Color(Convert.ToSingle(R.text), Convert.ToSingle(G.text), Convert.ToSingle(B.text), Convert.ToSingle(A.text)), Convert.ToSingle(duringTime.text));
    }
}