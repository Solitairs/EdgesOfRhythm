using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataNote : MonoBehaviour
{
    //����ÿһ���洢������Ϣ��text
    public int id;
    public TMP_InputField time, speed;
    public TMP_Dropdown rotate, type, hurt, road;
    public void delNote()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().register.delNote(id);
        transform.parent.gameObject.GetComponent<setNotesInformations>().refresh();
    }
}
