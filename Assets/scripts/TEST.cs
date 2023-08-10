using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private int a = 0;
    public ref int aa()
    {
        return ref a;
    }
    public void Start()
    {
        aa()=2;
        Debug.Log(a);
    }
}
