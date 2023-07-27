using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeSettings : MonoBehaviour
{
    public TMP_InputField Name, Difficulty, SpectrumCreator, MusicCreator, BackgroundCreator, offset, BPM;
    public TMP_Dropdown P;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }  
    }
}
