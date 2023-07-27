using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UIElements;

public class setNotesInformations : MonoBehaviour
{
    public Object text;
    void Start()
    {
        ERSRegister register = GameController.register;
        register.sort();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, register.noteNum * 20 + 2);
        for (int i = 0; i < register.noteNum; i++)
        {
            GameObject temp = Instantiate(text, transform) as GameObject;
            DataNote data = temp.GetComponent<DataNote>();
            data.rotate.value = register.notes[i].rotateType;
            data.time.text = register.notes[i].deterTime.ToString();
            data.speed.text = register.notes[i].startSpeed.ToString();
            data.road.value = register.notes[i].deterRoad;
            data.hurt.value = register.notes[i].hurtType;
            data.type.value = register.notes[i].Type;
            data.id = i;
            string t = "";
            for (int j = 0; j < 7 - i.ToString().Length; j++)
            {
                t += " ";
            }
            temp.GetComponent<TextMeshProUGUI>().text = "                                                           ID:" + i.ToString() + t + "Time:                                                                                                    Speed:";
        }
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
    public void refresh()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        ERSRegister register = GameController.register;
        register.sort();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, register.noteNum * 20 + 2);
        for (int i = 0; i < register.noteNum; i++)
        {
            GameObject temp = Instantiate(text, transform) as GameObject;
            DataNote data = temp.GetComponent<DataNote>();
            data.rotate.value = register.notes[i].rotateType;
            data.time.text = register.notes[i].deterTime.ToString();
            data.speed.text = register.notes[i].startSpeed.ToString();
            data.road.value = register.notes[i].deterRoad;
            data.hurt.value = register.notes[i].hurtType;
            data.type.value = register.notes[i].Type;
            data.id = i;
            string t = "";
            for(int j=0; j<7-i.ToString().Length; j++)
            {
                t += " ";
            }
            temp.GetComponent<TextMeshProUGUI>().text = "                                                           ID:"+ i.ToString()+t+"Time:                                                                                                    Speed:";
        }
    }
}
