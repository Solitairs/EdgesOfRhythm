using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreateNote : MonoBehaviour
{
    public TMP_InputField deterTime, StartSpeed;
    public TMP_Dropdown rotateType, Type, hurtType, deterRoad;
    public void Start()
    {
        //У׼
        float time = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().time;
        time = time / (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        time = MathF.Round(time);
        time = time * (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        deterTime.text = time.ToString();

    }
    public void moveTime()
    {
        float time=Convert.ToInt32(deterTime.text);
        time = time / (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        time = MathF.Round(time);
        time = time * (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        deterTime.text = time.ToString();
    }
    public void addNoteNow()
    {
        if (GameController.SaveAgain) GameController.register.delNote(GameController.register.noteNum - 1);
        GameController.register.addNote(new ERSRegister.Notes(Type.value, rotateType.value, hurtType.value, deterRoad.value, Convert.ToSingle(StartSpeed.text), Convert.ToSingle(deterTime.text)));
        GameController.SaveAgain = true;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
        {
            moveTime();
            addNoteNow();
        }
    }
}
