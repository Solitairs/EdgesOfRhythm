using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChangeSettings : MonoBehaviour
{
    public TMP_InputField Name, Difficulty, SpectrumCreator, MusicCreator, BackgroundCreator, offset, BPM;
    public TMP_Dropdown P;
    private void Start()
    {
        Name.text = GameController.register.meta.Name;
        Difficulty.text = GameController.register.meta.Difficulty;
        SpectrumCreator.text = GameController.register.meta.SpectrumCreator;
        MusicCreator.text = GameController.register.meta.MusicCreator;
        BackgroundCreator.text = GameController.register.meta.BackgroundCreator;
        offset.text = GameController.register.meta.offset.ToString();
        BPM.text = GameController.register.meta.BPM.ToString();
        P.value = GameController.register.meta.PValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.register.meta.Name = Name.text;
            GameController.register.meta.Difficulty = Difficulty.text;
            GameController.register.meta.SpectrumCreator = SpectrumCreator.text;
            GameController.register.meta.MusicCreator = MusicCreator.text;
            GameController.register.meta.BackgroundCreator = BackgroundCreator.text;
            GameController.register.meta.offset=Convert.ToSingle(offset.text);
            GameController.register.meta.BPM = Convert.ToInt32(BPM.text);
            GameController.register.meta.PValue = P.value;
            GameController.register.meta.P = GameController.Pl[P.value];
            Destroy(gameObject);
        }  
    }
}
