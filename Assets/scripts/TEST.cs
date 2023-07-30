using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) Debug.Log("1");
        if (Input.GetKeyDown(KeyCode.DownArrow)) Debug.Log("2");
        if (Input.GetKeyDown(KeyCode.RightArrow)) Debug.Log("4");
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Debug.Log("3");
    }
}
