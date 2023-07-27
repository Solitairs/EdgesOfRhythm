using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DataNote : MonoBehaviour
{
    //赋予每一个存储音符信息的text
    public int id;
    public TMP_InputField time, speed;
    public TMP_Dropdown rotate, type, hurt, road;
    public void delNote()
    {
        GameController.register.delNote(id);
        transform.parent.gameObject.GetComponent<setNotesInformations>().refresh();
    }
    public void OnTypeChanged()
    {
        GameController.register.notes[id].Type = type.value;
    }
    public void OnRotateChanged()
    {
        GameController.register.notes[id].rotateType = rotate.value;
    }
    public void OnHurtChanged()
    {
        GameController.register.notes[id].hurtType = hurt.value;
    }
    public void OnRoadChanged()
    {
        GameController.register.notes[id].deterRoad = road.value;
    }
    public void OnSpeedChanged()
    {
        GameController.register.notes[id].startSpeed = Convert.ToSingle(speed.text);
    }
    public void OnTimeChanged()
    {
        GameController.register.notes[id].deterTime = Convert.ToSingle(time.text);
    }
}
