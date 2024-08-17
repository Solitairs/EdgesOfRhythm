using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height,true);
        transform.position=new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).y);
    }
}
