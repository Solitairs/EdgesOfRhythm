using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateCmd : MonoBehaviour
{
    public TMP_InputField time;
    public TMP_Dropdown type;
    public UnityEngine.Object[] CmdTypes;
    private GameObject ChildType;
    public void Start()
    {
        float time = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().time;
        time = time / (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        time = MathF.Round(time);
        time = time * (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        this.time.text = time.ToString();
        ChildType = Instantiate(CmdTypes[0], transform) as GameObject;
    }
    public void moveTime()
    {
        float time = Convert.ToSingle(this.time.text);
        time = time / (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        time = MathF.Round(time);
        time = time * (60 / GameController.register.meta.BPM / GameController.register.meta.P);
        this.time.text = time.ToString();
    }
    public void OnTypeChanged()
    {
        Destroy(ChildType);
        ChildType = Instantiate(CmdTypes[type.value], transform) as GameObject;
    }
    public void addCmdNow()
    {
        moveTime();
        switch (type.value+1)
        {
            case 1:
                CUI01 c1=ChildType.GetComponent<CUI01>();
                GameController.cmd.addx01(Convert.ToSingle(time), c1.RC());
                break;
            case 2:
                CUI02 c2 = ChildType.GetComponent<CUI02>();
                GameController.cmd.addx02(Convert.ToSingle(time), c2.RC());
                break;
            case 3:
                CUI03 c3 = ChildType.GetComponent<CUI03>();
                GameController.cmd.addx03(Convert.ToSingle(time), c3.RC());
                break;
            case 4:
                CUI04 c4 = ChildType.GetComponent<CUI04>();
                GameController.cmd.addx04(Convert.ToSingle(time), c4.RC());
                break;
            case 5:
                CUI05 c5 = ChildType.GetComponent<CUI05>();
                GameController.cmd.addx05(Convert.ToSingle(time), c5.RC());
                break;
            case 6:
                CUI06 c6 = ChildType.GetComponent<CUI06>();
                GameController.cmd.addx06(Convert.ToSingle(time), c6.RC());
                break;
            case 7:
                CUI07 c7 = ChildType.GetComponent<CUI07>();
                GameController.cmd.addx07(Convert.ToSingle(time), c7.RC());
                break;
        }
    }
}
