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
        ERSRegister register = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().register;
        register.sort();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, register.noteNum * 20 + 2);
        string color = "", rotate = "", hurt = "", road = "", speed = "";
        for(int i=0;i< register.noteNum; i++)
        {
            GameObject temp = Instantiate(text, transform) as GameObject;
            DataNote data=temp.GetComponent<DataNote>();
            data.rotate.value = register.notes[i].angleType;
            data.time.text = register.notes[i].deterTime.ToString();
            data.road.value = register.notes[i].deterRoad;
            data.hurt.value = register.notes[i].hurtType;
            data.type.value = register.notes[i].Type;
            temp.GetComponent<DataNote>().id=i;
            string g = "";
            for(int o=0; o< (6 - i.ToString().Length); o++)
            {
                g += " ";
            }
            temp.GetComponent<TextMeshProUGUI>().text = "        ID:" + i.ToString() + g + "Time:                                                                                 Speed:";
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
        ERSRegister register = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().register;
        register.sort();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, register.noteNum * 20 + 2);
        string color = "", rotate = "", hurt = "", road = "", speed = "";
        for (int i = 0; i < register.noteNum; i++)
        {
            GameObject temp = Instantiate(text, transform) as GameObject;
            DataNote data = temp.GetComponent<DataNote>();
            data.rotate.value = register.notes[i].angleType;
            data.time.text = register.notes[i].deterTime.ToString();
            data.road.value = register.notes[i].deterRoad;
            data.hurt.value = register.notes[i].hurtType;
            data.type.value = register.notes[i].Type;
            temp.GetComponent<DataNote>().id = i;
        }
    }
}
